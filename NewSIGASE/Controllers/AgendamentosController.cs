using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewSIGASE.Comum;
using NewSIGASE.Dto;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Infra.Configuration;
using NewSIGASE.Models.Enum;
using NewSIGASE.Services.Interfaces;

namespace NewSIGASE.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        public AgendamentosController(IAgendamentoService agendamentoService,
            IUsuarioService usuarioService,
            ISalaService salaService)
        {
            _agendamentoService = agendamentoService;
            _usuarioService = usuarioService;
            _salaService = salaService;
        }

        // GET: Agendamentos
        public IActionResult Index()
        {
            var agendamentos = _agendamentoService.Obter(AppSettings.Perfil, AppSettings.Usuario);

            return View(agendamentos.Select(a => new AgendamentoItemListaDto(a)));
        }

        // GET: Agendamentos/ObterDados
        [HttpGet]
        public async Task<JsonResult> ObterAgendamento(Guid id)
        {
            var agendamento = await _agendamentoService.ObterAsync(id);

            return Json(new AgendamentoItemListaDto(agendamento));
        }

        [HttpPost]
        public async Task<JsonResult> AprovarAgendamento(AgendamentoAprovacaoDto aprovacao)
        {
            await _agendamentoService.AprovarAgendamento(aprovacao, AppSettings.Usuario);
            if (_agendamentoService.Invalid)
            {
                var erro = true;
                var strErro = string.Join(" - ", _agendamentoService.Notifications);
                return Json(new { erro, strErro });
            }

            return Json(new { erro = false, strErro = " " });
        }

        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.TipoSala = Combos.retornarOpcoesSala();

            return View();
        }

        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgendamentoDto dto)
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.TipoSala = Combos.retornarOpcoesSala();

            dto.Validate();
            if (dto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, TipoNotificacao.Warning);
                return View(dto);
            }

            await _agendamentoService.CriarAsync(dto);
            if (_agendamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_agendamentoService.Notifications, TipoNotificacao.Warning);
                return View(dto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarAgendamento", "Agendamento cadastrado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        [HttpGet]
        [Route("/Agendamentos/GerarRelatorio")]
        public IActionResult GerarRelatorio(AgendamentoRelatorioFiltroDto filtro)
        {
            filtro.DataInicio ??= DateTime.Now.Date;
            filtro.DataFim ??= DateTime.Now.Date.AddDays(10);

            ViewBag.DataInicio = filtro.DataInicio.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.DataFim = filtro.DataFim.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.Sala = filtro.Sala ?? string.Empty;
            ViewBag.Usuario = filtro.Usuario ?? string.Empty;
            ViewBag.Perfil = new SelectList(Combos.retornarOpcoesPerfil(), "Value", "Text", (int)filtro.PerfilUsuario);
            ViewBag.TipoLocal = new SelectList(Combos.retornarOpcoesSala(), "Value", "Text", (int)filtro.TipoLocal);

            var agendamentos = _agendamentoService.GerarRelatorio(filtro.DataInicio.Value, filtro.DataFim.Value);

            if (filtro.TipoLocal != EnumTipoSala.Nenhum)
            {
                agendamentos = agendamentos.Where(a => a.Sala.Tipo == filtro.TipoLocal);
            }

            if (!string.IsNullOrEmpty(filtro.Sala))
            {
                agendamentos = agendamentos.Where(a => a.Sala.IdentificadorSala.ToLower().Contains(filtro.Sala.ToLower()));
            }

            if (filtro.PerfilUsuario != EnumTipoPerfil.Nenhum)
            {
                agendamentos = agendamentos.Where(a => a.Usuario.Perfil == filtro.PerfilUsuario);
            }

            if (!string.IsNullOrEmpty(filtro.Usuario))
            {
                agendamentos = agendamentos.Where(a => a.Usuario.Nome.ToLower().Contains(filtro.Usuario.ToLower()));
            }

            var usuariosAprovadores = _usuarioService.ObterPorPerfil(EnumTipoPerfil.Administrador);

            var retorno = new List<AgendamentoRelatorioDto>();

            foreach (var agendamento in agendamentos)
            {
                var aprovador = usuariosAprovadores.Where(u => u.Id == agendamento.AprovadorId).FirstOrDefault();

                retorno.Add(new AgendamentoRelatorioDto(agendamento, aprovador));
            }

            return View("Relatorio", retorno);
        }

        public JsonResult RetornarSalas(EnumTipoSala tipoSala)
        {
            var salas = _salaService.Obter(tipoSala);
            Json(salas.Where(s => s.Observacao != null).Select(s => new { id = s.IdentificadorSala, obs = s.Observacao}).ToList());

            return Json(salas.Select(s => new SalaListaDto(s)).ToList());
        }

        public JsonResult RetorarObservacoes(EnumTipoSala tipoSala, Guid idSala)
        {
            var salas = _salaService.Obter(tipoSala).Where(s => s.Id == idSala);
            return Json(salas.Where(s => s.Observacao != null).Select(s => new { id = s.IdentificadorSala, obs = s.Observacao }));
        }

        //GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var agendamento = await _agendamentoService.ObterAsync(id);           

            if (_agendamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_agendamentoService.Notifications, TipoNotificacao.Warning);
                ViewBag.Controller = "Agendamentos";
                return View("_Confirmacao");
            }

            ViewBag.Tipo = (int)agendamento.Sala.Tipo;
            ViewBag.Sala = agendamento.SalaId;

            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.TipoSala = Combos.retornarOpcoesSala();

            return View(new AgendamentoDto(agendamento));
        }

        // POST: Agendamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AgendamentoDto dto)
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.TipoSala = Combos.retornarOpcoesSala();

            dto.Validate();
            if (dto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, TipoNotificacao.Warning);
                return View(dto);
            }

            await _agendamentoService.EditarAsync(dto);
            if (_agendamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_agendamentoService.Notifications, TipoNotificacao.Warning);
                return View(dto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento Editado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        // GET: Agendamentos/Delete/5
        public async Task<IActionResult> Cancelar(Guid id)
        {
            await _agendamentoService.Cancelar(id, AppSettings.Usuario);
            if (_agendamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_agendamentoService.Notifications, TipoNotificacao.Warning);
            }
            else
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CancelarAgendamento", "Agendamento cancelado com sucesso.") }, TipoNotificacao.Success);
            }

            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        [HttpGet]
        [Route("/Agendamentos/RelatorioAprovados")]
        public IActionResult RelatorioAprovados(AgendamentoRelatorioFiltroDto filtro)
        {
            filtro.DataInicio ??= DateTime.Now.Date;
            filtro.DataFim ??= DateTime.Now.Date.AddDays(10);

            ViewBag.DataInicio = filtro.DataInicio.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.DataFim = filtro.DataFim.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.Sala = filtro.Sala ?? string.Empty;
            ViewBag.Usuario = filtro.Usuario ?? string.Empty;
            ViewBag.TipoLocal = new SelectList(Combos.retornarOpcoesSala(), "Value", "Text", (int)filtro.TipoLocal);
            ViewBag.Status = new SelectList(Combos.retornarOpcoesStatusAgendamento(), "Value", "Text");

            var agendamentos = _agendamentoService.GerarRelatorio(filtro.DataInicio.Value, filtro.DataFim.Value);

            if (filtro.TipoLocal != EnumTipoSala.Nenhum)
            {
                agendamentos = agendamentos.Where(a => a.Sala.Tipo == filtro.TipoLocal);
            }

            if (!string.IsNullOrEmpty(filtro.Sala))
            {
                agendamentos = agendamentos.Where(a => a.Sala.IdentificadorSala.ToLower().Contains(filtro.Sala.ToLower()));
            }

            if (!string.IsNullOrEmpty(filtro.Usuario))
            {
                agendamentos = agendamentos.Where(a => a.Usuario.Nome.ToLower().Contains(filtro.Usuario.ToLower()));
            }

            if (filtro.StatusAgendamento != null)
            {
                ViewBag.Status = new SelectList(Combos.retornarOpcoesStatusAgendamento(), "Value", "Text", (int)filtro.StatusAgendamento);

                agendamentos = agendamentos.Where(a => a.Status == filtro.StatusAgendamento);
            }

            var usuariosAprovadores = _usuarioService.ObterPorPerfil(EnumTipoPerfil.Administrador);

            var retorno = new List<AgendamentoRelatorioDto>();

            foreach (var agendamento in agendamentos)
            {
                var aprovador = usuariosAprovadores.Where(u => u.Id == agendamento.AprovadorId).FirstOrDefault();

                retorno.Add(new AgendamentoRelatorioDto(agendamento, aprovador));
            }

            return View("RelatorioAprovacao", retorno);
        }
    }
}

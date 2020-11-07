using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EllipticCurve.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Comum;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Infra.Configuration;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using NewSIGASE.Services;
using NewSIGASE.Services.Interfaces;

namespace NewSIGASE.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IUsuarioService _usuarioService;
        public AgendamentosController(IAgendamentoService agendamentoService,
            IUsuarioService usuarioService)
        {
            _agendamentoService = agendamentoService;
            _usuarioService = usuarioService;
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
            //ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

            return View();
        }

        //public JsonResult RetornarSalas(EnumTipoSala tipoSala)
        //{
        //    var salas = _context.Salas.AsNoTracking().Where(s => s.Tipo == tipoSala).ToList();
        //    return Json(salas);
        //}

        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgendamentoDto dto)
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            //ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

            dto.Validate();
            if (dto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, "warning");
                return View(dto);
            }

            await _agendamentoService.CriarAsync(dto);
            if (_agendamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, "warning");
                return View(dto);
            }
            
            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarAgendamento", "Agendamento cadastrado com sucesso.") }, "success");
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        // GET: Agendamentos/Edit/5
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var agendamento = await _context.Agendamentos
        //        .Include(a => a.Sala)
        //        .Include(a => a.Usuario)
        //        .ThenInclude(u => u.Endereco)
        //        .FirstOrDefaultAsync(a => a.Id == id);

        //    if (agendamento == null)
        //    {
        //        TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento não encontrado.") }, "warning");
        //        ViewBag.Controller = "Agendamentos";
        //        return View("_Confirmacao");
        //    }

        //    ViewBag.TipoSala = (int)agendamento.Sala.Tipo;
        //    ViewBag.Sala = agendamento.SalaId;

        //    ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
        //    ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala", agendamento.SalaId);

        //    return View(new AgendamentoDto(agendamento));
        //}

        // POST: Agendamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(AgendamentoDto dto)
        //{
        //    ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
        //    ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala", dto.SalaId);

        //    if (dto.Id == null)
        //    {
        //        return NotFound();
        //    }

        //    dto.Validate();
        //    if (dto.Invalid)
        //    {
        //        TempData["Notificacao"] = new BadRequestDto(dto.Notifications, "warning");
        //        return View(dto);
        //    }

        //    var agendamentoEditar = await _context.Agendamentos.FirstOrDefaultAsync(a => a.Id == dto.Id);
        //    if (agendamentoEditar == null)
        //    {
        //        TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento não encontrado.") }, "warning");
        //        return View(dto);
        //    }

        //    var agendamentoDuplicado = _context.Agendamentos.AsNoTracking().FirstOrDefault(a => a.SalaId == dto.SalaId && a.Periodo == dto.Periodo && a.DataAgendada.Date == dto.DataAgendada.Date);

        //    if (agendamentoDuplicado != null && agendamentoEditar.UsuarioId != agendamentoDuplicado.UsuarioId)
        //    {
        //        TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Já existe esse agendamento para outro usuário.") }, "warning");
        //        return View(dto);
        //    }

        //    agendamentoEditar.Editar(dto.DataAgendada, dto.Periodo, dto.SalaId);
        //    agendamentoEditar.AtualizarAgendamento(Guid.Empty, EnumStatusAgendamento.Pendente, null);

        //    _context.Entry<Agendamento>(agendamentoEditar).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento Editado com sucesso.") }, "success");
        //    ViewBag.Controller = "Agendamentos";
        //    return View("_Confirmacao");
        //}

        // GET: Agendamentos/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var agendamento = await _context.Agendamentos.FirstOrDefaultAsync(m => m.Id == id);
        //    if (agendamento == null)
        //    {
        //        return NotFound();
        //    }

        //    agendamento.AtualizarAgendamento(AppSettings.Usuario, EnumStatusAgendamento.Cancelado);

        //    _context.Entry<Agendamento>(agendamento).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult GerarRelatorio(AgendamentoRelatorioFiltroDto filtro)
        {
            filtro.DataInicio ??= DateTime.Now.Date;
            filtro.DataFim ??= DateTime.Now.Date.AddDays(10);

            ViewBag.DataInicio = filtro.DataInicio.Value.Date.ToString("yyyy-MM-dd");
            ViewBag.DataFim = filtro.DataInicio.Value.Date.ToString("yyyy-MM-dd");
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EllipticCurve.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Infra.Configuration;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using NewSIGASE.Services;
using NewSIGASE.Services.InterfacesServices;

namespace NewSIGASE.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly SIGASEContext _context;
        private readonly IEmailService _emailService;
        public AgendamentosController(SIGASEContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Agendamentos
        public IActionResult Index()
        {
            List<Agendamento> agendamentos;
            if (AppSettings.Perfil == EnumTipoPerfil.Professor.ToString())
            {
                agendamentos = _context.Agendamentos
                    .Include(a => a.Sala)
                    .Include(a => a.Usuario)
                    .AsNoTracking()
                    .Where(a => a.UsuarioId == AppSettings.Usuario)
                    .ToList();
            }
            else
            {
                agendamentos = _context.Agendamentos
                    .Include(a => a.Sala)
                    .Include(a => a.Usuario)
                    .AsNoTracking()
                    .ToList();
            }

            //tratamento para exibição na grid, mostrar apenas nome e sobrenome. 
            foreach (var a in agendamentos)
            {
                var nome = a.Usuario.Nome.Split(" ");
                if (nome.Length > 1)
                    a.Usuario.Nome = nome[0] + " " + nome[1];
            }

            return View(agendamentos.Select(a => new AgendamentoListaDto(a)));
        }

        // GET: Agendamentos/ObterDados
        [HttpGet]
        public JsonResult ObterAgendamento(Guid id)
        {
            var retorno = _context.Agendamentos.Include(a => a.Sala).Include(a => a.Usuario).AsNoTracking().FirstOrDefault(a => a.Id == id);
            return Json(new AgendamentoListaDto(retorno));
        }

        [HttpPost]
        public JsonResult AprovarAgendamento(AgendamentoAprovacao aprovacao)
        {
            var retorno = _context.Agendamentos.Include(a => a.Usuario).Include(a => a.Sala).FirstOrDefault(a => a.Id == aprovacao.AgendamentoId);
            retorno.AtualizarAgendamento(AppSettings.Usuario, aprovacao.Status, aprovacao.Justificativa);
            string strErro = "";
            bool erro = false;
            try
            {
                _context.Entry<Agendamento>(retorno).State = EntityState.Modified;
                _context.SaveChanges();

                _emailService.AdicionarDestinatario(retorno.Usuario.Email, retorno.Usuario.Nome);
                _emailService.EnviarEmailAprovacaoAgendamento(retorno);
                erro = false;
            }
            catch(Exception ex)
            {
                erro = true;
                strErro = ex.Message;
            }

            return Json(new { erro = erro.ToString(), strErro = strErro.ToString()});
        }
        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

            return View();
        }

        public JsonResult RetornarSalas(EnumTipoSala tipoSala)
        {
            var salas = _context.Salas.AsNoTracking().Where(s => s.Tipo == tipoSala).ToList();
            return Json(salas);
        }

        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgendamentoDto dto)
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

            dto.Validate();
            if (dto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, "warning");
                return View(dto);
            }

            var sala = _context.Salas.FirstOrDefault(s => s.Id == dto.SalaId);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == dto.UsuarioId);
            if (sala == null || usuario == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarAgendamento", "Sala ou Usuario não encontrado.") }, "warning");
                return View(dto);
            }

            var agendamentoExiste = _context.Agendamentos.Where(a => a.SalaId == dto.SalaId && a.Periodo == dto.Periodo && a.DataAgendada.Date == dto.DataAgendada.Date).FirstOrDefault();
            if (agendamentoExiste != null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarAgendamento", "A sala escolhida já está reservada para esse período. Favor escolher outro.") }, "warning");
                return View(dto);
            }

            var agendamento = new Agendamento(dto.DataAgendada, dto.Periodo, dto.SalaId, dto.UsuarioId);

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarAgendamento", "Agendamento cadastrado com sucesso.") }, "success");
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        // GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Sala)
                .Include(a => a.Usuario)
                .ThenInclude(u => u.Endereco)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento não encontrado.") }, "warning");
                ViewBag.Controller = "Agendamentos";
                return View("_Confirmacao");
            }

            ViewBag.TipoSala = (int)agendamento.Sala.Tipo;
            ViewBag.Sala = agendamento.SalaId;

            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala", agendamento.SalaId);

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
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala", dto.SalaId);

            if (dto.Id == null)
            {
                return NotFound();
            }

            dto.Validate();
            if (dto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(dto.Notifications, "warning");
                return View(dto);
            }

            var agendamentoEditar = await _context.Agendamentos.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (agendamentoEditar == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento não encontrado.") }, "warning");
                return View(dto);
            }

            var agendamentoDuplicado = _context.Agendamentos.AsNoTracking().FirstOrDefault(a => a.SalaId == dto.SalaId && a.Periodo == dto.Periodo && a.DataAgendada.Date == dto.DataAgendada.Date);

            if (agendamentoDuplicado != null && agendamentoEditar.UsuarioId != agendamentoDuplicado.UsuarioId)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Já existe esse agendamento para outro usuário.") }, "warning");
                return View(dto);
            }

            agendamentoEditar.Editar(dto.DataAgendada, dto.Periodo, dto.SalaId);
            agendamentoEditar.AtualizarAgendamento(Guid.Empty, EnumStatusAgendamento.Pendente, null);

            _context.Entry<Agendamento>(agendamentoEditar).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento Editado com sucesso.") }, "success");
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        // GET: Agendamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.AtualizarAgendamento(AppSettings.Usuario, EnumStatusAgendamento.Cancelado);

            _context.Entry<Agendamento>(agendamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GerarRelatorio(AgendamentoRelatorioFiltroDto filtro)
        {
            ViewBag.DataInicio = filtro.DataInicio?.ToString("yyyy-MM-dd") ?? DateTime.Now.Date.ToString("yyyy-MM-dd");
            ViewBag.DataFim = filtro.DataFim?.ToString("yyyy-MM-dd") ?? DateTime.Now.AddDays(10).Date.ToString("yyyy-MM-dd");
            ViewBag.Sala = filtro.Sala ?? string.Empty;
            ViewBag.Usuario = filtro.Usuario ?? string.Empty;
            ViewBag.Perfil = new SelectList(Combos.retornarOpcoesPerfil(), "Value", "Text", (int)filtro.PerfilUsuario);
            ViewBag.TipoLocal = new SelectList(Combos.retornarOpcoesSala(), "Value", "Text", (int)filtro.TipoLocal);

            var agendamentos = _context.Agendamentos
                .Include(a => a.Usuario)
                .Include(a => a.Sala)
                    .ThenInclude(s => s.Equipamentos)
                .Where(a => a.DataAgendada >= filtro.DataInicio && a.DataAgendada <= filtro.DataFim)
                .AsNoTracking();

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

            var usuariosAprovadores = await _context.Usuarios.AsNoTracking().Where(u => u.Perfil == EnumTipoPerfil.Administrador).ToListAsync();

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

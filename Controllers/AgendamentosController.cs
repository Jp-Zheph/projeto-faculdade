using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Infra.Configuration;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;

namespace NewSIGASE.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly SIGASEContext _context;

        public AgendamentosController(SIGASEContext context)
        {
            _context = context;
        }

        // GET: Agendamentos
        public IActionResult Index()
        {
            List<Agendamento> agendamentos;
            if (AppSettings.Perfil == EnumTipoPerfil.Administrador.ToString())
            {
                agendamentos = _context.Agendamentos.Include(a => a.Sala).Include(a => a.Usuario).AsNoTracking().ToList();
            }
            else
            {
                agendamentos = _context.Agendamentos.Include(a => a.Sala).Include(a => a.Usuario).AsNoTracking().Where(a => a.UsuarioId == AppSettings.Usuario).ToList();
            }

            return View(agendamentos.Select(a => new AgendamentoListaDto(a)));
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
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarAgendamento", "Agendamento não encontrado.") }, "warning");
                ViewBag.Controller = "Agendamentos";
                return View("_Confirmacao");
            }

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

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Models;

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
            var agendamentos = _context.Agendamentos.Include(a => a.Sala).Include(a => a.Usuario).AsNoTracking();

            return View(agendamentos.Select(a => new AgendamentoListaDto(a)));
        }

        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

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
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala");

            dto.Validate();
            if (dto.Invalid)
            {
                return View(dto);
            }

            var sala = _context.Salas.FirstOrDefault(s => s.Id == dto.SalaId);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == dto.UsuarioId);
            if (sala == null || usuario == null)
            {
                return View(dto);
            }

            var agendamentoExiste = _context.Agendamentos.Where(a => a.SalaId == dto.SalaId && a.Periodo == dto.Periodo && a.DataAgendada.Date == dto.DataAgendada.Date).FirstOrDefault();
            if (agendamentoExiste != null)
            {
                //Mensagem: A sala escolhida já está reservada para esse período. Favor escolher outro.
                return View(dto);
            }

            var agendamento = new Agendamento(dto.DataAgendada, dto.Periodo, dto.SalaId, dto.UsuarioId);

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Sala)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (agendamento == null)
            {
                return NotFound();
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
            if (dto.Id == null)
            {
                return NotFound();
            }

            var agendamentoEditar = _context.Agendamentos.FirstOrDefault(a => a.Id == dto.Id);
            if (agendamentoEditar == null)
            {
                return NotFound();
            }

            ViewBag.Periodo = Combos.retornarOpcoesPeriodo();
            ViewBag.Salas = new SelectList(_context.Salas.AsNoTracking(), "Id", "IdentificadorSala", dto.SalaId);

            var agendamentoDuplicado = _context.Agendamentos.AsNoTracking().FirstOrDefault(a => a.SalaId == dto.SalaId && a.Periodo == dto.Periodo && a.DataAgendada.Date == dto.DataAgendada.Date);

            if (agendamentoDuplicado != null && agendamentoEditar.UsuarioId != agendamentoDuplicado.UsuarioId)
            {
                //Mensagem Já existe esse agendamento para outro usuario.
                return View(dto);
            }

            agendamentoEditar.Editar(dto.DataAgendada, dto.Periodo, dto.SalaId);

            _context.Entry<Agendamento>(agendamentoEditar).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
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

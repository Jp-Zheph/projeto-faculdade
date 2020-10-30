using System;
using System.Collections.Generic;
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
    public class SalasController : Controller
    {
        private readonly SIGASEContext _context;

        public SalasController(SIGASEContext context)
        {
            _context = context;
        }

        // GET: Salas
        public IActionResult Index()
        {
            var lista = _context.Salas.Include(s => s.Equipamentos).AsNoTracking();
            if (lista == null)
            {
                lista = Array.Empty<Sala>().AsQueryable();
            }

            return View(lista.Select(x => new SalaListaDto(x)));
        }


        // GET: Salas/Create
        public IActionResult Create()
        {
            ViewBag.TipoSala = Combos.retornarOpcoesSala();
            ViewBag.Equipamentos = new SelectList(_context.Equipamentos.AsNoTracking().Where(e => e.SalaId == null), "Id", "NomeModelo");

            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalaDto salaDto)
        {
            ViewBag.TipoSala = Combos.retornarOpcoesSala();
            ViewBag.Equipamentos = new SelectList(_context.Equipamentos.AsNoTracking().Where(e => e.SalaId == null), "Id", "NomeModelo");

            salaDto.Validate();
            if (salaDto.Invalid)
            {

                return View(salaDto);
            }

            var identificadorSala = _context.Salas
                .Where(x => x.IdentificadorSala == salaDto.IdentificadorSala)
                .FirstOrDefault();

            if (identificadorSala != null)
            {
                return View(salaDto);
            }

            List<Equipamento> listaEquips = salaDto.EquipamentoId == null ? null : _context.Equipamentos.Where(e => salaDto.EquipamentoId.Contains(e.Id)).ToList();
            var sala = new Sala(salaDto.Tipo, salaDto.IdentificadorSala, salaDto.Observacao, salaDto.CapacidadeAlunos);

            foreach (var e in listaEquips)
            {
                e.SalaId = sala.Id;
                _context.Equipamentos.Update(e);
            }
            _context.Salas.Add(sala);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FirstOrDefaultAsync(s => s.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(new SalaDto(sala));
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SalaDto salaDto)
        {
            if (salaDto.Id == null)
            {
                return View();
            }

            salaDto.Validate();
            if (salaDto.Invalid)
            {
                return View(salaDto);
            }

            var salaEditar = await _context.Salas.FirstOrDefaultAsync(s => s.Id == salaDto.Id);
            if (salaEditar == null)
            {
                return NotFound();
            }

            var identificadorSalaDuplicado = _context.Salas
                .Where(x => x.IdentificadorSala == salaEditar.IdentificadorSala)
                .AsNoTracking()
                .FirstOrDefault();

            if (identificadorSalaDuplicado != null && identificadorSalaDuplicado.Id != salaEditar.Id)
            {
                return View(salaDto);
            }

            salaEditar.Editar(salaDto.Tipo, salaDto.IdentificadorSala, salaDto.Observacao, salaDto.CapacidadeAlunos);

            _context.Entry<Sala>(salaEditar).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }
    }
}

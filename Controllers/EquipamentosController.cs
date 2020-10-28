using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Models;

namespace NewSIGASE.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly SIGASEContext _context;

        public EquipamentosController(SIGASEContext context)
        {
            _context = context;
        }

        // GET: Equipamentos
        public IActionResult Index()
        {
            var equipamentos = _context.Equipamentos.AsNoTracking();
            return View(equipamentos?.Select(e => new EquipamentoListaDto(e)));
        }

        // GET: Equipamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipamentoDto equipamentoDto)
        {
            equipamentoDto.Validate();
            if (equipamentoDto.Invalid)
            {
                return View(equipamentoDto);
            }

            var serialDuplicado = await _context.Equipamentos.FirstOrDefaultAsync(e => e.Serial == equipamentoDto.Serial);
            if (serialDuplicado != null)
            {
                return View(equipamentoDto); //TODO - mensagem validação serial ja existe
            }

            var equipamento = new Equipamento(equipamentoDto.Serial, equipamentoDto.Nome, equipamentoDto.Modelo, null);

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(new EquipamentoDto(equipamento));
        }

        // POST: Equipamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EquipamentoDto equipamentoDto)
        {
            equipamentoDto.Validate();
            if (equipamentoDto.Invalid)
            {
                return View(equipamentoDto);
            }

            var equipamento = await _context.Equipamentos.FirstOrDefaultAsync(u => u.Id == equipamentoDto.Id);
            if (equipamento == null)
            {
                return View(equipamentoDto); //TODO - msg validação equipamento não existe.
            }

            var serialDuplicado = _context.Equipamentos
                .AsNoTracking()
                .Where(e => e.Serial == equipamentoDto.Serial)
                .FirstOrDefault();

            if (serialDuplicado != null && serialDuplicado.Id != equipamento.Id)
            {
                return View(equipamentoDto); // TODO - msg validação serial já cadastrado em outro equipamento.
            }

            equipamento.Modelo = equipamentoDto.Modelo;
            equipamento.Nome = equipamentoDto.Nome;
            equipamento.Serial = equipamentoDto.Serial;

            _context.Update(equipamento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamentos
                //.Include(e => e.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipamento == null)
            {
                return NotFound();
            }

            if (equipamento == null) //TODO verificar se equipamento tem sala: equipamento.sala
            {
                //TODO - msg validação o equipamento está vinculado a uma sala.
            }
            else
            {
                _context.Equipamentos.Remove(equipamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

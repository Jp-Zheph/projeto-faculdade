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
	public class SalasController : Controller
    {
        private readonly SIGASEContext _context;

        public SalasController(SIGASEContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            var lista = _context.Salas.AsNoTracking();
            return View(lista.Select(x=>new SalaListaDto(x)));
           
        }

       
        // GET: Salas/Create
        public IActionResult Create()
        {
            ViewBag.Sala = Combos.retornarOpcoesSala();
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaDto saladto)
        {
            saladto.Validate();

             var identificadorSala = _context.Salas.Where(x => x.IdentificadorSala == saladto.IdentificadorSala)
                .FirstOrDefault();

            if (saladto.Invalid)
            {
                return View(saladto);
               
            }
          
            if(identificadorSala != null)
			{
                return View(saladto);
			}

            Sala sala = new Sala(saladto.Tipo, saladto.IdentificadorSala, saladto.Observacao, saladto.CapacidadeAlunos);
           
            _context.Add(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SalaDto sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
              
                
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
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

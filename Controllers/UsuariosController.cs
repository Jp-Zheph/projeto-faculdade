using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Services;
using SIGASE.Models;

namespace NewSIGASE.Controllers {
    public class UsuariosController : Controller {

        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = _usuarioService.Obter();

            return View(usuarios.Select(u => new UsuarioListaDto(u)));
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var usuario = _usuarioService.Obter(id);

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {           
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCriarDto usuarioDto)
        {
            usuarioDto.Validate();

            if (usuarioDto.Invalid)
            {
                return View();
            }

            await _usuarioService.Criar(usuarioDto);
            if (_usuarioService.Invalid)
            {
                return View();
            }

            return View();
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
           
            return View();
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Matricula,Email,Nome,Senha,Tipo")] Usuario usuario)
        {
           
            return View();
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
         

            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}

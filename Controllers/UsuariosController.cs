﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Services.InterfacesServices;
using NewSIGASE.Models;

namespace NewSIGASE.Controllers {
    public class UsuariosController : Controller {

        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _usuarioService.Obter();

            return View(usuarios?.Select(u => new UsuarioListaDto(u)));
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.Perfil = Combos.retornarOpcoesPerfil();
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDto usuarioDto)
        {
            ViewBag.Perfil = Combos.retornarOpcoesPerfil();

            usuarioDto.Validate();
            if (usuarioDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(usuarioDto.Notifications);
                return View(usuarioDto);
            }

            await _usuarioService.Criar(usuarioDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications);
                return View(usuarioDto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var usuario = await _usuarioService.Obter(id);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Perfil = Combos.retornarOpcoesPerfil();

            return View(new UsuarioDto(usuario));
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioDto usuarioDto)
        {
            ViewBag.Perfil = Combos.retornarOpcoesPerfil();

            if (usuarioDto.Id == null)
            {                
                return NotFound();
            }

            usuarioDto.Validate();
            if (usuarioDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(usuarioDto.Notifications);
                return View(usuarioDto);
            }

            await _usuarioService.Editar(usuarioDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications);
                return View(usuarioDto);
            }
           
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {         

            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/CriarSenha
        public IActionResult AlterarSenha(Guid id)
        {
            ViewBag.UsuarioId = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(SenhaCriarDto senhaDto)
        {
            ViewBag.UsuarioId = senhaDto.Id;
            senhaDto.Validate();
            if (senhaDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(senhaDto.Notifications);
                return View(senhaDto);
            }

            await _usuarioService.CriarSenha(senhaDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications);
                return View(senhaDto);
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Services.Interfaces;
using System.Collections.Generic;
using Flunt.Notifications;
using NewSIGASE.Comum;
using NewSIGASE.Dto.Response.IntegracaoCorreios;
using NewSIGASE.Dto;

namespace NewSIGASE.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly IUsuarioService _usuarioService;
        private readonly ICepService _cepService;

        public UsuariosController(IUsuarioService usuarioService,
            ICepService cepService)
        {
            _usuarioService = usuarioService;
            _cepService = cepService;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _usuarioService.Obter();

            return View(usuarios.Select(u => new UsuarioListaDto(u)));
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
                TempData["Notificacao"] = new BadRequestDto(usuarioDto.Notifications, TipoNotificacao.Warning);
                return View(usuarioDto);
            }

            await _usuarioService.Criar(usuarioDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications, TipoNotificacao.Warning);
                return View(usuarioDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarUsuario", "Usuário cadastrado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Usuarios";
            return View("_Confirmacao");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var usuario = await _usuarioService.Obter(id);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications, TipoNotificacao.Warning);
                ViewBag.Controller = "Usuarios";
                return View("_Confirmacao");
            }

            ViewBag.Perfil = Combos.retornarOpcoesPerfil();
            ViewBag.Status = Combos.retornarOpcoesStatus();

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
            ViewBag.Status = Combos.retornarOpcoesStatus();
            if (usuarioDto.Id == null)
            {
                return NotFound();
            }

            usuarioDto.Validate();
            if (usuarioDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(usuarioDto.Notifications, TipoNotificacao.Warning);
                return View(usuarioDto);
            }

            await _usuarioService.Editar(usuarioDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications, TipoNotificacao.Warning);
                return View(usuarioDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarUsuario", "Usuário editado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Usuarios";
            return View("_Confirmacao");
        }

        // POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.Controller = "Usuarios";

            await _usuarioService.Deletar(id);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications, TipoNotificacao.Warning);
                return View("_Confirmacao");
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirUsuario", "Usuário excluído com sucesso.") }, TipoNotificacao.Success);
            return View("_Confirmacao");
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
                TempData["Notificacao"] = new BadRequestDto(senhaDto.Notifications, TipoNotificacao.Warning);
                return View(senhaDto);
            }

            await _usuarioService.CriarSenha(senhaDto);
            if (_usuarioService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_usuarioService.Notifications, TipoNotificacao.Warning);
                return View(senhaDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("AlterarSenha", "Senha alterada com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Agendamentos";
            return View("_Confirmacao");
        }

        [HttpGet]
        [Route("/Usuarios/ConsultarCep")]
        public async Task<JsonResult> ConsultarCep(string cep)
        {
            var endereco = await _cepService.ConsultarCepAsync(cep);
            if (_cepService.Invalid)
            {
                var strErro = new BadRequestDto(_cepService.Notifications, TipoNotificacao.Warning);
                return Json(new { erro = true, strErro, endereco = "" });
            }

            return Json(new { erro = false, strErr = "", endereco });
        }
    }
}

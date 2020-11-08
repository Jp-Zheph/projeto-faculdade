using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Services.Interfaces;
using NewSIGASE.Models;
using Microsoft.AspNetCore.Http;
using NewSIGASE.Infra.Configuration;
using System;

namespace NewSIGASE.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Autenticar(string email, string password)
        {
            Usuario retorno = _usuarioService.ValidarLogin(email, password, out string mesangem);
            if (retorno != null)
            {
                HttpContext.Session.SetString("Perfil", retorno.Perfil.ToString());
                HttpContext.Session.SetString("UsuarioId", retorno.Id.ToString());
                AppSettings.Perfil = HttpContext.Session.GetString("Perfil");
                AppSettings.Usuario = Guid.Parse(HttpContext.Session.GetString("UsuarioId"));

                if(retorno.Senha == retorno.Matricula)
                {
                    return Json(new { erro = false, url = "Usuarios/AlterarSenha?id=" + retorno.Id });
                }

                return Json(new { erro = false, strErro = "", url = "Agendamentos/Index" });
            }
            return Json(new { erro = true, strErro = mesangem });
        }

        public IActionResult LogOut()
        {
            AppSettings.Perfil = "";
            AppSettings.Usuario = new Guid();

            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }
    }
}

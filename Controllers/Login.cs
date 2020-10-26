using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Services;
using SIGASE.Models;

namespace NewSIGASE.Controllers
{
    public class Login : Controller
    {
        private readonly UsuarioService _usuarioService;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Autenticar(string email, string password)
        {
            Usuario retorno = _usuarioService.ValidarLogin(email, password, out string mesangem);
            if (retorno != null)
            {
                RedirectToAction("Index", "Home");
            }
            return Json(new { erro = "true", strErro = mesangem });
        }
    }
}

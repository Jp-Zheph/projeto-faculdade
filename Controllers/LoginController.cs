using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Models.Enum;
using NewSIGASE.Services;
using NewSIGASE.Services.InterfacesServices;
using NewSIGASE.Models;
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
                return Json(new { erro = false, strErro = "" });
            }
            return Json(new { erro = true, strErro = mesangem });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NewSIGASE.Services.InterfacesServices;
using NewSIGASE.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

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
                Global.Perfil = HttpContext.Session.GetString("Perfil");
                return Json(new { erro = false, strErro = "" });
            }
            return Json(new { erro = true, strErro = mesangem });
        }
    }
}

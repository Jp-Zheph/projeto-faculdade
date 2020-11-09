using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewSIGASE.Comum;
using NewSIGASE.Dto;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Services.Interfaces;

namespace NewSIGASE.Controllers
{
    public class SalasController : Controller
    {
        private readonly ISalaService _salaService;
        private readonly IEquipamentoService _equipamentoService;

        public SalasController(ISalaService salaService,
            IEquipamentoService equipamentoService)
        {
            _salaService = salaService;
            _equipamentoService = equipamentoService;
        }

        // GET: Salas
        public IActionResult Index()
        {
            var salas = _salaService.Obter();

            return View(salas.Select(x => new SalaListaDto(x)));
        }

        //GET: Salas/Create
        public IActionResult Create()
        {
            ViewBag.TipoSala = Combos.retornarOpcoesSala();
            ViewBag.Equipamentos = new SelectList(_equipamentoService.ObterSemSala(), "Id", "NomeModelo");
            
            return View();
        }

        [HttpGet]
        public JsonResult RetornaEquipamentos()
        {
            return Json(new SelectList(_equipamentoService.ObterSemSala(), "Id", "NomeModelo").ToList());
        }

        //POST: Salas/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaDto salaDto)
        {
            ViewBag.TipoSala = Combos.retornarOpcoesSala();
            ViewBag.Equipamentos = new SelectList(_equipamentoService.ObterSemSala(), "Id", "NomeModelo");

            salaDto.Validate();
            if (salaDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(salaDto.Notifications, TipoNotificacao.Warning);
                return View(salaDto);
            }

            await _salaService.CriarAsync(salaDto);
            if (_salaService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_salaService.Notifications, TipoNotificacao.Warning);
                return View(salaDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarSala", "Sala cadastrada com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Salas";
            return View("_Confirmacao");
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var sala = await _salaService.ObterAsync(id);
            if (_salaService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_salaService.Notifications, TipoNotificacao.Warning);
                ViewBag.Controller = "Salas";
                return View("_Confirmacao");
            }

            ViewBag.TipoSala = new SelectList(Combos.retornarOpcoesSala(), "Value", "Text", sala.Tipo);
            ViewBag.Equipamentos = new SelectList(_equipamentoService.ObterSemSala(), "Id", "NomeModelo", sala.SalaEquipamentos.Select(s => s.EquipamentoId));

            return View(new SalaDto(sala));
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SalaDto salaDto)
        {
            ViewBag.TipoSala = new SelectList(Combos.retornarOpcoesSala(), "Value", "Text", salaDto.Tipo);
            ViewBag.Equipamentos = new SelectList(_equipamentoService.ObterSemSala(), "Id", "NomeModelo");

            salaDto.Validate();
            if (salaDto.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(salaDto.Notifications, TipoNotificacao.Warning);
                return View(salaDto);
            }

            await _salaService.EditarAsync(salaDto);
            if (_salaService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_salaService.Notifications, TipoNotificacao.Warning);
                return View(salaDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarSala", "Sala editada com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Salas";
            return View("_Confirmacao");
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.Controller = "Salas";

            await _salaService.DeletarAsync(id);
            if (_salaService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_salaService.Notifications, TipoNotificacao.Warning);
                return View("_Confirmacao");
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirSala", "Sala excluida com sucesso.") }, TipoNotificacao.Success);
            return View("_Confirmacao");
        }
    }
}

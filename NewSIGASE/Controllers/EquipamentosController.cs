using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Models;
using NewSIGASE.Services.Interfaces;

namespace NewSIGASE.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentosController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        // GET: Equipamentos
        public IActionResult Index()
        {
            var equipamentos = _equipamentoService.Obter();

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
                TempData["Notificacao"] = new BadRequestDto(equipamentoDto.Notifications, TipoNotificacao.Warning);
                return View(equipamentoDto);
            }

            await _equipamentoService.CriarAsync(equipamentoDto);
            if (_equipamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_equipamentoService.Notifications, TipoNotificacao.Warning);
                return View(equipamentoDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarEquipamento", "Equipamento cadastrado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Equipamentos";
            return View("_Confirmacao");
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var equipamento = await _equipamentoService.ObterAsync(id);
            if (_equipamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_equipamentoService.Notifications, TipoNotificacao.Warning);
                ViewBag.Controller = "Equipamentos";
                return View("_Confirmacao");
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
                TempData["Notificacao"] = new BadRequestDto(equipamentoDto.Notifications, TipoNotificacao.Warning);
                return View(equipamentoDto);
            }

            await _equipamentoService.EditarAsync(equipamentoDto);
            if (_equipamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_equipamentoService.Notifications, TipoNotificacao.Warning);
                return View(equipamentoDto);
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarEquipamento", "Equipamento editado com sucesso.") }, TipoNotificacao.Success);
            ViewBag.Controller = "Equipamentos";
            return View("_Confirmacao");
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.Controller = "Equipamentos";

            await _equipamentoService.DeletarAsync(id);
            if (_equipamentoService.Invalid)
            {
                TempData["Notificacao"] = new BadRequestDto(_equipamentoService.Notifications, TipoNotificacao.Warning);
                return View("_Confirmacao");
            }

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirEquipamento", "Equipamento excluido com sucesso.") }, TipoNotificacao.Success);
            return View("_Confirmacao");
        }
    }
}

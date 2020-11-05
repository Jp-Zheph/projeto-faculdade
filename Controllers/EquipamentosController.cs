using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewSIGASE.Dto.Request;
using NewSIGASE.Dto.Response;
using NewSIGASE.Models;

namespace NewSIGASE.Controllers
{
    public class EquipamentosController : Controller
    {
        private readonly SIGASEContext _context;

        public EquipamentosController(SIGASEContext context)
        {
            _context = context;
        }

        // GET: Equipamentos
        public IActionResult Index()
        {
            var equipamentos = _context.Equipamentos.AsNoTracking();
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
                TempData["Notificacao"] = new BadRequestDto(equipamentoDto.Notifications, "warning");
                return View(equipamentoDto);
            }

            var serialDuplicado = await _context.Equipamentos.FirstOrDefaultAsync(e => e.Serial == equipamentoDto.Serial);
            if (serialDuplicado != null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarEquipamento", "Serial já cadastrado.") }, "warning");
                return View(equipamentoDto);
            }

            var equipamento = new Equipamento(equipamentoDto.Serial, equipamentoDto.Nome, equipamentoDto.Modelo, null, equipamentoDto.Peso, equipamentoDto.Cor, equipamentoDto.Comprimento, equipamentoDto.Largura, equipamentoDto.Altura);

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("CadastrarEquipamento", "Equipamento cadastrado com sucesso.") }, "success");
            ViewBag.Controller = "Equipamentos";
            return View("_Confirmacao");
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var equipamento = await _context.Equipamentos.FirstOrDefaultAsync(e => e.Id == id);
            if (equipamento == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarEquipamento", "Equipamento não encontrado.") }, "warning");
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
                TempData["Notificacao"] = new BadRequestDto(equipamentoDto.Notifications, "warning");
                return View(equipamentoDto);
            }

            var equipamento = await _context.Equipamentos.FirstOrDefaultAsync(u => u.Id == equipamentoDto.Id);
            if (equipamento == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarEquipamento", "Equipamento não encontrado.") }, "warning");
                return View(equipamentoDto);
            }

            var serialDuplicado = _context.Equipamentos
                .AsNoTracking()
                .Where(e => e.Serial == equipamentoDto.Serial)
                .FirstOrDefault();

            if (serialDuplicado != null && serialDuplicado.Id != equipamento.Id)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarEquipamento", "Serial já cadastrado.") }, "warning");
                return View(equipamentoDto);
            }

            equipamento.Editar(equipamento.Serial, equipamento.Nome, equipamento.Modelo, equipamento.Peso, equipamento.Cor, equipamento.Comprimento, equipamento.Largura, equipamento.Altura);

            _context.Entry<Equipamento>(equipamento).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("EditarEquipamento", "Equipamento editado com sucesso.") }, "success");
            ViewBag.Controller = "Equipamentos";
            return View("_Confirmacao");
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.Controller = "Equipamentos";

            var equipamento = await _context.Equipamentos
                .Include(e => e.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipamento == null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirEquipamento", "Equipamento não encontrado.") }, "warning");
                return View("_Confirmacao");
            }

            if (equipamento.Sala != null)
            {
                TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirEquipamento", "Não é possivel excluir pois equipamento está vinculado a uma sala.") }, "warning");
                return View("_Confirmacao");
            }

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();

            TempData["Notificacao"] = new BadRequestDto(new List<Notification>() { new Notification("ExcluirEquipamento", "Equipamento excluido com sucesso.") }, "success");
            return View("_Confirmacao");
        }
    }
}

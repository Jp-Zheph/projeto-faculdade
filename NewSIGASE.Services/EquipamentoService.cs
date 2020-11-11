using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSIGASE.Services
{
    public class EquipamentoService : Notifiable, IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipamentoRepository;

        public EquipamentoService(IEquipamentoRepository equipamentoRepository)
        {
            _equipamentoRepository = equipamentoRepository;
        }

        public IQueryable<Equipamento> Obter()
        {
            return _equipamentoRepository.Obter();
        }

        public IQueryable<Equipamento> ObterSemSala()
        {
            return _equipamentoRepository.ObterSemSala();
        }

        public SelectList ObterPorSalaEdicao(Guid salaId)
        {
            var listaComSala = _equipamentoRepository.ObterPorSala(salaId).ToList();
            var listaRetorno = new List<Equipamento>();

            if (listaComSala.Any())
            {
                listaRetorno.AddRange(listaComSala);
            }

            listaRetorno.AddRange(_equipamentoRepository.ObterSemSala().ToList());

            var listafim = new SelectList(listaRetorno, "Id", "NomeModelo");

            foreach(var e in listafim)
            {
                if(listaComSala.Any(i => i.Id == Guid.Parse(e.Value)))
                {
                    e.Selected = true;
                }
            }

            return listafim;
        }

        public async Task<Equipamento> ObterAsync(Guid id)
        {
            var equipamento = await _equipamentoRepository.ObterAsync(id);
            if (equipamento == null)
            {
                AddNotification("ObterEquipamento", MensagemValidacao.Equipamento.NaoExiste);
                return null;
            }

            return equipamento;
        }

        public async Task CriarAsync(EquipamentoDto dto)
        {

            var serialDuplicado = await _equipamentoRepository.ObterAsync(dto.Serial);
            if (serialDuplicado != null)
            {
                AddNotification("CadastrarEquipamento", MensagemValidacao.Equipamento.SerialJaExiste);
                return;
            }

            decimal? peso = dto.Peso == null ? (decimal?)null : Convert.ToDecimal(dto.Peso.Replace('.', ','));
            decimal? comprimento = dto.Comprimento == null ? (decimal?)null : Convert.ToDecimal(dto.Comprimento.Replace('.', ','));
            decimal? largura = dto.Largura == null ? (decimal?)null : Convert.ToDecimal(dto.Largura.Replace('.', ','));
            decimal? altura = dto.Altura == null ? (decimal?)null : Convert.ToDecimal(dto.Altura.Replace('.', ','));

            var equipamento = new Equipamento(dto.Serial, dto.Nome, dto.Modelo, peso, dto.Cor, comprimento, largura, altura, dto.Marca);

            try
            {
                await _equipamentoRepository.CriarAsync(equipamento);
            }
            catch(Exception ex)
            {
                AddNotification("CadastrarEquipamento", MensagemValidacao.ContacteSuporte);
                return;
            }
            
        }

        public async Task EditarAsync(EquipamentoDto dto)
        {
            if (dto.Id == null)
            {
                AddNotification("EditarAgendamento", MensagemValidacao.CampoInvalido);
                return;
            }

            var equipamentoEditar = await _equipamentoRepository.ObterAsync(dto.Id.Value);
            if (equipamentoEditar == null)
            {
                AddNotification("EditarEquipamento", MensagemValidacao.Equipamento.NaoExiste);
                return;
            }

            var serialDuplicado = await _equipamentoRepository.ObterAsync(dto.Serial);
            if (serialDuplicado != null && serialDuplicado.Id != equipamentoEditar.Id)
            {
                AddNotification("CadastrarEquipamento", MensagemValidacao.Equipamento.SerialJaExiste);
                return;
            }

            decimal? peso = dto.Peso == null ? (decimal?)null : Convert.ToDecimal(dto.Peso.Replace('.', ','));
            decimal? comprimento = dto.Comprimento == null ? (decimal?)null : Convert.ToDecimal(dto.Comprimento.Replace('.', ','));
            decimal? largura = dto.Largura == null ? (decimal?)null : Convert.ToDecimal(dto.Largura.Replace('.', ','));
            decimal? altura = dto.Altura == null ? (decimal?)null : Convert.ToDecimal(dto.Altura.Replace('.', ','));

            equipamentoEditar.Editar(dto.Serial, dto.Nome, dto.Modelo, peso, dto.Cor, comprimento, largura, altura, dto.Marca);

            await _equipamentoRepository.EditarAsync(equipamentoEditar);
        }

        public async Task DeletarAsync(Guid id)
        {
            var equipamento = await _equipamentoRepository.ObterAsync(id);
            if (equipamento == null)
            {
                AddNotification("ExcluirEquipamento", MensagemValidacao.Equipamento.NaoExiste);
                return;
            }

            AddNotifications(new Contract()
                .IsNotNull(equipamento, "ExcluirEquipamento", MensagemValidacao.Equipamento.NaoExiste)
                .IfNotNull(equipamento, x => x.IsNull(equipamento.SalaEquipamento, "ExcluirEquipamento", MensagemValidacao.Equipamento.PossuiSala))
            );

            if (Invalid)
            {
                return;
            }

            await _equipamentoRepository.DeletarAsync(equipamento);
        }
    }
}

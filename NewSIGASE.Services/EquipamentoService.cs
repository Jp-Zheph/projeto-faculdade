using Flunt.Notifications;
using Flunt.Validations;
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

            var equipamento = new Equipamento(dto.Serial, dto.Nome, dto.Modelo, dto.Peso, 
                dto.Cor, dto.Comprimento, dto.Largura, dto.Altura, dto.Marca);

            await _equipamentoRepository.CriarAsync(equipamento);
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

            equipamentoEditar.Editar(dto.Serial, dto.Nome, dto.Modelo, dto.Peso, dto.Cor, dto.Comprimento,
                dto.Largura, dto.Altura, dto.Marca);

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

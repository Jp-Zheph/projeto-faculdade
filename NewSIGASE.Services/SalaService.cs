using Flunt.Notifications;
using NewSIGASE.Data.Repositories.Interfaces;
using NewSIGASE.Dto.Request;
using NewSIGASE.Models;
using NewSIGASE.Models.Enum;
using NewSIGASE.Services.Constantes;
using NewSIGASE.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewSIGASE.Services
{
    public class SalaService : Notifiable, ISalaService
    {
        private readonly ISalaRepository _salaRepository;
        private readonly IEquipamentoRepository _equipamentoRepository;

        public SalaService(ISalaRepository salaRepository,
            IEquipamentoRepository equipamentoRepository)
        {
            _salaRepository = salaRepository;
            _equipamentoRepository = equipamentoRepository;
        }

        public IQueryable<Sala> Obter()
        {
            return _salaRepository.Obter();
        }

        public IQueryable<Sala> ObterSomenteAtivos()
        {
            return _salaRepository.ObterSomenteAtivos();
        }

        public IQueryable<Sala> Obter(EnumTipoSala tipo)
        {
            return _salaRepository.Obter(tipo);
        }

        public async Task<Sala> ObterAsync(Guid id)
        {
            var sala = await _salaRepository.ObterAsync(id);
            if (sala == null)
            {
                AddNotification("ObterSala", MensagemValidacao.Sala.NaoExiste);
                return null;
            }

            return sala;
        }

        public async Task CriarAsync(SalaDto dto)
        {
            var identificadorDuplicado = await _salaRepository.ObterAsync(dto.IdentificadorSala);
            if (identificadorDuplicado != null)
            {
                AddNotification("CadastrarSala", MensagemValidacao.Sala.IdentificadorJaExiste);
                return;
            }

            var sala = new Sala(dto.Tipo, dto.IdentificadorSala, dto.Observacao, Convert.ToDecimal(dto.Area.Replace('.', ',')), dto.Andar, dto.CapacidadeAlunos);

            if (dto.EquipamentoId != null && dto.EquipamentoId.Any())
            {
                var equipamentos = await _equipamentoRepository.ObterAsync(dto.EquipamentoId);
                if (equipamentos == null || !equipamentos.Any())
                {
                    AddNotification("CadastrarSala", MensagemValidacao.Equipamento.NaoExiste);
                    return;
                }

                sala.AdicionarSalaEquipamento(equipamentos.Select(e => new SalaEquipamento(sala.Id, e.Id)).ToList());
            }

            await _salaRepository.CriarAsync(sala);
        }

        public async Task EditarAsync(SalaDto dto)
        {
            if (dto.Id == null)
            {
                AddNotification("EditarSala", MensagemValidacao.CampoInvalido);
                return;
            }

            var salaEditar = await _salaRepository.ObterAsync(dto.Id.Value);
            if (salaEditar == null)
            {
                AddNotification("EditarSala", MensagemValidacao.Sala.NaoExiste);
                return;
            }

            var identificadorDuplicado = await _salaRepository.ObterAsync(dto.IdentificadorSala);
            if (identificadorDuplicado != null && identificadorDuplicado.Id != salaEditar.Id)
            {
                AddNotification("EditarSala", MensagemValidacao.Sala.IdentificadorJaExiste);
                return;
            }

            salaEditar.Editar(dto.Tipo, dto.IdentificadorSala, dto.Observacao, Convert.ToDecimal(dto.Area.Replace('.', ',')), dto.Andar, dto.CapacidadeAlunos, dto.Ativo);

            if (dto.EquipamentoId != null && dto.EquipamentoId.Any())
            {
                var equipamentos = await _equipamentoRepository.ObterAsync(dto.EquipamentoId);
                if (!equipamentos.Any())
                {
                    AddNotification("EditarSala", MensagemValidacao.Equipamento.NaoExiste);
                    return;
                }

                if (salaEditar.SalaEquipamentos != null && salaEditar.SalaEquipamentos.Any())
                {
                    await _salaRepository.DeletarSalaEquipamentosAsync(salaEditar.SalaEquipamentos);
                }

                await _salaRepository.CriarSalaEquipamentosAsync(equipamentos.Select(e => new SalaEquipamento(salaEditar.Id, e.Id)).ToList());
            }
            else if (salaEditar.SalaEquipamentos != null && salaEditar.SalaEquipamentos.Any())
            {
                await _salaRepository.DeletarSalaEquipamentosAsync(salaEditar.SalaEquipamentos);
            }

            await _salaRepository.EditatAsync(salaEditar);       
        }

        public async Task DeletarAsync(Guid id)
        {
            var sala = await _salaRepository.ObterAsync(id);
            if (sala == null)
            {
                AddNotification("ExcluirSala", MensagemValidacao.Sala.NaoExiste);
                return;
            }

            if (sala.Agendamentos != null && sala.Agendamentos.Any())
            {
                AddNotification("ExcluirSala", MensagemValidacao.Sala.PossuiAgendamento);
                return;
            }

            await _salaRepository.DeletarAsync(sala);
        }
    }
}

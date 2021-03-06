﻿using Flunt.Notifications;
using Flunt.Validations;
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
    public class AgendamentoService : Notifiable, IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        private readonly IEmailService _emailService;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository,
            IUsuarioService usuarioService,
            ISalaService salaService,
            IEmailService emailService)
        {
            _agendamentoRepository = agendamentoRepository;
            _usuarioService = usuarioService;
            _emailService = emailService;
            _salaService = salaService;
        }

        public IQueryable<Agendamento> Obter(string perfil, Guid usuarioId)
        {
            if (perfil == EnumTipoPerfil.Professor.ToString())
            {
                return _agendamentoRepository.ObterPorUsuario(usuarioId);
            }

            return _agendamentoRepository.Obter();
        }

        public IQueryable<Agendamento> Obter()
        {
            return _agendamentoRepository.Obter();
        }

        public async Task<Agendamento> ObterAsync(Guid id)
        {
            var agendamento = await _agendamentoRepository.ObterAsync(id);
            if (agendamento == null)
            {
                AddNotification("ObterAgendamento", MensagemValidacao.Agendamento.NaoExiste);
                return null;
            }

            return agendamento;
        }

        public async Task AprovarAgendamento(AgendamentoAprovacaoDto aprovacaoDto, Guid aprovadorId)
        {
            var agendamento = await _agendamentoRepository.ObterAsync(aprovacaoDto.AgendamentoId);
            var aprovador = await _usuarioService.Obter(aprovadorId);

            ValidarAprovadorAgendamento(agendamento, aprovador);
            if (Invalid)
            {
                return;
            }

            agendamento.AtualizarAgendamento(aprovador.Id, aprovacaoDto.Status, aprovacaoDto.Justificativa);

            await _agendamentoRepository.EditarAsync(agendamento);

            _emailService.AdicionarDestinatario(agendamento.Usuario.Email, agendamento.Usuario.Nome);
            await _emailService.EnviarEmailAprovacaoAgendamento(agendamento);
        }

        public void ValidarAprovadorAgendamento(Agendamento agendamento, Usuario aprovador)
        {
            var perfisAprovadores = new EnumTipoPerfil[]
            {
                EnumTipoPerfil.Administrador,
                EnumTipoPerfil.Diretor
            };

            AddNotifications(new Contract()
                .IsNotNull(agendamento, "AprovarAgendamento", MensagemValidacao.Agendamento.NaoExiste)
                .IsNotNull(aprovador, "AprovarAgendamento", MensagemValidacao.Usuario.NaoExiste)
                .IfNotNull(aprovador, x => x.IsTrue(perfisAprovadores.Contains(aprovador.Perfil), "AprovarAgendamento", MensagemValidacao.Agendamento.SemPermissao))
            );
        }

        public async Task CriarAsync(AgendamentoDto dto)
        {
            var sala = await _salaService.ObterAsync(dto.SalaId);
            var usuario = await _usuarioService.Obter(dto.UsuarioId);

            ValidarSalaUsuario(sala, usuario);
            if (Invalid)
            {
                return;
            }

            await ValidarAgendamentoDoUsuario(dto.Periodo, dto.DataAgendada, usuario.Id, "CadastrarAgendamento");
            if (Invalid)
            {
                return;
            }

            var agendamentoExiste = await _agendamentoRepository.ObterAsync(dto.SalaId, dto.Periodo, dto.DataAgendada);
            if (agendamentoExiste != null && (agendamentoExiste.Status == EnumStatusAgendamento.Aprovado || agendamentoExiste.Status == EnumStatusAgendamento.Pendente))
            {
                AddNotification("CadastrarAgendamento", MensagemValidacao.Agendamento.JaExiste);
                return;
            }

            var agendamento = new Agendamento(dto.DataAgendada, dto.Periodo, dto.SalaId, dto.UsuarioId);

            try
            {
                await _agendamentoRepository.CriarAsync(agendamento);
            }
            catch(Exception ex)
            {
                AddNotification("CadastrarAgendamento", MensagemValidacao.ContacteSuporte);
                return;
            }
            
        }

        public async Task ValidarAgendamentoDoUsuario(EnumPeriodo periodo, DateTime data, Guid usuarioId, string metodo)
        {
            var agendamento = await _agendamentoRepository.ObterAsync(periodo, data);
            if (agendamento != null && agendamento.UsuarioId == usuarioId && (agendamento.Status == EnumStatusAgendamento.Aprovado || agendamento.Status == EnumStatusAgendamento.Pendente))
            {
                AddNotification(metodo, MensagemValidacao.Agendamento.PermitidoUm);
                return;
            }
        }

        public void ValidarSalaUsuario(Sala sala, Usuario usuario)
        {
            AddNotifications(new Contract()
                .IsNotNull(sala, "CadastrarAgendamento", MensagemValidacao.Sala.NaoExiste)
                .IsNotNull(usuario, "CadastrarAgendamento", MensagemValidacao.Usuario.NaoExiste)
            );
        }

        public IQueryable<Agendamento> GerarRelatorio(DateTime dataInicio, DateTime dataFim)
        {
            return _agendamentoRepository.ObterPorData(dataInicio, dataFim);
        }

        public async Task EditarAsync(AgendamentoDto dto)
        {
            if (dto.Id == null)
            {
                AddNotification("EditarAgendamento", MensagemValidacao.CampoInvalido);
                return;
            }

            var sala = await _salaService.ObterAsync(dto.SalaId);
            var usuario = await _usuarioService.Obter(dto.UsuarioId);

            ValidarSalaUsuario(sala, usuario);
            if (Invalid)
            {
                return;
            }

            var agendamentoEditar = await _agendamentoRepository.ObterAsync(dto.Id.Value);
            if (agendamentoEditar == null)
            {
                AddNotification("EditarAgendamento", MensagemValidacao.Agendamento.NaoExiste);
                return;
            }

            await ValidarAgendamentoDoUsuario(dto.Periodo, dto.DataAgendada, usuario.Id, "EditarAgendamento");
            if (Invalid)
            {
                return;
            }

            var agendamentoDuplicado = await _agendamentoRepository.ObterAsync(dto.SalaId, dto.Periodo, dto.DataAgendada);
            if (agendamentoDuplicado != null && agendamentoEditar.UsuarioId != agendamentoDuplicado.UsuarioId && (agendamentoDuplicado.Status == EnumStatusAgendamento.Aprovado || agendamentoDuplicado.Status == EnumStatusAgendamento.Pendente))
            {
                AddNotification("EditarAgendamento", MensagemValidacao.Agendamento.JaExiste);
                return;
            }

            agendamentoEditar.Editar(dto.DataAgendada, dto.Periodo, dto.SalaId);
            agendamentoEditar.AtualizarAgendamento(Guid.Empty, dto.Status, null);

            await _agendamentoRepository.EditarAsync(agendamentoEditar);
        }

        public async Task Cancelar(Guid id, Guid usuarioId)
        {
            var agendamento = await _agendamentoRepository.ObterAsync(id);
            if (agendamento == null)
            {
                AddNotification("EditarAgendamento", MensagemValidacao.Agendamento.NaoExiste);
                return;
            }

            agendamento.AtualizarAgendamento(usuarioId, EnumStatusAgendamento.Cancelado);

            await _agendamentoRepository.EditarAsync(agendamento);
        }
    }
}

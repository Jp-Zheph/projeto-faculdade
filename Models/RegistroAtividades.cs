using System;
using System.ComponentModel.DataAnnotations;

namespace NewSIGASE.Models
{
    public class RegistroAtividades
    {
        [Key]
        public Guid Id { get; set; }
        public string Acao { get; set; }
        public string Entidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Detalhes { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }

        public RegistroAtividades(string acao,
            string entidade,
            string detalhes,
            Guid usuarioId)
        {
            Id = Guid.NewGuid();
            Acao = acao;
            Entidade = entidade;
            DataCriacao = DateTime.Now;
            Detalhes = detalhes;
            UsuarioId = usuarioId;
        }

        public RegistroAtividades()
        { }

        public void InserirCadastroEquipamento(Equipamento equipamento)
        {
            Acao = "Cadastro";
            Entidade = nameof(Equipamento);
            Detalhes = $"Data Criação: {FormatarData(equipamento.DataCriacao)}, Serial: {equipamento.Serial}, Modelo: {equipamento.Modelo}, " +
                       $"Nome: {equipamento.Nome}";
        }

        public void InserirCadastroSala(Sala sala)
        {
            Acao = "Cadastro";
            Entidade = nameof(Sala);
            Detalhes = $"Data Criação: {FormatarData(sala.DataCriacao)}, Tipo: {sala.Tipo.ToString()}, Identificador da Sala: {sala.IdentificadorSala}";
        }

        public void InserirCadastroUsuario(Usuario usuario)
        {
            Acao = "Cadastro";
            Entidade = nameof(Usuario);
            Detalhes = $"Data Criação: {FormatarData(usuario.DataCriacao)}, Matricula: {usuario.Matricula}, Nome: {usuario.Nome}, " +
                       $"Email: {usuario.Email}, Perfil: {usuario.Perfil.ToString()}";
        }

        public void InserirCadastroAgendamento(Agendamento agendamento)
        {
            Acao = "Cadastro";
            Entidade = nameof(Agendamento);
            Detalhes = $"Data Criação: {FormatarData(agendamento.DataCriacao)}, Sala: {agendamento.Sala.IdentificadorSala}, " +
                       $"Agendada Por: {agendamento.Usuario.Nome}({agendamento.Usuario.Matricula}), " +
                       $"Agendada Para: {FormatarData(agendamento.DataAgendada)} - {agendamento.Periodo.ToString()}, " +
                       $"Status: {agendamento.Status.ToString()}";
        }

        public void InserirAprovacaoAgendamento(Agendamento agendamento, Usuario aprovador)
        {
            Acao = "Aprovacao";
            Entidade = nameof(Agendamento);
            Detalhes = $"Data Criação: {FormatarData(agendamento.DataCriacao)}, Sala: {agendamento.Sala.IdentificadorSala}, " +
                       $"Agendada Por: {agendamento.Usuario.Nome}({agendamento.Usuario.Matricula}), " +
                       $"Agendada Para: {agendamento.DataAgendada} - {agendamento.Periodo.ToString()}, Status: {agendamento.Status.ToString()}" +
                       $"Aprovada Por: {aprovador.Nome}({aprovador.Matricula}), Data Aprovação: {FormatarData(agendamento.DataAtualizacaoStatus)}";
        }

        public static string FormatarData(DateTime? data)
        {
            if (data == null)
            {
                return String.Empty;
            }

            return data.Value.ToString("dd/MM/yyyy HH:mm");
        }
        
    }
}

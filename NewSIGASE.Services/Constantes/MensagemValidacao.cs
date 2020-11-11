
namespace NewSIGASE.Services.Constantes
{
    public static class MensagemValidacao
    {
        internal static readonly string CampoInvalido = "Campo Inválido";
        internal static readonly string CampoObrigatorio = "Campo Obrigatório";
        internal static readonly string ContacteSuporte = "Ocorreu um erro interno contacte o suporte do sistema.";

        internal static class Usuario
        {            
            internal static readonly string Inativo = "Usuário inativo.";
            internal static readonly string NaoExiste = "O usuário não existe.";
            internal static readonly string JaConfirmouCadastro = "O usuário já confirmou seu cadastro.";
            internal static readonly string SenhaDiferente = "Senha atual inválida.";
            internal static readonly string SenhaNaoPodeSerMatricula = "Senha não pode ser igual a matricula.";

            internal static string JaCadastrado(string campo) => string.Format("{0} já cadastrado.", campo);
        }

        internal static class Agendamento
        {
            internal static readonly string NaoExiste = "Agendamento não encontrado.";
            internal static readonly string PermitidoUm = "É permitido apenas um agendamento por Data e Período para o usuário solicitante.";
            internal static readonly string SemPermissao = "Usuário não tem permissão para executar essa funcionalidade.";
            internal static readonly string JaExiste = "A sala escolhida já está reservada para esse período e data. Favor escolher outro.";
        }

        internal static class Equipamento
        {
            internal static readonly string NaoExiste = "Equipamento não encontrado.";
            internal static readonly string SerialJaExiste = "Serial já cadastrado.";
            internal static readonly string PossuiSala = "Não é possivel excluir pois equipamento está vinculado a uma sala.";
            
        }

        internal static class Sala
        {
            internal static readonly string NaoExiste = "Sala não encontrada.";
            internal static readonly string SemPermissaoDesativar = "Não é possível inativar a sala, pois ela possui agendamentos futuros aprovados.";
            internal static readonly string IdentificadorJaExiste = "Identificador de Sala já cadastrado.";
            internal static readonly string PossuiAgendamento = "Não é possivel excluir pois a sala está vinculada a agendamentos.";            
        }

        internal static class Cep
        {
            internal static readonly string NaoEncontrado = "Endereço não encontrado.";
            internal static readonly string ErrorService = "Não foi possível consultar o cep. Favor preencher manualmente.";
        }

    }
}

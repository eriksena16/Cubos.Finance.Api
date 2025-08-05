namespace Cubos.Finance.Shared
{
    public static class CubosErrorMessages
    {
        #region PEOPLE
        public static string INVALID_DOCUMENT = "Documento inválido.";
        public static string INVALID_PASSWORD = "Senha inválida.";
        public static string USER_NOT_AUTHENTICATED = "Usuário não autenticado.";
        public static string USER_NOT_FOUND = "Usuário ou senha inválidos.";
        public static string PEOPLE_ALREADY_EXISTS = "Já existe uma pessoa com este documento.";
        #endregion

        #region ACCOUNT
        public static string ACCOUNT_ALREADY_EXISTS = "Já existe uma conta com este número.";
        public static string ACCOUNT_NOT_FOUND = "Conta não encontrada.";

        #endregion

        #region CARD
        public static string INVALID_CARD_NUMBER = "Número do cartão inválido.";
        public static string PHYSICAL_CARD_ALREADY_EXISTS = "Já existe um cartão físico vinculado a esta conta.";
        public static string CARD_ALREADY_EXISTS = "Já existe um cartão com este número."; 
        #endregion

        #region TRANSACTION
        public static string INVALID_TRANSACTION_VALUE = "O valor da transação deve ser diferente de zero.";
        #endregion

    }

}

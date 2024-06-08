namespace Autoglass.Infra.CrossCutting.Common.TratarErros
{
    public static class Mensagens
    {
        #region Methods
        public static string TratarErro(string mensagemErro)
        {
            var mensagemTratada = mensagemErro;
            if (mensagemErro.ToLower().Contains("user name") && mensagemErro.ToLower().Contains("is already taken"))
                mensagemTratada = "O e-mail informado já está sendo utilizado.";

            return mensagemTratada;
        } 
        #endregion
    }
}

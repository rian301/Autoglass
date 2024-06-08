using Autoglass.API.Admin.ViewModels;
using System;

namespace Autoglass.ViewModels
{
    public class ProdutoViewModel : BaseViewModel
    {
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJFornecedor { get; set; }
    }
}

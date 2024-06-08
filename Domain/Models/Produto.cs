using Autoglass.Domain.Core.Models;
using Dufry.Domain.Enums;
using FluentValidation;
using System;

namespace Domain.Models
{
    public class Produto : EntityLog<Produto, int>
    {
        #region Properties
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public SituacaoProduto Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJFornecedor { get; set; }
        #endregion

        #region Constructor
        protected Produto() { }

        public Produto(int codigoProduto, string descricao, SituacaoProduto situacao, DateTime dataFabricacao, DateTime dataValidade, int codigoFornecedor, string descricaoFornecedor, string cNPJFornecedor)
        {
            CodigoProduto = codigoProduto;
            Descricao = descricao;
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cNPJFornecedor;
        }

        #endregion

        #region Methods
        public CommandResult Update(int codigoProduto, string descricao, SituacaoProduto situacao, DateTime dataFabricacao, DateTime dataValidade, int codigoFornecedor, string descricaoFornecedor, string cNPJFornecedor)
        {
            CodigoProduto = codigoProduto;
            Descricao = descricao;
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cNPJFornecedor;

            return new CommandResult(true, "Produto atualizado com sucesso");
        }

        public void SetSituacalChange(int id) { Situacao = SituacaoProduto.Inativo; }

        #endregion

        #region Validators
        public override bool IsValid()
        {
            RuleFor(c => c.DataFabricacao)
                .NotEmpty()
                .Must(VerificaDataFabricacao)
                .WithMessage("A data de fabricação não pode ser maior que a data de validade.");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private bool VerificaDataFabricacao(DateTime data)
        {
            if(data >= DataValidade)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}

Passos para executar o projeto:

1 - Mudar a ConnectionStrings no arquivo appsettings.Development.json que fica na camada 1 - API => appsettings.json
2 - Abrir o Console de Gerenciador de Pacotes, mudar para o projeto 6 - Infra\6.1 - Data\Infra.Data e rodar o comando Update-Database


O projeto conta com o Swegger como documentação e ao fazer uma consulta paginada lembrar de passar o pageIndex inicial com o valor 0 (já está como default)

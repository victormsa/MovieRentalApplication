# MovieRentalApplication
 Movies rental application designed in Asp .NET Core 3.1 using MVC
 
 Aplicação de locação de filmes desenvolvida em Asp .NET Core 3.1 com MVC
 
## Migrations - Migrações
 Os arquivos de migração do banco de dados já estão contidos na aplicação, porém antes de executar as modificações na base de dados, deve-se modificar a [ConnectionString](https://github.com/victormsa/MovieRentalApplication/blob/master/RentalWorkPlease/appsettings.json) contida no projeto. Altere o atributo Server para o caminho do seu servidor SQL instalado.
 
```json
"ConnectionStrings": {
    "RentalWorkPleaseContext": "Server=[YourServer];Database=RentalWorkPlease;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AuthDbContextConnection": "Server=[YourServer];Database=RentalWorkPlease;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```
Para executar a migração usando Visual Studio 2019, vá em Tools>NuGet Package Manager>Package Manager Console e utilize os seguintes comandos:
```sh
EntityFrameworkCore\Update-Database -Context AuthDbContext
EntityFrameworkCore\Update-Database -Context RentalWorkPleaseContext
```
Aguarde o fim de cada comando e inicie a aplicação

## Sobre a aplicação
* Inicie cadastrando um usuário pelo link Register, em seguida realize o login.

Na tela seguinte a mensagem _Welcome_ e o cabeçalho com o email utilizado indicam que o usuário está logado.

Na barra de navegação superior encontram-se os links __Home__, __Movies__, __Genres__ e __Rentals__
* Idealmente, primeiro cadastre novos gêneros em __Genres__ 

teste editar e deletar um gênero, e preferencialmente, mantenha mais de um gênero cadastrado, para fins demonstrativos.

* Navegue para __Movies__ pela barra superior.

Adicione filmes e atribua a eles um ou mais gêneros, em seguida teste a edição e remoção, mantenha mais de um filme cadastrado.

* Navegue para __Rentals__ pela barra superior.

Adicione locações e atribua filmes, teste a edição e remoção. Se quiser, agora teste deletar gêneros, para observar o que acontece com o filmes cadastrados.
Faça o mesmo com os filmes e observe o resultado nas locações.

## Para implementar
A aplicação foi toda feita utilizado EntityFramework, podendo ainda ser implementada uma forma de manipulação do banco de dados híbirda, usando Dapper e EntityFramework, com Dapper para leitura e EF para escrita.

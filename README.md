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

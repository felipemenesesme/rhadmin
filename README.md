# RHAdmin

## Executando localmente

Para começar, basta clonar o repositório

alterar a string de conexão dentro de `Api/appsettings.json` em `ConnectionStrings.SqlServerConnection`

```bash
    # clone repo
    $ git clone https://github.com/felipemenesesme/rhadmin.git

    # abrir a pasta api do projeto
    $ cd api

    # criar migrations do banco
    $ dotnet ef migrations add initial
    # fazer update do banco
    $ dotnet ef database update
    # rodar back-end
    $ dotnet run

    # abra a pasta app do projeto
    $ cd app

    # instale as dependências
    $ npm install
    # ou
    $ yarn

    # execute a aplicação
    $ ng serve
```

Abra http://localhost:4200/ com seu navegador para ver o resultado.

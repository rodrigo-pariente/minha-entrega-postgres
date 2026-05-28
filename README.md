# Minha Entrega - Postgres

<p align="center">
  <img src="https://github.com/rodrigo-pariente/minha-entrega-postgres/blob/main/img/minha-entrega-logo.png" alt="Imagem da logo do Minha Entrega" width="250">
</p>
<p align="center"><b>Um sistema de gerenciamento de entregas dos correios a receber.</b></p>


## Como Usar

### API

Criar rastreio

`POST {"code": "codigo-de-rastreio", "name": "nome-pro-rastreio"} localhost:port/orders`

Renomear rastreio

`PUT {"name": "novo-nome"} localhost:port/orders/$code`

Ler os rastreios

`GET localhost:port/orders`

Ler um único rastreio

`GET localhost:port/orders/$code`

Ler os últimos eventos de cada rastreio

`GET localhost:port/orders/inspect`

Ler os últimos eventos de um único rastreio

`GET localhost:port/orders/$code/inspect`

Atualizar informações de todos os rastreios

`POST localhost:port/orders/refresh`

Atualizar informações de um único rastreio

`POST localhost:port/orders/$code/refresh`

Forçar a atualização até em rastreios já entregues

`POST localhost:port/orders/dumb_refresh`

Deletar um rastreio

`DELETE localhost:port/orders/$code`

<img src="https://github.com/rodrigo-pariente/minha-entrega-postgres/blob/main/img/screencapture_curl.gif" alt="Gif de uma requisição http" width="900">


### WebUI

O WebApp oferece uma interface gráfica intuitiva para interagir com o banco de dados através de requisições

<img src="https://github.com/rodrigo-pariente/minha-entrega-postgres/blob/main/img/screencapture_webui.gif" alt="Gif de uma requisição http" width="900">


### CLI

Criar rastreio

`$ minha-entrega add <code> <name>`

Renomear rastreio

`$ minha-entrega update <name> <new-name>`

Visualizar rastreios configurados

`$ minha-entrega list [name ...]`

<img src="https://github.com/rodrigo-pariente/minha-entrega-postgres/blob/main/img/screenshot_list.png" alt="Captura de tela da linda visualização de lista do Minha Entrega" width="900">

Visualizar as atualizações de um rastreio

`$ minha-entrega events <name>`

Visualizar a atualização mais recente dos rastreios

`$ minha-entrega inspect [name ...]`

<img src="https://github.com/rodrigo-pariente/minha-entrega-postgres/blob/main/img/screenshot_inspect.png" alt="Imagem de uma linda visualização das últimas informações de entrega" width="900">

Atualizar os eventos dos rastreios

`$ minha-entrega refresh [name ...]`

Remover um rastreio do banco de dados

`$ minha-entrega remove <name>`

Para mais informações de uso

`$ minha-entrega --help`


## Como Reproduzir

### Postgres

O container Postgres pode ser iniciado usando

`$ docker compose up`

de acordo com o arquivo compose `docker-compose.yml`.


### MinhaEntrega.Api

O servidor http foi escrito usando ASP.NET e para o banco de dados é dependente de:
- Microsoft.EntityFrameworkCore.Design
- Npgsql.EntityFrameworkCore.PostgreSQL

É necessário ter o Dotnet SDK para realizar o build.

Também é necessário inicializar o banco de dados

`$ dotnet ef database update`

Execute o servidor

`$ dotnet run`

Os dados de rastreio são obtidos através da API Site Rastreio.


### MinhaEntrega.WebApp

A WebUI é gerada com Razor e faz requisições para a API.

Pode ser subida com

`$ dotnet run`


### MinhaEntrega.Cli

A CLI foi escrita em Python usando Typer e Rich.

Teste a interface do usuário usando

`$ python3 MinhaEntrega.Cli --help`


## Motivação

Esse projeto foi realizado exclusivamente para aprender ASP.NET, Razor, Entity Framework Core, PostgreSQL e Docker através da prática.


<p align="center"> [ 📦 🐘 ]</p>

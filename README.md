# Sistema de Gerenciamento de Ativos de TI 

Este projeto é uma aplicação web desenvolvida para simular um ambiente corporativo real de controle de inventário de TI (Hardware/Software).

O objetivo é demonstrar competências em desenvolvimento Fullstack com foco no ecossistema .NET, aplicando boas práticas de arquitetura e modelagem de dados.

## Tecnologias Utilizadas

* **Back-end:** C# com .NET 10 (LTS) / ASP.NET Core MVC
* **Banco de Dados:** SQL Server / Entity Framework Core (Code-First)
* **Front-end:** Razor Views, HTML5, CSS3, Bootstrap
* **Ferramentas:** Visual Studio 2026 Community, Git

## Funcionalidades Atuais

- [x] Cadastro de Ativos (CRUD Completo)
- [x] Validação de dados (Data Annotations)
- [x] Uso de Enums para tipagem forte (Status e Tipo de Ativo)
- [ ] Dashboard com indicadores (Em desenvolvimento)
- [ ] Login e Autenticação (Planejado)

## Como rodar o projeto

1. Clone o repositório
2. Abra a solução no Visual Studio 2026
3. Atualize a ConnectionString no `appsettings.json` (se necessário)
4. Rode o comando `Update-Database` no Console do Gerenciador de Pacotes para criar o banco local
5. Execute o projeto (F5)

---
*Desenvolvido por Pedro Henrique como parte de portfólio prático de evolução técnica.*
# TechAsset Manager

Sistema corporativo para gerenciamento e controle de inventário de ativos de TI (Hardware e Periféricos). Desenvolvido como projeto de portfólio focando em boas práticas de Engenharia de Software, Arquitetura MVC e Rastreabilidade.

![Badge](https://img.shields.io/badge/Status-Versao%201.1%20%28Sprint%202%29-blue) ![.NET](https://img.shields.io/badge/.NET-ASP.NET_Core-purple)


## Screenshots

### Dashboard Gerencial
Visão geral com indicadores conectados ao banco de dados (`CountAsync`), oferecendo dados vivos sobre o inventário.
<img width="1852" height="910" alt="Image" src="https://github.com/user-attachments/assets/a1834ed1-9e73-4df7-848e-c1169ec4e3c6" />

### Listagem com Busca e Paginação
Controle de inventário com filtros avançados (Nome/Patrimônio + Status) e paginação no servidor.
<img width="1854" height="909" alt="Image" src="https://github.com/user-attachments/assets/85a0c441-584a-465a-9a57-42ceed979fc1" />

### Cadastro e Validação
Formulários com validação de dados e feedback visual em Português.
<img width="1830" height="918" alt="Image" src="https://github.com/user-attachments/assets/c5113455-5280-48ae-ac7d-f6688a56a808" />

### Detalhes e Rastreabilidade
Visão detalhada do ativo integrada ao log de auditoria, permitindo rastrear cronologicamente todas as alterações e mudanças de status.
<img width="1851" height="905" alt="Image" src="https://github.com/user-attachments/assets/167d1cc2-a398-46c3-832b-abbb6ac3e6ca" />

---

## Funcionalidades

### Gestão e Controle (Core)
- [x] **CRUD Completo:** Criação, Leitura, Atualização e Exclusão de ativos.
- [x] **Dashboard Conectado:** Painel gerencial alimentado em tempo real pelo banco de dados.
- [x] **Busca e Filtros:** Pesquisa textual (LINQ) combinada com filtros de Status (Enums).
- [x] **Paginação:** Otimização de performance no Back-end (*Server-side pagination*) para grandes volumes de dados.

### Segurança e Qualidade
- [x] **Auditoria de Dados:** Sistema de histórico automático que registra criações e edições para rastreabilidade (`1:N Relationship`).
- [x] **Validação Robusta:** Regras de negócio via *Data Annotations* e tratamento de erros defensivo.
- [x] **Identidade Visual:** Interface corporativa baseada em Bootstrap 5 com feedback visual de status.

---

## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** ASP.NET Core MVC (.NET 10 Preview – projeto de estudo)
- **Banco de Dados:** SQL Server / Entity Framework Core
- **Conceitos Aplicados:** Repository Pattern (Simulado), LINQ, Migrations, Dependency Injection.
- **Front-end:** Razor Views, Bootstrap 5, CSS Customizado
- **Controle de Versão:** Git & GitHub.

---

## Como Rodar o Projeto

1. **Clone o repositório:**

```bash
git clone https://github.com/drexsi0/TechAssetManager.git
```

2. **Configure o Banco de Dados:**
Certifique-se de ter o SQL Server (LocalDB) instalado. O Entity Framework aplicará as Migrations automaticamente ao rodar (Update-Database implícito ou via Package Manager Console).

3. **Execute a Aplicação:**
Abra a solução no Visual Studio e pressione `F5`.

---

Desenvolvido por **Pedro Henrique**, como parte de estudos avançados em Desenvolvimento Fullstack .NET.
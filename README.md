# MeuDiarioSenac

Aplicação de console em C# para registrar anotações pessoais (um "diário"), com cadastro/login por usuário — cada usuário só vê e edita os próprios registros.

Projeto acadêmico (Senac), construído em .NET 9 seguindo arquitetura **Model / View / Controller**, com persistência 100% via **Entity Framework Core** sobre **MySQL**.

## Tecnologias

- C# / .NET 9
- Entity Framework Core + [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- MySQL

## Arquitetura

O código é dividido em dois projetos:

```
MeuDiarioSenac/           # aplicação de console (apresentação)
├── Controllers/          # orquestram o fluxo: leem input, validam, chamam os Repositories
├── Views/                 # só entrada/saída no console (Console.Write/ReadLine)
└── Program.cs

MeuDiarioSenac.data/       # camada de dados (DAL)
├── Models/                # classes de dados puras (Usuario, Registro)
├── Repositories/          # acesso a dados via EF Core (sem SQL manual)
├── Migrations/            # migrations do EF Core
└── MeuDiarioSenacContext.cs
```

| Camada | Responsabilidade |
|---|---|
| **Model** | `Usuario`, `Registro` — só propriedades, sem lógica |
| **View** | `MenuView`, `AutenticacaoView`, `RegistroView` — só imprimem e leem do console |
| **Controller** | `MenuController`, `AutenticacaoController`, `RegistroController` — regra de fluxo e validação |
| **Repository (DAL)** | `UsuarioRepository`, `RegistroRepository` — consultas/gravações via `DbContext` (EF Core) |

## Funcionalidades

- Cadastro de conta (nome, e-mail, senha — com validação de formato)
- Login por e-mail/senha
- Criar registro (título + conteúdo, com data automática)
- Listar registros do usuário logado
- Alterar um registro existente

## Pré-requisitos

- [.NET SDK 9](https://dotnet.microsoft.com/download)
- MySQL Server rodando em `localhost`

## Configuração do banco de dados

A connection string está em `MeuDiarioSenac.data/MeuDiarioSenacContext.cs`:

```csharp
private static string connectionString = "Server=localhost;Database=sistema_registros;Uid=root;Pwd=1234;";
```

Ajuste usuário/senha/porta se o seu MySQL local for diferente.

**Banco novo (do zero):**

```bash
mysql -u root -p1234 -e "CREATE DATABASE sistema_registros;"
dotnet ef database update --project MeuDiarioSenac.data --startup-project MeuDiarioSenac
```

**Banco já existente, faltando só a tabela `registros`:** rode o script `criar_tabela_registros.sql` (na raiz do repositório) contra o banco `sistema_registros` — ele é idempotente (seguro rodar mesmo já tendo aplicado antes):

```bash
mysql -u root -p1234 sistema_registros < criar_tabela_registros.sql
```

## Como rodar

```bash
dotnet run --project MeuDiarioSenac
```

## Comandos úteis de migration (EF Core)

```bash
# ver migrations aplicadas/pendentes
dotnet ef migrations list --project MeuDiarioSenac.data --startup-project MeuDiarioSenac

# criar uma nova migration após alterar um Model
dotnet ef migrations add NomeDaMigration --project MeuDiarioSenac.data --startup-project MeuDiarioSenac

# aplicar migrations pendentes no banco
dotnet ef database update --project MeuDiarioSenac.data --startup-project MeuDiarioSenac
```

## Limitações conhecidas

- Senha é armazenada e comparada em texto puro (sem hash) — aceitável para fins didáticos, não recomendado em produção.
- Connection string fixa no código-fonte (não usa variável de ambiente/secrets).

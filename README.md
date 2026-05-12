# FIAP Cloud Games (FCG) 🎮

O **FIAP Cloud Games (FCG)** é uma plataforma robusta de venda de jogos digitais e gestão de servidores para partidas online. O projeto foi desenvolvido com foco em escalabilidade, segurança e testabilidade, seguindo princípios de arquitetura limpa.

## 🚀 Tecnologias Utilizadas

- **Runtime:** .NET 8 / C#
- **ORM:** Entity Framework (EF) Core
- **Database:** Supabase (PostgreSQL)
- **Segurança:** - Autenticação via **JWT** (JSON Web Token)
  - Hash de senhas com **BCrypt.Net**
- **Testes:** xUnit e Moq para Testes Unitários

## 🏗️ Arquitetura e Organização

O projeto utiliza uma divisão de responsabilidades clara entre as camadas:
- **Domain:** Entidades de negócio e regras fundamentais.
- **Application:** Casos de uso, interfaces de serviços e as **DTOs** (Inputs e Responses) para garantir que dados sensíveis não sejam expostos.
- **Infrastructure:** Implementação dos repositórios e contexto do banco de dados (Supabase).
- **HttpApi:** Controllers que expõem os endpoints e gerenciam o ciclo de vida das requisições.

## 🔒 Segurança e Níveis de Acesso

O sistema implementa autenticação via Token JWT com dois perfis (Roles):

1.  **Usuário (User):** Pode acessar a plataforma e gerenciar sua própria biblioteca de jogos.
2.  **Administrador (Admin):** Possui permissões totais para cadastrar jogos, administrar outros usuários e criar promoções.


## ⚙️ Configuração Local (User Secrets)

Para manter a segurança, informações sensíveis como a String de Conexão e a Chave do JWT não estão no `appsettings.json`. Utilize o **Secret Manager** do .NET para configurar o ambiente:

1. Na pasta do projeto `HttpApi`, execute os seguintes comandos:

# Configurar a string de conexão com o Supabase
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=db.xxx.supabase.co;Database=postgres;Username=postgres;Password=sua_senha"

# Configurar a chave secreta do JWT (mínimo 32 caracteres)
dotnet user-secrets set "Jwt:Key" "Sua_Chave_Super_Secreta_Com_Muitos_Caracteres"
dotnet user-secrets set "Jwt:Issuer" "FCG_API"
dotnet user-secrets set "Jwt:Audience" "FCG_Users"

## 📡 Endpoints da API

### Autenticação
- `POST /api/auth` - Realiza o login e retorna o token JWT.

### Jogos (Games)
- `GET /api/game/all` - Lista todos os jogos disponíveis.
- `GET /api/game/{id}` - Obtém detalhes de um jogo específico.
- `POST /api/game` 🛡️ - Cadastra um novo jogo.
- `PUT /api/game` 🛡️ - Atualiza informações de um jogo.
- `PATCH /api/game/{id}/promotion/{percentage}` 🛡️ - Aplica desconto em um jogo.
- `DELETE /api/game/{id}` 🛡️ - Remove um jogo da plataforma.

### Usuários (Users)
- `POST /api/user` - Cria uma nova conta na plataforma.
- `GET /api/user/{id}` 🛡️ - Detalhes do perfil.
- `GET /api/user/all` 🛡️ - Lista todos os usuários (Apenas Admin).
- `PUT /api/user` 🛡️ - Atualiza dados do perfil.
- `PATCH /api/user/{id}/game/{gameId}` 🛡️ - Adiciona um jogo à biblioteca do usuário.
- `PATCH /api/user/reset-password` 🛡️ - Altera a senha do usuário logado.
- `DELETE /api/user/{id}` 🛡️ - Remove um usuário.

> 🛡️ *Endpoints que exigem Token JWT no cabeçalho (Authorization: Bearer).*

## 🧪 Testes Unitários

A qualidade do código é garantida por uma suíte de testes unitários que cobre os principais serviços da aplicação (`GameService` e `UserService`), validando desde regras de negócio até o mapeamento correto de DTOs.

Para rodar os testes, utilize o comando:

```bash
dotnet test
# 📌 API Secretaria FIAP  

Este projeto é uma **Web API em .NET** desenvolvida como parte de um teste técnico.  
A API permite gerenciar turmas e alunos, com autenticação e acesso via Swagger.  

---

## 🚀 Tecnologias utilizadas
- [.NET 8]  
- C#  
- Entity Framework Core  
- SQL Server  
- Swagger (Swashbuckle)  

---

## ⚙️ Pré-requisitos

Antes de rodar o projeto, certifique-se de ter instalado:
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)  
- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)  

---

## 🗄️ Banco de Dados

Na raiz do projeto há o arquivo **`dump.sql`**, que contém:
- Criação do banco de dados `FIAPSecretaria`  
- Criação das tabelas necessárias (`Alunos`, `Turmas`, `Usuarios`, `Matriculas`)  
- Inserção de registros iniciais para teste  

### Como criar o banco:
1. Abra o **SQL Server Management Studio (SSMS)** ou **Azure Data Studio**  
2. Execute o script `dump.sql` em sua instância do SQL Server  
3. Confirme que o banco `FIAPSecretaria` foi criado corretamente  

---

## ▶️ Como rodar a aplicação

1. Clone este repositório:  
   Utilize a branch main para rodar a plicação.

## 🔑 Usuário inicial para autenticação

Um usuário administrador já é criado automaticamente pelo script **dump.sql**.  
A senha é armazenada de forma criptografada no banco, mas para login utilize as credenciais abaixo:  

```json
{
  "email": "admin@fiap.com",
  "senha": "admin@123"
}

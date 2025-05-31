
# 📡 API - Conexão Solidária (Mensagens de Emergência)

Conexão Solidária é uma plataforma de comunicação offline que utiliza redes mesh via Bluetooth Low Energy (BLE) para permitir a troca de mensagens mesmo sem internet ou rede móvel. 
Cada celular com o aplicativo instalado atua como um nó da rede, transmitindo mensagens entre dispositivos próximos até que um deles alcance conexão com a internet e sincronize os dados com a central. 

Essa arquitetura distribuída e resiliente garante que a comunidade possa continuar comunicando-se, organizando-se e pedindo ajuda, mesmo sob total desconexão. 


## 🧩 Diagrama Entidade-Relacionamento

```
Mensagem
---------
- Id (GUID) PK
- UUID (string)
- Titulo (string)
- Conteudo (string)
- Prioridade (string)
- Localizacao (string)
- DataEnvio (DateTime)
- TTL (int)
- Status (string)
- UsuarioId (string)
```

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8.0
- Razor Pages + TagHelpers
- Entity Framework Core 8
- Oracle.EntityFrameworkCore
- Oracle Database (via SQL Developer)
- Swagger / Swashbuckle
- Docker (Azure-ready)
- Visual Studio 2022

---

## 🛠 Como executar localmente

```bash
# Restaurar dependências
dotnet restore

# Criar a migration
dotnet ef migrations add InitialCreate

# Aplicar ao banco Oracle
dotnet ef database update

# Rodar o servidor
dotnet run
```

Acesse a documentação Swagger em:  
`http://localhost:<PORTA>/swagger`

---

## 🧪 Testes de Endpoints

### 1. POST /api/Mensagem

```json
{
  "titulo": "Alerta de enchente",
  "conteudo": "Nível do rio subiu rapidamente",
  "prioridade": "Alta",
  "localizacao": "Rua das Águas, 123",
  "usuarioId": "1"
}
```

✅ Retorna 201 Created com a mensagem gerada

---

### 2. GET /api/Mensagem/usuario/{usuarioId}

- Exemplo: `/api/Mensagem/usuario/1`

✅ Retorna todas as mensagens enviadas pelo usuário

---

### 3. PUT /api/Mensagem/{uuid}

(Coloque os campos que deseja atualizar)

```json
{
  "titulo": "Atualização",
  "conteudo": "Chuva intensa chegando",
  "prioridade": "Alta",
  "localizacao": "Rua Central, 999",
  "status": "Pendente",
  "usuarioId": "1"
}
```

✅ Retorna 204 No Content

---

### 4. DELETE /api/Mensagem/{uuid}

✅ Remove uma mensagem individual por UUID

---

### 5. DELETE /api/Mensagem/usuario/{usuarioId}

✅ Remove todas as mensagens de um usuário específico

---

## 🧾 Consultas no Oracle SQL

```sql
SELECT * FROM MENSAGENS;
```

---

## 🐳 Deploy com Docker (Azure-ready)

### Dockerfile já incluso no projeto

```bash
# Build da imagem
docker build -t conexao-solidaria-api .

# Executar localmente
docker run -p 5000:80 conexao-solidaria-api
```

---

## 👨‍💻 Autores
**Diego Furigo**  
📌 RM: 558755

**Melissa Pereira**  
📌 RM: 555656  

**Lu Vieira**  
📌 RM: 558935  

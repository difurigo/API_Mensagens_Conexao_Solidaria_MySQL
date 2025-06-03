
# 📡 API - Conexão Solidária (Mensagens de Emergência)

**Conexão Solidária** é uma plataforma de comunicação offline que utiliza redes mesh com Bluetooth Low Energy (BLE) para permitir a troca de mensagens em cenários de desastre, mesmo sem acesso à internet ou rede móvel.

Cada celular com o app instalado age como um nó na rede, retransmitindo mensagens entre dispositivos próximos até que uma conexão com a internet seja encontrada para sincronização com a central.  
Essa abordagem resiliente mantém a comunicação comunitária ativa mesmo sob blackout total.

---

## 🧩 Diagrama Entidade-Relacionamento (1:N)

```
Usuario
-------
- Id (string) PK
- Nome (string)
- Email (string)

Mensagem
--------
- Id (GUID) PK
- UUID (string)
- Titulo (string)
- Conteudo (string)
- Prioridade (string)
- Localizacao (string)
- DataEnvio (DateTime)
- TTL (int)
- Status (string)
- UsuarioId (string) FK → Usuario.Id
```

---

## 🚀 Tecnologias Utilizadas

- ✅ ASP.NET Core 8.0
- ✅ Razor Pages + TagHelpers
- ✅ Entity Framework Core 8 (com Migrations)
- ✅ Banco de Dados Oracle (Oracle.EntityFrameworkCore)
- ✅ Swagger (via Swashbuckle)
- ✅ Docker (Azure-ready)
- ✅ Visual Studio 2022

---

## 🛠 Como executar localmente

```bash
# Restaurar dependências
dotnet restore

# Aplicar migration
dotnet ef database update

# Rodar a API
dotnet run
```

📄 Documentação Swagger:
```
http://localhost:<PORTA>/swagger
```

---

## 🧪 Endpoints e Exemplos de Testes

### 📌 POST /api/Usuario

```json
{
  "id": "123",
  "nome": "Maria da Silva",
  "email": "maria@email.com"
}
```

✅ Cria novo usuário

---

### 📨 POST /api/Mensagem

```json
{
  "titulo": "Alerta de enchente",
  "conteudo": "Nível do rio subiu rapidamente",
  "prioridade": "Alta",
  "localizacao": "Rua das Águas, 123",
  "ttl": 5,
  "status": "Pendente",
  "usuarioId": "123"
}
```

✅ Cria nova mensagem vinculada a um usuário existente

---

### 🔍 GET /api/Mensagem/usuario/{usuarioId}

Retorna todas as mensagens de um usuário.

---

### ✏️ PUT /api/Mensagem/{uuid}

```json
{
  "titulo": "Atualização",
  "conteudo": "Chuva intensa chegando",
  "prioridade": "Alta",
  "localizacao": "Rua Central, 999",
  "status": "Pendente",
  "usuarioId": "123"
}
```

✅ Atualiza a mensagem com base no UUID

---

### ❌ DELETE /api/Mensagem/{uuid}

Remove uma mensagem específica por UUID.

---

### ❌ DELETE /api/Mensagem/usuario/{usuarioId}

Remove **todas** as mensagens do usuário.

---

## 🧾 Consulta rápida no Oracle SQL

```sql
SELECT * FROM MENSAGENS;
SELECT * FROM USUARIOS;
```

---

## 🐳 Deploy com Docker

```bash
# Build da imagem
docker build -t conexao-solidaria-api .

# Executar localmente
docker run -p 5000:80 conexao-solidaria-api
```

> O projeto está pronto para ser deployado no Azure com ajustes simples no `Dockerfile`.

---

## 📹 Vídeos obrigatórios para entrega

- 🎥 **Vídeo de Demonstração (até 8 min):**  
  Mostrar os principais endpoints funcionando via Swagger + código

- 🎙️ **Vídeo Pitch (até 3 min):**  
  Explicar a ideia, o problema resolvido e como o sistema funciona

---

## 👨‍💻 Autores

- **Diego Furigo** – RM: 558755  
- **Melissa Pereira** – RM: 555656  
- **Lu Vieira** – RM: 558935  


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

## 🧾 Consulta rápida no MySQL

```sql
SELECT * FROM MENSAGENS;
SELECT * FROM USUARIOS;
```

---

## 🐳 Deploy com Docker

# Passo a passo de Conteinerização da API .NET + MySQL (Conexão Solidária)

## ETAPA 1 — Subir o MySQL em container

```bash
docker run -d --name mysql-db \
    -e MYSQL_ROOT_PASSWORD=Fruba123 \
    -e MYSQL_DATABASE=conexao_solidaria_db \
    -e MYSQL_USER=fruba \
    -e MYSQL_PASSWORD=Fruba123 \
    -p 3306:3306 \
    -v mysql-volume:/var/lib/mysql \
    mysql:8.0
```

---

## ETAPA 2 — Testar conexão no MySQL no prompt

```bash
docker exec -it mysql-db mysql -u fruba -p
# Senha: Fruba123
```

### Dentro do MySQL:

```sql
SHOW DATABASES;
USE conexao_solidaria_db;
SHOW TABLES;
exit;
```

---

## ETAPA 3 — Build e run da API com Docker

```bash
cd "C:<CAMINHO_PARA_API>\api_gs_mensagens_conexao_solidaria"

docker build -t conexao-solidaria-api .

docker run -d --name conexao-solidaria-api \
    --link mysql-db:mysql-db \
    -p 8080:8080 \
    -e ASPNETCORE_ENVIRONMENT=Production \
    conexao-solidaria-api

docker ps
```

---

## ETAPA 4 — Testar via Swagger

Acesse:

```
http://localhost:8080/swagger
```

Se der erro:

```bash
docker logs conexao-solidaria-api
```

---

## 👨‍💻 Autores

- **Diego Furigo** – RM: 558755  
- **Melissa Pereira** – RM: 555656  
- **Lu Vieira** – RM: 558935  

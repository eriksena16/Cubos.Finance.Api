# Cubos.Finance.Api

git clone https://github.com/eriksena16/Cubos.Finance.Api.git

API para gerenciamento financeiro — **contas**, **cartões**, **pessoas**, **autenticação** e **transações**.

## 📌 Endpoints disponíveis

### 🔐 Auth
- `POST /api/v1/auth/login`  
  Autenticação de usuário (login).

### 🧾 Accounts
- `GET /api/v1/accounts`  
  Retorna a lista de contas bancárias.

- `POST /api/v1/accounts`  
  Cria uma nova conta bancária.

### 💳 Cards
- `GET /api/v1/accounts/{accountId}/cards`  
  Retorna os cartões associados a uma conta específica.

- `POST /api/v1/accounts/{accountId}/cards`  
  Adiciona um novo cartão à conta especificada.

- `GET /api/v1/cards`  
  Retorna todos os cartões (de todas as contas).

### 🧍 People
- `POST /api/v1/people`  
  Cria uma nova pessoa.

### 💰 Transactions
- `GET /api/v1/accounts/{accountId}/transactions`  
  Retorna as transações da conta especificada.

- `POST /api/v1/accounts/{accountId}/transactions`  
  Registra uma nova transação para a conta especificada.

- `POST /api/v1/accounts/{accountId}/transactions/internal`  
  Registra uma transação interna para a conta especificada.

---

## 🚀 Como usar

🐳 Via Docker Compose
Para executar o projeto com Docker:
- Certifique-se de ter o Docker Desktop instalado e em execução.
- Navegue até a pasta docker do projeto.
- Copie o arquivo .env que foi enviado por e-mail para dentro da pasta docker.
- Abra o terminal ou o CMD dentro da pasta docker e rode:

`docker-compose -f cubos-finance-api.yml up --build`

Isso criará e iniciará os containers definidos no arquivo cubos-finance-api.yml


Você pode usar a API fazendo requisições HTTP para os endpoints acima, com os métodos indicados.

### 📋 Acessando o endereco http://localhost:5005/swagger/index.html

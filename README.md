# Projeto RestauranteGerenciamento API

Este é um projeto de API REST para gerenciar **pedidos** e **reservas** em um sistema de restaurante. A API foi construída utilizando **ASP.NET Core** e é configurada para funcionar com armazenamento em memória (listas em memória), permitindo realizar operações de CRUD (Criar, Ler, Atualizar e Deletar) para pedidos e reservas.

## Funcionalidades

### 1. **Pedidos**
   - Criar pedido
   - Listar todos os pedidos
   - Obter pedido por ID
   - Atualizar pedido
   - Cancelar pedido
   - Excluir pedido

### 2. **Reservas**
   - Criar reserva
   - Listar todas as reservas
   - Obter reserva por ID
   - Atualizar reserva
   - Deletar reserva

## Tecnologias Usadas

- **ASP.NET Core**: Framework para construir APIs.
- **Swagger**: Ferramenta para gerar documentação interativa da API.
- **In-Memory Data Storage**: Uso de listas em memória para armazenar os dados, ideal para fins de desenvolvimento e testes.

## Como Rodar o Projeto

### Pré-requisitos

- **.NET 6.0 ou superior**: Certifique-se de ter o .NET SDK instalado. Você pode verificar a versão instalada com o comando:

    ```bash
    dotnet --version
    ```

### Passos para Execução

1. **Clone o repositório**:

    ```bash
    git clone [https://github.com/Marceloalvesll/GerenciamentoRestaurante.API.git]
    cd GerenciamentoRestaurante.API
    ```

2. **Executar a aplicação**:

    Para iniciar o servidor local, execute o comando:

    ```bash
    dotnet run
    ```

3. **Acessar a API**:

    Após rodar o projeto, você poderá acessar a documentação da API através do Swagger em:

    ```
    http://localhost:5000/swagger
    ```

## Endpoints

### **Pedidos**

#### Criar Pedido
- **Método**: `POST`
- **Rota**: `/api/pedidos`
- **Corpo da Requisição**:

    ```json
    {
      "itens": [
        { "nome": "Item 1", "preco": 10.00, "quantidade": 2 },
        { "nome": "Item 2", "preco": 5.00, "quantidade": 1 }
      ]
    }
    ```

- **Resposta**: Retorna o pedido criado com um `201 Created` e o ID gerado.

#### Listar Pedidos
- **Método**: `GET`
- **Rota**: `/api/pedidos`
- **Resposta**: Retorna todos os pedidos armazenados.

#### Obter Pedido por ID
- **Método**: `GET`
- **Rota**: `/api/pedidos/{id}`
- **Resposta**: Retorna o pedido correspondente ao `id` fornecido.

#### Atualizar Pedido
- **Método**: `PUT`
- **Rota**: `/api/pedidos/{id}`
- **Corpo da Requisição**:

    ```json
    {
      "dataHora": "2024-12-06T14:00:00Z",
      "confirmado": true,
      "cancelado": false,
      "itens": [
        { "id": "id_item", "nome": "Item 1", "preco": 10.00, "quantidade": 3 }
      ]
    }
    ```

- **Resposta**: Retorna o pedido atualizado.

#### Cancelar Pedido
- **Método**: `POST`
- **Rota**: `/api/pedidos/{id}/cancelar`
- **Resposta**: Retorna `204 No Content` se o pedido foi cancelado com sucesso.

#### Deletar Pedido
- **Método**: `DELETE`
- **Rota**: `/api/pedidos/{id}`
- **Resposta**: Retorna `204 No Content` se o pedido foi removido.

### **Reservas**

#### Criar Reserva
- **Método**: `POST`
- **Rota**: `/api/reservas`
- **Corpo da Requisição**:

    ```json
    {
      "numeroPessoas": 4,
      "mesaId": "id_mesa",
      "confirmada": false
    }
    ```

- **Resposta**: Retorna a reserva criada.

#### Listar Reservas
- **Método**: `GET`
- **Rota**: `/api/reservas`
- **Resposta**: Retorna todas as reservas.

#### Obter Reserva por ID
- **Método**: `GET`
- **Rota**: `/api/reservas/{id}`
- **Resposta**: Retorna a reserva correspondente ao `id` fornecido.

#### Atualizar Reserva
- **Método**: `PUT`
- **Rota**: `/api/reservas/{id}`
- **Corpo da Requisição**:

    ```json
    {
      "numeroPessoas": 3,
      "mesaId": "id_mesa",
      "confirmada": true
    }
    ```

- **Resposta**: Retorna `204 No Content` após atualização.

#### Deletar Reserva
- **Método**: `DELETE`
- **Rota**: `/api/reservas/{id}`
- **Resposta**: Retorna `204 No Content` após exclusão.

## Configuração do Swagger

O Swagger foi configurado para documentar todos os endpoints da API. Para visualizar a documentação da API:

1. Após rodar o projeto, abra o navegador e acesse a URL:

    ```
    http://localhost:5000/swagger
    ```

2. A documentação interativa permitirá explorar os endpoints, enviar requisições e visualizar as respostas.





Projeto desenvolvido por Marcelo Alves Leite


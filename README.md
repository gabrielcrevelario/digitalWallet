# Projeto Wallet API

## Visão Geral do Projeto
O projeto **Wallet API** foi desenvolvido com foco em uma arquitetura escalável, flexível e fácil de manter, utilizando princípios sólidos de design, padrões de projeto bem conhecidos e técnicas de **Domain-Driven Design (DDD)**. O objetivo foi construir uma API robusta que simule a gestão de transações e carteiras digitais.

Neste projeto, foram aplicados conceitos modernos de arquitetura de software, com ênfase particular em DDD, para garantir que as necessidades de negócio sejam atendidas de forma eficiente e clara.

## Princípios SOLID

### 1. **Single Responsibility Principle (SRP)**
O projeto segue o SRP com uma clara separação de responsabilidades:
- **Camadas bem definidas**: Cada camada da aplicação tem uma responsabilidade única:
  - **Domínio**: Define as entidades e lógicas de negócio.
  - **Aplicação**: Orquestra as operações de negócios sem se preocupar com persistência.
  - **Infraestrutura**: Concentra-se no acesso a dados e outras operações de baixo nível.

### 2. **Open/Closed Principle (OCP)**
A arquitetura do projeto permite que o sistema seja facilmente estendido sem modificar o código existente, por meio de:
- **Interfaces e abstrações**: Exemplos de interfaces como `ITransactionService` e `IWalletRepository`, permitindo que novas implementações sejam inseridas sem alterar a lógica central do sistema.

### 3. **Liskov Substitution Principle (LSP)**
As implementações das interfaces podem ser substituídas sem afetar o comportamento do sistema:
- **Exemplo**: A classe `TransactionService` pode ser substituída por outra implementação sem que o código que a consome precise ser modificado.

### 4. **Interface Segregation Principle (ISP)**
As interfaces são projetadas para serem específicas e coesas:
- **Exemplo**: Em vez de criar interfaces grandes e genéricas, usamos interfaces específicas como `ICreditTransaction` e `IDebitTransaction`, que segregam as responsabilidades de cada tipo de transação.

### 5. **Dependency Inversion Principle (DIP)**
A dependência é invertida, utilizando a injeção de dependências para desacoplar as camadas de alto e baixo nível:
- **Exemplo**: A camada de aplicação não depende diretamente de implementações de persistência, mas de interfaces (como `IWalletRepository`), permitindo que diferentes implementações de persistência possam ser trocadas facilmente.

## Domain-Driven Design (DDD)

O uso de **DDD** foi central para estruturar o projeto de forma a refletir as necessidades do negócio e ser fácil de entender à medida que o sistema cresce.

### 1. **Camada de Domínio**
A camada de **domínio** é o coração do projeto, contendo as entidades, agregados e serviços de domínio:
- **Entidades** como `Transaction`, `Wallet` e `User` representam o estado central do negócio.
- **Agregados** como `Wallet` e `Transaction` garantem a consistência dos dados.
- **Serviços de domínio** lidam com regras de negócio complexas que não pertencem diretamente a uma entidade.

### 2. **Camada de Aplicação**
A camada de **aplicação** orquestra a lógica de uso, sem envolver-se com a lógica de persistência, utilizando serviços de domínio e repositórios para cumprir os casos de uso da aplicação.

### 3. **Camada de Infraestrutura**
A camada de **infraestrutura** fornece os detalhes da implementação do banco de dados e outras dependências externas, como serviços de autenticação e sistemas de pagamento. A implementação do repositório para acessar dados, como `WalletRepository` e `TransactionRepository`, é feita aqui.

### 4. **Uso de Aggregates e Repositories**
O conceito de **Aggregates** foi empregado para garantir que a lógica de consistência e validação seja feita corretamente:
- **Wallet** é um agregado que gerencia transações e validações dentro de um contexto transacional.
- **TransactionRepository** foi criado para encapsular a persistência das transações, mantendo a consistência dos dados.

## Padrões de Projeto

### 1. **Padrão Repository**
O **padrão Repository** abstrai o acesso a dados, facilitando a interação com o banco de dados sem expor detalhes de implementação. Exemplos de repositórios no projeto incluem `IWalletRepository` e `ITransactionRepository`.

### 2. **Padrão Factory**
O **padrão Factory** é utilizado para criar instâncias de objetos de forma centralizada, permitindo que novos tipos de transações ou carteiras possam ser adicionados sem impactar o restante do código. A criação de objetos é delegada para fábricas como `TransactionFactory`.

### 3. **Padrão Dependency Injection**
A **injeção de dependências** desacopla as dependências entre as diferentes camadas. Serviços como `TransactionService` e `WalletService` são injetados na camada de aplicação sem precisar conhecer suas implementações concretas.

## Bibliotecas Externas

### 1. **Microsoft.EntityFrameworkCore**
Simplifica o acesso a dados e garante a persistência das entidades de forma eficiente, utilizando mapeamento objeto-relacional (ORM).

### 2. **Swashbuckle.AspNetCore (Swagger)**
O **Swagger** gera uma documentação interativa da API, tornando o desenvolvimento e testes mais fáceis. Com o Swagger, os desenvolvedores podem visualizar todos os endpoints disponíveis e realizar testes diretamente pela interface.

### 3. **AutoMapper**
O **AutoMapper** é utilizado para mapear objetos entre diferentes camadas, como de entidades para DTOs (Data Transfer Objects), evitando a duplicação de código de mapeamento manual.

### 4. **Microsoft.AspNetCore.Authentication.JwtBearer**
A autenticação JWT foi configurada para garantir que apenas usuários autenticados possam acessar endpoints protegidos. A segurança foi aplicada com o JWT Bearer Token, garantindo que os dados trocados entre o cliente e o servidor sejam seguros.

### 5. **Docker**
O uso de **Docker** e **Docker Compose** garante que o ambiente de desenvolvimento e produção seja consistente, facilitando a configuração e execução do projeto sem se preocupar com a configuração do ambiente local.

## Conclusão

Este projeto foi estruturado com os melhores princípios de design de software, utilizando **DDD** para manter o foco nas necessidades do negócio e **SOLID** para garantir que o sistema seja escalável, flexível e fácil de manter.

A escolha de padrões de projeto como **Repository** e **Factory**, além do uso de bibliotecas externas, ajudou a acelerar o desenvolvimento e melhorar a qualidade do código. Ao adotar essas práticas, conseguimos criar um sistema eficiente, de fácil manutenção e que pode ser facilmente estendido conforme as necessidades futuras.

---

## Requisitos

Para rodar este projeto, você precisa ter as seguintes ferramentas instaladas:

- [Docker](https://www.docker.com/products/docker-desktop) (certifique-se de ter o Docker e Docker Compose configurados corretamente no seu sistema).
- [Git](https://git-scm.com/) para clonar o repositório.
- [.NET SDK 8.0](https://dotnet.microsoft.com/download) (se quiser desenvolver ou testar localmente sem Docker).

## Estrutura do Projeto

- **wallet-api**: A API principal, onde a lógica de negócios é executada.
- **src**: Contém os projetos de dependências, incluindo as bibliotecas e a API.
  - **DigitalWallet.Domain**: Contém as entidades do domínio.
  - **DigitalWallet.Aplication**: Contém a camada de aplicação com a lógica de serviços.
  - **DigitalWallet.Infrastructure**: Contém a camada de infraestrutura, como acesso a banco de dados.
  
## Instruções de Uso

### 1. Clonar o Repositório

Clone o repositório em sua máquina local:

```bash
git clone https://github.com/gabrielcrevelario/digitalWallet
cd digitalWallet

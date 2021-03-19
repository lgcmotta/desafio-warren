# Desafio Warren

Desafio proposto pela Warren para uma vaga de desenvolvedor full stack

## Descrição

Implementar um sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos. Um ponto extra seria rentabilizar o dinheiro parado em conta de um dia para o outro como uma conta corrente remunerada.

## Environment

Para rodar esse projeto é necessário ter instalado

- **.NET 5 SDK**
- **Docker**
- **Node.js**

## Generate backend certificate

```
dotnet dev-certs https -ep $env:UserProfile\.aspnet\https\desafiowarren.pfx -p desafiowarren # PowerShell

dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\desafiowarren.pfx -p desafiowarren # cmd

dotnet dev-certs https -ep $HOME\.aspnet\https\desafiowarren.pfx -p desafiowarren # bash

dotnet dev-certs https --trust
```

## Start Backend Project

```
git clone https://github.com/luizmotta01/desafio-warren.git desafio-warren

cd .\desafio-warren\

docker-compose up -d # silent mode, to see containers logs remove the '-d' parameter
```

## Start Frontend Project

```
cd .\desafio-warren-client\

npm install

npm start
```

## Azure AD Credentials

Basta me pedir :)

# Requisitos

.NET 6.0 

MySQL

# Executando o projeto

## Git clone

```
git clone https://github.com/LosSantosBoys/PontoAPonto-api.git
```

## Configurações

No arquivo <b>appsettings.json</b> (PontoAPonto.Api -> appsettings.json), alterar as seguintes propriedades:

```
{
...
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONN_STRING" // Connection string do MySQL
  },
  "Jwt": {
    "Key": "YOUR_KEY", // Key aleatoria para JWT
    ...
  },
  "EncryptionKey": "YOUR_KEY", // Key aleatoria de criptografia
  "EmailConfig": {
    "Email": "YOUR_EMAIL", // Email para uso SMTP
    "Password": "YOUR_PASSWORD" // Senha do email para uso do SMTP
  }
}
```
Para uso da API no aplicativo Kotlin, se for utilizar emulador no Android Studio é necessário configurar proxy ou seu IPv4 (obtido executando o comando "ipconfig" no cmd) como url. 

No <b>launchSettings.json</b>, alterar:

```
"http": {
  ...
  "applicationUrl": "http://YOUR_URL:5000" // Alterar para IPv4 ou porta do proxy
}
```

## Executando a aplicação

Na pasta raiz do projeto:

```
  cd PontoAPonto.Api
  dotnet run
```

Agora em seu navegador, coloque a seguinte url (substituindo o YOUR_URL pela url inserida anteriormente)

```
http://YOUR_URL:5000/swagger/index.html
```

Se tudo deu certo, você verá a página do swagger e pode presseguir para o aplicativo:

https://github.com/LosSantosBoys/PontoAPonto





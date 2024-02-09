# BancoAPI
Para rodar a aplicação precisamos primeiro iniciar baixa e iniciar o banco de dados SQL Server, que foi banco escolhido para persistir os dados, faremos isso utilizando docker.

Abra o prompt de comando e navegue até a pasta da solution da aplicação

Após isso iremos baixar a imagem oficial do SQL Server isso será efetuado com o comando abaixo:
docker pull mcr.microsoft.com/mssql/server

Após isso, iremos iniciar a instância do SQL Server com o comando:
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Teste@123"   -p 1433:1433 --name sql1 --hostname sql1   -d   mcr.microsoft.com/mssql/server:2022-latest

Agora falta configurar devidamente o banco de dados para receber os dados da aplicação, para tanto iremos utilizar o script que está na pasta do projeto, utilizando o comando abaixo:
docker cp ScriptBanco.sql sql1:/ScriptBanco.sql

Agora basta rodar o comando a baixo para rodar o script no banco de dados:
docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Teste@123 -i ScriptBanco.sql
Pronto, agora o banco está devidamente configurado. Vamos partir para a aplicação, para isso devemos rodar o comando a baixo para construir a imagem do container:
docker build -t app:v0.0.1 .

Aguarde finalizar o build da imagem, e rode o seguinte comando para rodar a aplicação e expor ela nas portas 8080 e 4433 do localhost:
docker run -p 8080:80 -p 4433:443 --name api -d app:v0.0.1


Agora temos que conectar o banco e a aplicação na mesma rede com os seguintes comandos:
docker network create acessoaobanco
docker network connect acessoaobanco api
docker network connect acessoaobanco sql1


Agora basta acessar o endereço http://localhost:8080/swagger/index.html que será direcionado para o swagger da API

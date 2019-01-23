# Serviços disponíveis na núvem

As APIs disponíveis estão documentadas e disponível no padrão Swagger na página abaixo:

[Swagger UI](https://calculadoraapi-dev.azurewebsites.net/index.html) .

Conforme pode ser observado na documentação Swagger, os serviços estão disponíveis nos caminhos listados abaixo:

https://calculadoraapi-dev.azurewebsites.net/api/calculadora/calculajuros
<br>https://calculadoraapi-dev.azurewebsites.net/api/calculadora/ShowMeTheCode

# Testes Automatizados

## Testes Unitários Automatizados

Foram desenvolvidos testes unitários utilizando a framework MSTest e Live Unit Testing, obtendo cobertura de código de 87% de todas as linhas de código e 100% das linhas de código desenvolvidas.


![](https://lucaspsepe.visualstudio.com/826de35e-83a4-4c4c-9d53-3d860133fd8f/_apis/git/repositories/8bb9e253-c06b-4e5c-b47b-b5f17dc6932b/Items?path=%2F.attachments%2Fimage-46793487-cdea-4047-b57c-98ac59a414e4.png)

## Testes de Integração Automatizados

Está disponível no arquivo abaixo o projeto de testes desenvolvido em Postman, com os cenários de teste considerados necessários para abrangência de todas as regras de negócio e pontos de falha do sistema.

[Projeto de Testes de Integração](https://lucaspsepe.visualstudio.com/_git/CalculadoraPrj?path=%2FCalculator.postman_test_collection.json&version=GBmaster) 

![](https://lucaspsepe.visualstudio.com/826de35e-83a4-4c4c-9d53-3d860133fd8f/_apis/git/repositories/8bb9e253-c06b-4e5c-b47b-b5f17dc6932b/Items?path=%2F.attachments%2Fimage-ad073828-fdf4-401b-bdc6-bc197774e0cf.png)

# Docker

Segue abaixo o link para repositório no Docker Hub que disponibiliza uma imagem com a aplicação em Container Linux.

https://hub.docker.com/r/lucassepe/calculadoraweb

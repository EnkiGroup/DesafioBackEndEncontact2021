# Teste back-end enContact

Bem-vindo ao teste para desenvolvimento back-end na enContact.

## O teste

Criamos um pequeno projeto onde tentamos simular o que acontece no dia a dia.
Um código onde você poderá encontrar pontos com bugs, necessidade de refatoração, problemas que podem gerar má performance etc.
O projeto consiste em uma API simples para cadastro e consulta de contatos em uma agenda.

Neste teste você poderá mostrar suas habilidades em c# e dotnet, Orientação a objetos, lógica de programação, SQL, refatoração e também suas habilidades de debug para encontrar os problemas.

O foco deste teste é a garantia dos requisitos abaixo estejam funcionais na API:

- [X] Criar, editar, excluir e listar agendas.
- [X] Criar, editar, excluir e listar empresas.
- [X] Importar contatos a partir de um arquivo .csv
  - Fique a vontade para definir o leiaute do arquivo .csv
  - Caso dê erro na importação de um registro, não deve impactar a importação dos demais.
  - É obrigatório ter uma agenda vinculada ao contato.
  - No arquivo, se for informada uma empresa ao contato, ela deve existir previamente no sistema. Caso não seja informado, o contato é registrado sem vinculo com a empresa.
- [X] Pesquisar contatos
  - Deve pesquisar em qualquer campo do contato (incluído o nome da empresa).
  - A pesquisa deve ser paginada (Fique a vontade para utilizar qualquer estratégia).
- [X] Pesquisa de contatos da empresa
  - Poder consultar os contatos de uma agenda e que estejam em uma determinada empresa.

## O repositório

1. Faça o fork do nosso repositório no Github.
2. Clone do projeto.
3. Faça as devidas alterações para atender aos requisitos.
4. Caso seja necessário, enviar junto o arquivo "Database.db" que fica na raiz do projeto.

## O que fazer?

1. Crie/Edite as APIs necessárias para atender os requisitos.
2. Refatore o código da maneira que achar melhor.
3. Fique a vontade para usar qualquer biblioteca externa, caso seja necessário.

## Desafio do desafio

Tem um tempinho a mais? Acha que pode fazer mais? Então aqui vai alguns desafios para seu projeto, que serve como um plus no seu teste!

- Seria uma boa se pontos críticos do código tivessem testes unitários.
- Adicionar autenticação na API seria interessante.
- Poder exportar a agenda completa também seria legal.

##

## Alterações 

Alterações feitas: Mudança de código na string de consulta método GetAll de Conpany para Company que causava erro na consulta no banco de dados;
Inserção de código no médoto Delete que estava faltando no para o funcionamento esperado do mesmo;
Inserção de entidade Contact;
Alteração do método construtor em ContactBookDao;
Implementação de upload de arquivo para importação de contatos;
Refatoração de pastas do projeto;
Implementação de pesquisa paginada e pesquisa de contatos por agenda pelo nome;
Refatoração de trechos de códigos semelhantes.




 
# Locadora
Entity Framework (Code First) com Dapper

Antes de falar sobre o sistema, eu gostaria de explicar:
- A máquina usada para desenvolver a aplicação é muito (muito mesmo) antiga
- Configuração: Notebook c/ Intel Celeron de 1.5 Ghz com 2 Gb de memória (rodando Windows 10, Visual Studio Community 2017, SQL Server, navegador, etc...)
- Por isso a demora na entrega e falta de desenvolvimento de 1 item solicitado (exclusão de vários filmes de uma só vez - além de não ter dado tanto foco no layout)
- Pelo mesmo motivo, não consegui comentar muito o código dando mais ênfase no desenvolvimento, já que a máquina passava mais tempo travada do que me permitindo desenvolver
- Com um pouco mais de tempo (e uma máquina melhor), seria possível fazer uma especificação técnica diagramada e organizada (documentação)

Conforme especificado:
- criar um usuário na área de segurança do SQL Server (no meu caso EXPRESS - criei com o nome "base" e senha "12345678" com permissão de criação de base "dbcreator")
- adicionar este usuário na string de conexão (web.config)
- o resto o EF Code First faz

Link da aplicação na nuvem Microsoft (Azure): http://defes.azurewebsites.net/ (Usuário: admin / Senha: admin)

Testei nos navegadores Firefox, Chrome e IE/Edge

Responsivo (teste no Moto G5 Plus e Samsung J7 Pro)


Obrigado pela oportunidade!

Tiago Mantello

# Scrap_News

Em resumo é um projeto para fins de estudo onde eu capturo notícias de vários sites.

## Estrutura do Projeto

Este workspace contém os seguintes projetos:

### Scrap_News

- **Descrição**: Projeto principal que captura notícias de vários sites.
- **Localização**: [Scrap_News.csproj](Scrap_News.csproj)
- **Dependências**:
  - HtmlAgilityPack (v1.11.42)
  - Selenium.Support (v4.1.0)
  - Selenium.WebDriver (v4.1.0)
  - Selenium.WebDriver.ChromeDriver (v100.0.4896.6000)
  - System.Data.SQLite (v1.0.115.5)
- **Arquivos Principais**:
  - [Program.cs](Program.cs): Contém o ponto de entrada do aplicativo.
  - [Scrap_News.sln](Scrap_News.sln): Arquivo de solução do Visual Studio.

### Scrap_olhar_digital

- **Descrição**: Projeto específico para capturar notícias do site Olhar Digital.
- **Localização**: [Scrap_olhar_digital.csproj](Scrap_olhar_digital.csproj.nuget.dgspec.json)
- **Dependências**: As mesmas do projeto principal, pois compartilham o mesmo ambiente de execução.

## Como Executar

1. **Clone o repositório**:
	```sh
	git clone <URL_DO_REPOSITORIO>
	cd Scrap_News
	```

2. **Restaure as dependências**:
	```sh
	dotnet restore
	```

3. **Compile o projeto**:
	```sh
	dotnet build
	```

4. **Execute o projeto**:
	```sh
	dotnet run --project Scrap_News
	```

## Estrutura de Pastas

- `.vs/`: Configurações específicas do Visual Studio.
- `bin/`: Saída de compilação.
- `obj/`: Arquivos temporários de build.
- `Scrap_News.csproj`: Arquivo de projeto do .NET.
- `Scrap_News.sln`: Arquivo de solução do Visual Studio.
- `Program.cs`: Ponto de entrada do aplicativo.
- `README.md`: Este arquivo.

## Contribuição

Sinta-se à vontade para contribuir com este projeto. Para isso, siga os passos abaixo:

1. Faça um fork do projeto.
2. Crie uma nova branch (`git checkout -b feature/nova-feature`).
3. Faça commit das suas alterações (`git commit -am 'Adiciona nova feature'`).
4. Faça push para a branch (`git push origin feature/nova-feature`).
5. Crie um novo Pull Request.

## Licença

Este projeto é licenciado sob a [MIT License](LICENSE).

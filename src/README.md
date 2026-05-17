🎮 EcoGame - Instruções de Setup
Para rodar este projeto, todos os membros precisam seguir estas etapas exatamente na ordem:

1. 🛠️ Ferramentas Necessárias
Godot .NET: Baixe a versão "Godot Engine - .NET Edition" (A versão Standard NÃO roda C#).

Download Godot -> https://godotengine.org/pt-br/

.NET SDK: Instale o .NET SDK (versão 6.0 ou 8.0) da Microsoft. Sem isso, o código não compila.

Download .NET SDK -> https://dotnet.microsoft.com/en-us/download

Editor: No VS Code, instale a extensão "C# Dev Kit".

🚀 Como abrir e rodar pela primeira vez
No Godot, clique em Import e selecione o arquivo src/ecogame/project.godot.

PASSO CRUCIAL: Assim que o Godot abrir, clique no ícone do Martelo (Build) no canto superior direito.

Por que? Isso vai criar os arquivos necessários para o VS Code reconhecer o projeto.

Agora você pode abrir o código clicando em qualquer arquivo .cs.

⚠️ Regra de Ouro: O Ritual do GitHub
O Godot usa arquivos .uid para rastrear os scripts. Se você subir um .cs sem o seu .uid, o projeto dos colegas vai quebrar.

Antes de dar Commit/Push: 1. Salve tudo no VS Code.
2. Abra a janela do Godot e espere 2 segundos (ele vai gerar os UIDs automaticamente).
3. Só agora faça o seu Commit.

✅ Checklist de Sucesso (Seu código está funcionando se...):
[ ] O terminal do VS Code diz Build Succeeded ao rodar dotnet build.

[ ] Não existem erros vermelhos na aba "Output" do Godot.

[ ] Você não deletou nenhum arquivo .uid.
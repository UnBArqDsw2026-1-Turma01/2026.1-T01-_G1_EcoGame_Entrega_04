# EcoGame — Código Fonte

## Como abrir no Godot

1. Abra o Godot 4.6
2. Clique em **Import** e selecione a pasta `src/ecogame/`
3. O projeto vai abrir com a cena **TesteBuilder** como principal

## Cenas disponíveis

| Cena | Localização | O que faz |
|------|-------------|-----------|
| `TesteBuilder.tscn` | `scenes/reciclagem/` | Roda os testes da Frente A (Builder + Strategy) |
| `DemoFrenteC.tscn` | `scenes/loja/` | Demo da Frente C (Factory + Template Method + Proxy) |
| `DemoFrenteD.tscn` | `scenes/mapa/` | Demo da Frente D (Object Pool + Observer + Facade) |
| `sala_teste_movimento.tscn` | `scenes/testes/player/` | Teste de movimentação do Player (Frente B) |

## Trocar a cena principal

Project → Project Settings → Application → Run → Main Scene

## Estrutura MVC

```
src/ecogame/
├── models/       ← entidades de domínio (Item, Lixo, Player, Loja...)
├── views/        ← scripts de UI (InventarioView, MapaView...)
├── controllers/  ← lógica de coordenação (MapaController, LojaController...)
├── core/
│   └── autoloads/  ← FachadaInventarioJogador (singleton global)
├── scenes/       ← cenas .tscn + scripts de demo
└── assets/       ← sprites, audio, testes
```

# Guia do Desenvolvedor — EcoGame


## Onde plugar seu código

### `models/` — Entidades de domínio

**O que vai aqui:** classes que representam dados e regras de negócio puras. Sem UI, sem lógica de cena.

**Exemplos:** `Item`, `Lixo`, `Player`, `Loja`, `ReciclagemItem`

**Regra:** um Model não referencia nada de `views/` nem de `controllers/`. Ele só conhece a si mesmo e outros Models.


---

### `views/` — Scripts de UI

**O que vai aqui:** qualquer script que lê dados e os exibe na tela. Views não tomam decisões de negócio.

**Exemplos:** `InventarioView`, `MapaView`, `LojaView`, `ReciclagemView`

**Regra:** uma View recebe dados já processados (do Controller ou do Model) e só os renderiza. Ela emite sinais para avisar que o jogador agiu — nunca processa a ação diretamente.


---

### `controllers/` — Lógica de coordenação

**O que vai aqui:** lógica que conecta View e Model. Reage a eventos, atualiza Models, instrui Views a se redesenharem.

**Exemplos:** `MapaController`, `LojaController`, `ReciclagemController`, `PlayerController`

**Regra:** um Controller conhece tanto a View quanto o Model, mas não renderiza nada diretamente. Ele orquestra.


---


### `scenes/` — Cenas `.tscn` + scripts de demo

**O que vai aqui:** arquivos `.tscn` do Godot e scripts de demonstração/prototipagem que montam uma cena usando Models, Views e Controllers juntos.

**Exemplos:** `mapa_demo.tscn`, `loja_demo.tscn`, cenas de teste rápido **Verifiquem se seu codigo tem um arquivo .tscn**

**Regra:** scripts em `scenes/` podem instanciar e conectar as três camadas, mas não devem conter lógica de negócio nova — isso volta para `models/` ou `controllers/`.

---

### `assets/` — Sprites, áudio e testes

**O que vai aqui:** recursos estáticos (imagens, sons, fontes).

**Regra:** nenhum script de produção deve viver aqui. Se você está testando algo, crie o arquivo de teste em `assets/tests/` com o sufixo `_test.gd`.

---

## Fluxo de dados resumido

```
Jogador age na tela
       ↓
    View emite sinal
       ↓
 Controller recebe
       ↓
 Controller atualiza Model
       ↓
 Controller instrui View a redesenhar
```

---

## Adicionando uma frente nova (exemplo: Reciclagem)

1. Crie `models/reciclagem_item.gd` com os dados e regras da reciclagem.
2. Crie `views/reciclagem_view.gd` com os nós de UI e sinais de interação.
3. Crie `controllers/reciclagem_controller.gd` conectando os dois.
4. Crie a cena em `scenes/reciclagem.tscn` e anexe o Controller como script raiz.



## Compilar no VSCode

O Godot gera o build automaticamente ao salvar. Se precisar forçar:
```
dotnet build src/ecogame/Ecogame.csproj

```


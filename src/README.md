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

## Compilar no VSCode

O Godot gera o build automaticamente ao salvar. Se precisar forçar:
```
dotnet build src/ecogame/Ecogame.csproj
```

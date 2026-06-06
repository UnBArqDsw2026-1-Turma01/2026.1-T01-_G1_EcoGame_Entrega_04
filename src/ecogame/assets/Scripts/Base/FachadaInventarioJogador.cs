using Godot;
using System;

namespace EcoGame;

public partial class FachadaInventarioJogador : Node
{
    private static FachadaInventarioJogador _instancia;
    private Inventario _inventario;
    private Player _player;
    private bool _conectado = false;

    public static FachadaInventarioJogador Instancia
    {
        get
        {
            if (_instancia == null)
            {
                var tree = Engine.GetMainLoop() as SceneTree;
                if (tree != null)
                {
                    _instancia = new FachadaInventarioJogador();
                    tree.Root.AddChild(_instancia);
                }
            }
            return _instancia;
        }
    }

    public override void _Ready()
    {
        _instancia = this;
        _inventario = new Inventario();
    }

    public override void _Process(double delta)
    {
        if (_conectado) return;

        var p = Player.GetInstancia();
        if (p == null) return;

        _player = p;

        // Tenta conectar ao evento C# gerado pelo atributo [Signal]
        try
        {
            _player.ItemColetado += OnPlayerItemColetado;
        }
        catch (Exception)
        {
                try
                {
                    _player.Connect(Player.SignalName.ItemColetado, new Callable(this, nameof(OnPlayerItemColetado)));
                }
            catch (Exception)
            {
                GD.PrintErr("[FACHADA] Falha ao conectar ao sinal ItemColetado do Player.");
            }
        }

        _conectado = true;
        GD.Print("[FACHADA] Conectado ao Player e inicializado inventário.");
    }

    private void OnPlayerItemColetado(string nomeItem)
    {
        bool sucesso = _inventario.AdicionarItem(nomeItem);
        if (sucesso)
            GD.Print($"[FACHADA] '{nomeItem}' adicionado ao inventário.");
        else
            GD.Print($"[FACHADA] Falha ao adicionar '{nomeItem}' — inventário cheio.");
    }

    public bool AdicionarItemNoInventario(string nomeItem)
    {
        return _inventario.AdicionarItem(nomeItem);
    }

    public SlotInventario[] GetSlots()
    {
        return _inventario.GetSlots();
    }

    public Player GetPlayer()
    {
        return Player.GetInstancia();
    }

    public void AdicionarMoedasAoPlayer(int quantidade)
    {
        var p = Player.GetInstancia();
        if (p != null)
            p.SetMoedas(p.GetMoedas() + quantidade);
    }

    public void AplicarBuffVelocidadeAoPlayer(float duracao, int novaVelocidade)
    {
        var p = Player.GetInstancia();
        p?.AplicarBuffVelocidade(duracao, novaVelocidade);
    }

    // Método auxiliar: tenta adicionar ao inventário e notifica o jogador (emite sinal)
    public void ColetarItemERegistrar(string nomeItem)
    {
        bool sucesso = _inventario.AdicionarItem(nomeItem);
        if (sucesso)
        {
            GD.Print($"[FACHADA] Coleta: '{nomeItem}' adicionada ao inventário.");
            Player.GetInstancia()?.ColetarItem(nomeItem);
        }
        else
        {
            GD.Print($"[FACHADA] Coleta falhou: inventário cheio para '{nomeItem}'.");
        }
    }
}

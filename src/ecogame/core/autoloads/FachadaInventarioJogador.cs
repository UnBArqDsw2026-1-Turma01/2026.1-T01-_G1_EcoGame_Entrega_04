using Godot;
using System;
using System.Collections.Generic;

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
        try { _player.ItemColetado += OnPlayerItemColetado; }
        catch (Exception)
        {
            try { _player.Connect(Player.SignalName.ItemColetado, new Callable(this, nameof(OnPlayerItemColetado))); }
            catch (Exception) { GD.PrintErr("[FACHADA] Falha ao conectar ao sinal ItemColetado."); }
        }
        _conectado = true;
        GD.Print("[FACHADA] Conectado ao Player.");
    }

    private void OnPlayerItemColetado(string nomeItem)
    {
        var itemTemp = new Lixo(nomeItem, 1, MaterialBase.OUTROS);
        _inventario.AdicionarItem(itemTemp);
        GD.Print($"[FACHADA] '{nomeItem}' adicionado ao inventário.");
    }

    public bool AdicionarItemNoInventario(string nomeItem)
    {
        var itemTemp = new Lixo(nomeItem, 1, MaterialBase.OUTROS);
        _inventario.AdicionarItem(itemTemp);
        return true;
    }

    public List<SlotInventario> GetSlots() => _inventario.GetSlots();
    public Player GetPlayer() => Player.GetInstancia();

    public void AdicionarMoedasAoPlayer(int quantidade)
    {
        var p = Player.GetInstancia();
        if (p != null) p.SetMoedas(p.GetMoedas() + quantidade);
    }

    public void AplicarBuffVelocidadeAoPlayer(float duracao, int novaVelocidade)
    {
        Player.GetInstancia()?.AplicarBuffVelocidade(duracao, novaVelocidade);
    }

    public void ColetarItemERegistrar(string nomeItem)
    {
        var itemTemp = new Lixo(nomeItem, 1, MaterialBase.OUTROS);
        _inventario.AdicionarItem(itemTemp);
        Player.GetInstancia()?.ColetarItem(nomeItem);
        GD.Print($"[FACHADA] Coleta: '{nomeItem}' adicionada.");
    }
}
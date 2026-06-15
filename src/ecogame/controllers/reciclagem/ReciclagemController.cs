using Godot;
using System.Collections.Generic;

namespace EcoGame;

// ReciclagemController é o Controller MVC da reciclagem.
// Recebe inputs da cena (quais receitas usar, quais itens alimentar)
// e coordena a MaquinaReciclagem (Model) com a ReciclagemView.
public partial class ReciclagemController : Node
{
    private MaquinaReciclagem _maquina;
    private ReciclagemView _view;

    public override void _Ready()
    {
        _maquina = new MaquinaReciclagem();
        _view = GetNodeOrNull<ReciclagemView>("../ReciclagemView");

        // Registra as receitas disponíveis
        _maquina.AdicionarReceita(new PlasticoBuilder());
        _maquina.AdicionarReceita(new MetalBuilder());
        _maquina.AdicionarReceita(new VidroBuilder());
        _maquina.AdicionarReceita(new OrganicoBuilder());
        _maquina.AdicionarReceita(new BaldeBuilder());
        _maquina.AdicionarReceita(new EnxadaBuilder());
        _maquina.AdicionarReceita(new FoiceBuilder());
        _maquina.AdicionarReceita(new MachadoBuilder());
        _maquina.AdicionarReceita(new PicaretaBuilder());
        _maquina.AdicionarReceita(new RegadorBuilder());
    }

    // Chamado quando o jogador deposita um item na máquina
    public void AlimentarMaquina(Item item)
    {
        _maquina.AlimentarMaquina(item);
        _view?.AtualizarPlacar();
    }

    public int GetTotalPontos() => _maquina.GetTotalPontos();
    public string GetEstrategiaAtual() => _maquina.GetNomeEstrategiaAtual();
}

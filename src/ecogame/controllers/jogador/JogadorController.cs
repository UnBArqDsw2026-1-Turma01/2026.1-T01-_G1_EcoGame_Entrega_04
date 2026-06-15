using System.Collections.Generic;
using Godot;

namespace EcoGame;

// JogadorController é o Controller MVC do jogador.
// Recebe eventos de gameplay (coleta, compra, buff) e delega
// para a FachadaInventarioJogador, mantendo o Player como Model puro.
public partial class JogadorController : Node
{
    private FachadaInventarioJogador _fachada;

    public override void _Ready()
    {
        _fachada = FachadaInventarioJogador.Instancia;
    }

    // Chamado quando o jogador interage com um item no mapa
    public void OnColetarItem(string nomeItem)
    {
        _fachada.ColetarItemERegistrar(nomeItem);
    }

    // Chamado pela loja ao comprar
    public void OnReceberMoedas(int quantidade)
    {
        _fachada.AdicionarMoedasAoPlayer(quantidade);
    }

    // Chamado ao consumir CafeItem
    public void OnAplicarBuffVelocidade(float duracao, int novaVelocidade)
    {
        _fachada.AplicarBuffVelocidadeAoPlayer(duracao, novaVelocidade);
    }

    // Retorna os slots para a View do inventário renderizar
    public List<SlotInventario> GetSlotsParaView()
    {
        return _fachada.GetSlots();
    }
}

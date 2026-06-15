using Godot;

namespace EcoGame;

// LojaController é o Controller MVC da loja.
// Usa as Factories (FLojaFerramentas, FLojaSementes) para criar lojas
// e o Proxy (VerificadorCompra) para intermediar compras/vendas.
public partial class LojaController : Node
{
    private FachadaInventarioJogador _fachada;
    private LojaFerramentas _lojaFerramentas;
    private LojaSementes _lojaSementes;
    private VerificadorCompra _proxy;

    public override void _Ready()
    {
        _fachada = FachadaInventarioJogador.Instancia;

        _lojaFerramentas = new FLojaFerramentas().criarLoja("Loja de Ferramentas", true);
        _lojaSementes    = new FLojaSementes().criarLoja("Loja de Sementes", true);
        _proxy = new VerificadorCompra(_lojaFerramentas);
    }

    public void AbrirLojaFerramentas() => _proxy.setTarget(_lojaFerramentas);
    public void AbrirLojaSementes()    => _proxy.setTarget(_lojaSementes);

    public void ComprarItem(Item item) => _proxy.templateComprarItem(item);
    public void VenderItem(Item item)  => _proxy.templateVenderItem(item);

    public string GetLojaAtual() => _proxy.getTargetName();
}

using Godot;

namespace EcoGame;

// Lixo atua como ConcreteObserver (GoF Observer) e Reusable (Object Pool).
// Implementa a reação ao ciclo dia/noite e os hooks de reuso para o LixoPool.
// Nota: Lixo herda de Item (POCO), não de Node — sem _Ready() ou GetNode().
public partial class Lixo : Item, ISunObserver
{
    private bool _estaBrilhando = false;

    public Lixo(string nome, int quantidade, MaterialBase material)
        : base(nome, quantidade, material) { }

    public void OnSunrise()
    {
        _estaBrilhando = true;
        GD.Print($"[Lixo:{GetNome()}] Amanheceu — Ativando brilho visual.");
    }

    public void OnSunset()
    {
        _estaBrilhando = false;
        GD.Print($"[Lixo:{GetNome()}] Anoiteceu — Desativando brilho visual.");
    }

    public bool GetEstaBrilhando() => _estaBrilhando;

    public new void Reconfigurar(string nome, int quantidade, MaterialBase material)
    {
        base.Reconfigurar(nome, quantidade, material);
        _estaBrilhando = false;
    }

    public void Reset()
    {
        base.Reconfigurar("lixo_pool", 0, MaterialBase.OUTROS);
        _estaBrilhando = false;
        GD.Print($"[Lixo] Instância resetada e pronta para reuso no Pool.");
    }
}

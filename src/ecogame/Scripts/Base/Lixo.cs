using Godot;

namespace EcoGame;

// Lixo atua como ConcreteObserver (GoF Observer) e Reusable (Object Pool).
// Implementa a reação ao ciclo dia/noite e os hooks de reuso para o LixoPool.
public partial class Lixo : Item, ISunObserver
{
    private bool _estaBrilhando = false;

    // Referências para componentes visuais
    private Sprite2D _sprite;
    private AnimationPlayer _anim;

    public Lixo(string nome, int quantidade, MaterialBase material)
        : base(nome, quantidade, material) { }

    public override void _Ready()
    {
        // Inicialização de componentes para o efeito visual de brilho
        _sprite = GetNodeOrNull<Sprite2D>("Sprite2D");
        _anim = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
    }

    // --- Implementação do padrão Observer ---

    public void OnSunrise()
    {
        _estaBrilhando = true;
        GD.Print($"[Lixo:{GetNome()}] Amanheceu — Ativando brilho visual.");

        // Ativa o feedback visual
        if (_anim != null && _anim.HasAnimation("brilhar_dia"))
            _anim.Play("brilhar_dia");
    }

    public void OnSunset()
    {
        _estaBrilhando = false;
        GD.Print($"[Lixo:{GetNome()}] Anoiteceu — Desativando brilho visual.");

        // Desativa o feedback visual
        if (_anim != null)
            _anim.Stop();
    }

    public bool GetEstaBrilhando() => _estaBrilhando;

    // --- Implementação do padrão Object Pool ---

    /// <summary>
    /// Reconfigura a instância reciclada. 
    /// É 'public new' para que o LixoPool possa acessá-lo, enquanto na classe
    /// mãe (Item) o método permanece 'protected' para encapsulamento.
    /// </summary>
    public new void Reconfigurar(string nome, int quantidade, MaterialBase material)
    {
        // Chama o hook protected da classe Item
        base.Reconfigurar(nome, quantidade, material);

        // Garante que o estado local comece limpo
        _estaBrilhando = false;
        if (_anim != null) _anim.Stop();
    }

    /// <summary>
    /// Retorna o objeto ao estado neutro antes de ser devolvido ao pool,
    /// evitando o "defeito clássico de pool"
    /// </summary>
    public void Reset()
    {
        // Reseta atributos base usando valores default do pool
        base.Reconfigurar("lixo_pool", 0, MaterialBase.OUTROS);

        _estaBrilhando = false;
        if (_anim != null) _anim.Stop();

        GD.Print($"[Lixo] Instância resetada e pronta para reuso no Pool.");
    }
}

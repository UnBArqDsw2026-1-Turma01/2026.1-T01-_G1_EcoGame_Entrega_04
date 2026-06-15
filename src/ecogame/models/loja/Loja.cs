using System;
using System.Collections.Generic;
using Godot;
namespace EcoGame;

// look into this class declaration later. This might need some more keyword specification...

public abstract class ILoja // had to change this from an interface, else we'd have a big compile error.
{
    public abstract void templateVenderItem(Item item);
    public abstract void templateComprarItem(Item item);

}

public class VerificadorCompra(LojaAbstrata loja) : ILoja //this is the proxy. Instance a class, call this guy and be done. 
{
    // todo -> define LojaAbstrata later...
    private LojaAbstrata target = loja;

    public void setTarget(LojaAbstrata loja)
    {
        this.target = loja;
    }

    public string getTargetName()
    {
        return target.getNome();
    }
    // para executar esses aqui, espera-se acesso à classe jogador como um global.
    private bool verificarProcessoCompra(Item item)
    {
        if (target.getEstado() && target.ItensDisponiveis.Contains(item)) return true;
        GD.Print("Loja Fechada :0");
        return true; //alterar com base em necessidades de projeto. -> necessita acoplamento com jogador e verificação de inventário.
    }
    private bool verificarProcessoVenda(Item item)
    {
        if (target.getEstado()) return true; //store should at the very least be open and stuff. alter as needed.
        GD.Print("Loja Fechada :0");
        return false;
    }
    public override void templateVenderItem(Item item)
    {
        if (verificarProcessoCompra(item))
            target.templateVenderItem(item);
    }
    public override void templateComprarItem(Item item)
    {
        if (verificarProcessoVenda(item))
            target.templateComprarItem(item);
    }

}
public abstract class LojaAbstrata : ILoja
{
    private string nome;
    private bool estado;
    public List<Item> ItensDisponiveis { get; set; } //had to set this to public. Many headaches with keywords would proceed if not public. 
    public List<Item> ItensVendidos { get; set; }
    public string getNome() => nome;
    public void setEstado(bool novoestado)
    {
        GD.Print("state set to " + novoestado + " sucessfully...");
        estado = novoestado;
    }

    public bool getEstado()
    {
        return estado;
    }


    public LojaAbstrata(string nome, bool estado)
    {
        this.nome = nome;
        this.estado = estado;
        ItensDisponiveis = [];
        ItensVendidos = [];
    }

    public abstract void venderItem(Item item);
    public abstract void comprarItem(Item item);
    public override sealed void templateVenderItem(Item item)
    {

        // at this moment, not much preparation for buy or sell, but this already leaves a straight framework
        // on where to add or remove stuff from process and who and what will be called.
        // quite atomic design!
        venderItem(item);
    }
    public override sealed void templateComprarItem(Item item)
    {

        // same things said on tvi applies here.
        comprarItem(item);
    }

}
public class LojaSementes : LojaAbstrata
{
    public LojaSementes(string nome, bool estado) : base(nome, estado)
    {
        this.ItensDisponiveis = base.ItensDisponiveis;
        this.ItensVendidos = base.ItensVendidos;
    }
    public override void venderItem(Item item)
    {
        GD.Print("Não aceitamos vendas de sementes :>\n");
    }
    public override void comprarItem(Item item)
    {
        if (ItensDisponiveis.Contains(item))
        {
            ItensDisponiveis.RemoveAt(
                ItensDisponiveis.BinarySearch(item)
            );
            //todo -> logica de manipulacao do inventário
            GD.Print("Item Vendido :)\n");
        }
        else
        {
            GD.Print("Item fora de estoque :(\n");
        }
    }
}
public class LojaFerramentas : LojaAbstrata
{
    public LojaFerramentas(string nome, bool estado) : base(nome, estado) { }

    public override void venderItem(Item item)
    {
        if (!ItensVendidos.Contains(item))
        {
            GD.Print("oooohhhh, I'll buy it at a high price :)\n");
            ItensVendidos.Add(item);
            //todo -> logica para apagar o item do inventario...
            //Jogador.inventario.removeItem(item) // algo do genero!
        }
        else
        {
            GD.Print("Já temos muitas dessas...");
        }
    }
    public override void comprarItem(Item item)
    {
        GD.Print("Ferramentas são itens raríssimos :>! Faça sua própria...\n");
    }

}


public abstract class FLoja
{
    protected LojaAbstrata loja;

    public abstract LojaAbstrata criarLoja(string nome, bool estado);

}

public class FLojaSementes : FLoja
{

    public override LojaSementes criarLoja(string nome, bool estado)
    {
        base.loja = new LojaSementes(nome, estado);
        return (LojaSementes)base.loja;
    }
}
public class FLojaFerramentas : FLoja
{
    // podemos inclusive fazer um overload no nome criarLoja. Agora temos criarLoja aridade 0
    public LojaFerramentas criarLoja()
    {
        base.loja = new LojaFerramentas("Nome padrao 123", true);
        return (LojaFerramentas)base.loja;
    }
    //e criarLoja/2 -> o verdadeiro override, com aridade 2!
    public override LojaFerramentas criarLoja(string nome, bool estado)
    {
        base.loja = new LojaFerramentas(nome, estado);
        return (LojaFerramentas)base.loja;
    }
}





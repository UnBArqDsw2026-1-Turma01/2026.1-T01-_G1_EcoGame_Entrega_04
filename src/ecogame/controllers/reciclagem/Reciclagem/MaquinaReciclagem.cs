using System.Collections.Generic;
using Godot;

namespace EcoGame;

public partial class MaquinaReciclagem : Node
{
    private List<ReceitaBuilder> _receitasDisponiveis = new List<ReceitaBuilder>();
    
    // Injeção da Calculadora (Contexto do Strategy)
    // A Máquina é o "Client" que usa a calculadora para pontuar os resultados
    private CalculadoraPontuacao _calculadora = new CalculadoraPontuacao();

    public void AdicionarReceita(ReceitaBuilder novaReceita)
    {
        _receitasDisponiveis.Add(novaReceita);
    }

    public void AlimentarMaquina(Item itemRecebido) 
    {
        if (itemRecebido is BaldeComposite balde) 
        {
            GD.Print("[Maquina] Balde detectado! Extraindo conteúdo...");
            List<Item> itensInternos = balde.ExtrairConteudo(); 

            foreach (var subItem in itensInternos) 
            {
                DistribuirParaBuilders(subItem); 
            }
            GD.Print("Balde descarregado na máquina!");
        } 
        else 
        {
            DistribuirParaBuilders(itemRecebido);
        }
    }

    private void DistribuirParaBuilders(Item item)
    {
        foreach (var receita in _receitasDisponiveis)
        {
            if (receita.EhCompativel(item))
            {
                receita.AdicionarIngrediente(item);
               GD.Print($"{item.GetNome()} adicionado à receita!");
            }
        }
    }

    public Item ReciclarItem(ReceitaBuilder r)
    {
        if (r.ValidarIngredientes())
        {
            // 1. O Builder constrói o novo item transformado
            Item novoItem = r.Construir();
            
            // 2. O Strategy calcula a pontuação desse novo item
            // Note: A Maquina não sabe qual regra (Base, Raridade ou Combo) está ativa.
            // Ela apenas delega para o Contexto.
            int pontosGanhos = _calculadora.CalcularEAcumular(novoItem);

            GD.Print($"[SUCESSO] {novoItem.GetNome()} criado! +{pontosGanhos} pontos acumulados.");
            GD.Print($"[PROGRESSÃO] Total: {_calculadora.GetPontosAcumulados()} | Estratégia: {_calculadora.GetEstrategiaAtiva()}");
            
            return novoItem;
        }

        GD.Print("Puts! Você não tem ingredientes suficientes para a receita! :(");
        return null;
    }

    // Métodos para facilitar o acesso da UI aos dados da calculadora
    public int GetTotalPontos() => _calculadora.GetPontosAcumulados();
    public string GetNomeEstrategiaAtual() => _calculadora.GetEstrategiaAtiva();
}
using Godot;
using System.Collections.Generic;

namespace EcoGame;

public partial class TesteBuilder : Node
{
    public override void _Ready()
    {
        GD.Print("--- INICIANDO DEMONSTRAÇÃO DO PADRÃO BUILDER (EcoGame) --- \n");

        // 1. Instanciar o Diretor (A Máquina)
        var maquina = new MaquinaReciclagem();

        // ---------------------------------------------------------
        // PASSO 1: SUCESSO - Tier 1 (Lixo -> Reciclado)
        // ---------------------------------------------------------
        GD.Print(">> PASSO 1: Criando Bloco de Plástico (3 garrafas PET necessárias!)");
        
        var plasticoBuilder = new PlasticoBuilder();
        var lixoPlastico = new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET); 

        plasticoBuilder.AdicionarIngrediente(lixoPlastico);
        Item blocoPlastico = maquina.ReciclarItem(plasticoBuilder);

        if (blocoPlastico != null)
        {
            GD.Print($"[SUCESSO] Criado: {blocoPlastico.GetNome()} | XP: {blocoPlastico.GetPontos()}\n");
        }

        // ---------------------------------------------------------
        // PASSO 2: FALHA INTENCIONAL - Mostrando a proteção do Builder
        // ---------------------------------------------------------
        GD.Print(">> PASSO 2: Tentando criar Bloco de Plástico com ingredientes INSUFICIENTES");
        
        var plasticoBuilderFalha = new PlasticoBuilder();
        var lixoInsuficiente = new Lixo("Garrafa PET de Água", 1, MaterialBase.GARRAFA_PET); 
        
        plasticoBuilderFalha.AdicionarIngrediente(lixoInsuficiente);
        
        // ERRO: Dispara o GD.Print("Puts!...") na MaquinaReciclagem
        Item blocoFalha = maquina.ReciclarItem(plasticoBuilderFalha); 
        
        if (blocoFalha == null)
        {
            GD.Print("[COMPORTAMENTO ESPERADO] O Builder barrou a construção do item incompleto!\n");
        }

        // ---------------------------------------------------------
        // PASSO 3: SUCESSO - Tier 2 (Reciclados -> Ferramenta)
        // ---------------------------------------------------------
        GD.Print(">> PASSO 3: Criando Ferramenta (Regador) a partir de Materiais Reciclados");

        var regadorBuilder = new RegadorBuilder();

        // RECEITA ATUALIZADA: 1 Metal e 2 Plásticos
        var ingredienteMetal = new Reciclado("Bloco de Metal", 1, MaterialBase.METAL, 15);
        var ingredientePlastico = new Reciclado("Bloco de Plástico", 2, MaterialBase.GARRAFA_PET, 10);

        regadorBuilder.AdicionarIngrediente(ingredienteMetal);
        regadorBuilder.AdicionarIngrediente(ingredientePlastico);

        Item regador = maquina.ReciclarItem(regadorBuilder);

        if (regador != null)
        {
            GD.Print($"[SUCESSO] Criado: {regador.GetNome()} | Material Principal: {regador.GetMaterial()} | XP: {regador.GetPontos()}");
        }
        
        GD.Print("\n--- FIM DA DEMONSTRAÇÃO ---");
    }
}
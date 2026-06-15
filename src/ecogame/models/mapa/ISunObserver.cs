namespace EcoGame;

// Contrato do Observer no padrão GoF Observer.
// No EcoGame: o Mapa (Subject) chama OnSunrise/OnSunset em cada entidade
// registrada quando o ciclo dia/noite avança. Lixo implementa esta interface
// para ativar/desativar o brilho ao amanhecer/anoitecer.
public interface ISunObserver
{
    void OnSunrise();
    void OnSunset();
}

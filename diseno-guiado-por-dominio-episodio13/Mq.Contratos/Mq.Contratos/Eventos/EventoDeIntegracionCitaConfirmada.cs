using KernelCompartido;

namespace Mq.Contratos.Eventos;

public class EventoDeIntegracionCitaConfirmada : EventoDeIntegracionBase
{
    public EventoDeIntegracionCitaConfirmada()
    {
        Fecha = DateTimeOffset.UtcNow;
    }

    public EventoDeIntegracionCitaConfirmada(DateTimeOffset fechaDeEvento)
    {
        Fecha = fechaDeEvento;
    }

    public string TipoDeEvento => nameof(EventoDeIntegracionCitaConfirmada);
    public Guid CitaId { get; set; }
}
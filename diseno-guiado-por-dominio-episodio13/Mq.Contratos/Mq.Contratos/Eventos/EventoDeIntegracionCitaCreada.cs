using KernelCompartido;
namespace Mq.Contratos.Eventos;

public class EventoDeIntegracionCitaCreada : EventoDeIntegracionBase
{
    public EventoDeIntegracionCitaCreada()
    {
        //La fecha del evento
        Fecha = DateTime.UtcNow;
    }

    public string TipoDeEvento => nameof(EventoDeIntegracionCitaCreada);
    public string Tienda { get; set; }
    public Guid CitaId { get; set; }
    public string Cliente { get; set; }
    public string Peluquero { get; set; }
    public string TipoDeCita { get; set; }
    public DateTime FechaDeLaCita { get; set; }
    public int DuracionEnMinutos { get; set; }
}


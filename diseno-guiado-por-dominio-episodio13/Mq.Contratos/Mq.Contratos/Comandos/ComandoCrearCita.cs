namespace Mq.Contratos.Comandos;
public class ComandoCrearCita
{
    public Guid TiendaId { get; set; }
    public Guid CitaId { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PeluqueroId { get; set; }
    public Guid TipoDeCitaId { get; set; }
    public DateTime Fecha { get; set; }
    public int DuracionEnMinutos { get; set; }
}

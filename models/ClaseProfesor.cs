using System;

public class ClaseProfesor
{
    public int ClaseProfesorId { get; set; }
    public int ProfesorId { get; set; }
    public int SalonId { get; set; }
    public int GrupoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    public Profesor Profesor { get; set; }
    public Salon Salon { get; set; }
    public Grupo Grupo { get; set; }

    public ClaseProfesor()
    {
    }

    public ClaseProfesor(int profesorId, int salonId, int grupoId, DateTime fechaInicio, DateTime fechaFin)
    {
        ProfesorId = profesorId;
        SalonId = salonId;
        GrupoId = grupoId;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }
}

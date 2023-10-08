using System.Collections.Generic;

public class Salon
{
    public int SalonId { get; set; }
    public string NombreSalon { get; set; }
    public List<ClaseProfesor> ClasesProfesor { get; set; }

    public Salon()
    {
        ClasesProfesor = new List<ClaseProfesor>();
    }

    public Salon(string nombreSalon)
    {
        NombreSalon = nombreSalon;
        ClasesProfesor = new List<ClaseProfesor>();
    }
}

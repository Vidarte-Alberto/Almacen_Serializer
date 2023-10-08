using System;
using System.Text;
using System.IO;

public class Profesor
{
    public int ProfesorId { get; set; }
    public string NombreCompleto { get; set; }
    public string NominaEncriptada { get; set; }
    public string PasswordEncriptada { get; set; }
    public string[] MateriasQueImparte { get; set; }
    public string Division { get; set; }
    public List<ClaseProfesor> ClasesProfesor { get; set; }

    public Profesor()
    {
        NombreCompleto = string.Empty;
        NominaEncriptada = string.Empty;
        PasswordEncriptada = string.Empty;
        MateriasQueImparte = new string[0];
        Division = string.Empty;
        ClasesProfesor = new List<ClaseProfesor>();
    }


    // Constructor para la clase Profesor
    public Profesor(string nombreCompleto, string nomina, string password, string[] materias, string division)
    {
        NombreCompleto = nombreCompleto;
        NominaEncriptada = EncriptacionHelper.Encriptar(nomina);
        PasswordEncriptada = EncriptacionHelper.Encriptar(password);
        MateriasQueImparte = materias;
        Division = division;
        ClasesProfesor = new List<ClaseProfesor>();
    }
}

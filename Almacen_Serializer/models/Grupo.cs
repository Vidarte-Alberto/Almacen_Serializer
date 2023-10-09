using System.Collections.Generic;

public class Grupo
{
    public int GrupoId { get; set; }
    public string NombreGrupo { get; set; }
    public List<ClaseProfesor> ClasesProfesor { get; set; }

    public Grupo()
    {
        NombreGrupo = string.Empty;
        ClasesProfesor = new List<ClaseProfesor>();
    }

    public Grupo(string nombreGrupo)
    {
        NombreGrupo = nombreGrupo;
        ClasesProfesor = new List<ClaseProfesor>();
    }
}

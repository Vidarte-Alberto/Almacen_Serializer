using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ReporteProfesorJSON
{
    private List<Profesor> profesores;

    private const string fileName = "profesores.json";

    public ReporteProfesorJSON()
    {
        // Inicializa la lista de profesores desde el archivo JSON si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            profesores = JsonConvert.DeserializeObject<List<Profesor>>(json) ?? new List<Profesor>();
        }
        else
        {
            profesores = new List<Profesor>();
        }
    }

    public void GenerarReportePorNombreAscendente()
    {
        var profesoresPorNombreAscendente = profesores.OrderBy(p => p.NombreCompleto).ToList();
        GuardarReporteEnJSON(profesoresPorNombreAscendente, "reporte.json", false);
    }

    public void GenerarReportePorNominaAscendente()
    {
        // Desencriptar la nómina de cada profesor antes de ordenar
        var profesoresPorNominaAscendente = profesores.Select(p =>
        {
            p.NominaEncriptada = EncriptacionHelper.Desencriptar(p.NominaEncriptada);
            return p;
        }).OrderBy(p => ExtractNumeroFromNomina(p.NominaEncriptada, "NOMI")).ToList();
        GuardarReporteEnJSON(profesoresPorNominaAscendente, "reporte.json", true);
    }

    public void GenerarReportePorDivisionAscendente()
    {
        var profesoresPorDivisionAscendente = profesores.OrderBy(p => p.Division).ToList();
        GuardarReporteEnJSON(profesoresPorDivisionAscendente, "reporte.json", false);
    }

    public void GenerarReportePorMateriasAscendente()
    {
        var profesoresPorMateriasAscendente = profesores.OrderBy(p => string.Join(",", p.MateriasQueImparte)).ToList();
        GuardarReporteEnJSON(profesoresPorMateriasAscendente, "reporte.json", false);
    }

    public void GenerarReportePorIDAscendente()
    {
        var profesoresPorIDAscendente = profesores.OrderBy(p => p.ProfesorId).ToList();
        GuardarReporteEnJSON(profesoresPorIDAscendente, "reporte.json", false);
    }

    private void GuardarReporteEnJSON(List<Profesor> listaProfesores, string nombreArchivo, bool encript)
    {
        if (listaProfesores.Any())
        {
            if (encript == false)
            {
                listaProfesores.ForEach(p =>
                {
                    p.NominaEncriptada = EncriptacionHelper.Desencriptar(p.NominaEncriptada);
                    p.PasswordEncriptada = EncriptacionHelper.Desencriptar(p.PasswordEncriptada);
                });
            }

            string jsonReporte = JsonConvert.SerializeObject(listaProfesores, Formatting.Indented);

            // Guarda el JSON en el archivo especificado
            File.WriteAllText(nombreArchivo, jsonReporte);

            Console.WriteLine($"El reporte se ha guardado en el archivo {nombreArchivo}.");
        }
        else
        {
            Console.WriteLine("No se encontraron resultados para el reporte.");
        }
    }

    private int ExtractNumeroFromNomina(string nomina, string formato)
    {
        string numeroStr = nomina.Replace(formato, "");
        if (int.TryParse(numeroStr, out int numero))
        {
            return numero;
        }
        return 0; // Valor predeterminado si no se puede analizar el número
    }
}

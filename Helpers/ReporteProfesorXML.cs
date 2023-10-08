using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ReporteProfesorXML
{
    private List<Profesor> profesores;

    private const string fileName = "profesores.xml";

    public ReporteProfesorXML()
    {
        // Inicializa la lista de profesores desde el archivo XML si existe, o crea una nueva lista
        if (File.Exists(fileName))
        {
            using (var streamReader = new StreamReader(fileName))
            {
                var serializer = new XmlSerializer(typeof(List<Profesor>));
                profesores = (List<Profesor>)serializer.Deserialize(streamReader) ?? new List<Profesor>();
            }
        }
        else
        {
            profesores = new List<Profesor>();
        }
    }

    public void GenerarReportePorNombreAscendente()
    {
        var profesoresPorNombreAscendente = profesores.OrderBy(p => p.NombreCompleto).ToList();
        GuardarReporteEnXML(profesoresPorNombreAscendente, "reporte.xml", false);
    }

    public void GenerarReportePorNominaAscendente()
    {
        // Desencriptar la nómina de cada profesor antes de ordenar
        var profesoresPorNominaAscendente = profesores.Select(p =>
        {
            p.NominaEncriptada = EncriptacionHelper.Desencriptar(p.NominaEncriptada);
            return p;
        }).OrderBy(p => ExtractNumeroFromNomina(p.NominaEncriptada, "NOMI")).ToList();
        GuardarReporteEnXML(profesoresPorNominaAscendente, "reporte.xml", true);
    }

    public void GenerarReportePorDivisionAscendente()
    {
        var profesoresPorDivisionAscendente = profesores.OrderBy(p => p.Division).ToList();
        GuardarReporteEnXML(profesoresPorDivisionAscendente, "reporte.xml", false);
    }

    public void GenerarReportePorMateriasAscendente()
    {
        var profesoresPorMateriasAscendente = profesores.OrderBy(p => string.Join(",", p.MateriasQueImparte)).ToList();
        GuardarReporteEnXML(profesoresPorMateriasAscendente, "reporte.xml", false);
    }

    public void GenerarReportePorIDAscendente()
    {
        var profesoresPorIDAscendente = profesores.OrderBy(p => p.ProfesorId).ToList();
        GuardarReporteEnXML(profesoresPorIDAscendente, "reporte.xml", false);
    }

    private void GuardarReporteEnXML(List<Profesor> listaProfesores, string nombreArchivo, bool encript)
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
            // Guarda la lista de profesores en el archivo XML
            using (var streamWriter = new StreamWriter(nombreArchivo))
            {
                var serializer = new XmlSerializer(typeof(List<Profesor>));
                serializer.Serialize(streamWriter, listaProfesores);
            }

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

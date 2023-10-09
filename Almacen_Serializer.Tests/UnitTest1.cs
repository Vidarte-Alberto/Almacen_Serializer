namespace Almacen_Serializer.Tests;

public class UnitTest1
{
    [Fact]
    public void TestGenerateKeys()
    {
        var encriptacionHelper = new EncriptacionHelper();
        encriptacionHelper.CrearClavesEncriptacion();
        Assert.True(File.Exists("clave.bin"));
        Assert.True(File.Exists("iv.bin"));
    }

    [Fact]
    public void TestRegistrarAlmacenista()
    {
        var nombreCompleto = "Almacenista 1";
        var password = "'123456";
        var almacenistaHelperJSON = new AlmacenistaHelperJSON("../almacenistas.json");
        almacenistaHelperJSON.CrearAlmacenista(nombreCompleto, password);
        Assert.True(File.Exists("../almacenistas.json"));
        FileInfo fileInfo = new FileInfo("../almacenistas.json");
        long tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"../almacenistas.json"} está vacío.");

        var almacenistaHelperXML = new AlmacenistaHelperXML("../almacenistas.xml");
        almacenistaHelperXML.CrearAlmacenista(nombreCompleto, password);
        Assert.True(File.Exists("../almacenistas.xml"));
        fileInfo = new FileInfo("../almacenistas.xml");
        tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"../almacenistas.xml"} está vacío.");
    }

    [Fact]
    public void TestRegistrarProfesor()
    {
        var nombreCompleto = "Profesor 1";
        var passwordEncriptada = "'123456";
        var nominaEncriptada = "NOMI123456";
        string[] materiasQueImparte = { "Materia 1", "Materia 2" };
        var division = "Division 1";
        var profesorHelperJSON = new ProfesorHelperJSON("../profesores.json");
        profesorHelperJSON.CrearProfesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
        Assert.True(File.Exists("../profesores.json"));
        FileInfo fileInfo = new FileInfo("../profesores.json");
        long tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"../profesores.json"} está vacío.");

        var profesorHelperXML = new ProfesorHelperXML("../profesores.xml");
        profesorHelperXML.CrearProfesor(nombreCompleto, nominaEncriptada, passwordEncriptada, materiasQueImparte, division);
        Assert.True(File.Exists("../profesores.xml"));
        fileInfo = new FileInfo("../profesores.xml");
        tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"../profesores.xml"} está vacío.");
    }

    [Fact]
    public void TestIniciarSesionAlmacenista()
    {
        var nombreCompleto = "Almacenista 1";
        var password = "123456";
        var almacenistaHelperJSON = new AlmacenistaHelperJSON("../almacenistas.json");
        var almacenista = almacenistaHelperJSON.IniciarSesion(nombreCompleto, password);
        Assert.NotNull(almacenista);

        var almacenistaHelperXML = new AlmacenistaHelperXML("../almacenistas.xml");
        almacenista = almacenistaHelperXML.IniciarSesion(nombreCompleto, password);
        Assert.NotNull(almacenista);
    }

    [Fact]
    public void CambiarPassAlmacenista()
    {
        var id = 1;
        var pass = "123456";
        var almacenistaHelperJSON = new AlmacenistaHelperJSON("../almacenistas.json");
        almacenistaHelperJSON.EditarPassAlmacenista(id, pass);
        var almacenista = almacenistaHelperJSON.ListarAlmacenistas().Find(a => a.AlmacenistaId == id);
        Assert.NotNull(almacenista);
        Assert.Equal(pass, EncriptacionHelper.Desencriptar(almacenista.PasswordEncriptada));

        var almacenistaHelperXML = new AlmacenistaHelperXML("../almacenistas.xml");
        almacenistaHelperXML.EditarPassAlmacenista(id, pass);
        almacenista = almacenistaHelperXML.ListarAlmacenistas().Find(a => a.AlmacenistaId == id);
        Assert.NotNull(almacenista);
        Assert.Equal(pass, EncriptacionHelper.Desencriptar(almacenista.PasswordEncriptada));
    }

    [Fact]
    public void CambiarPassProfesor()
    {
        var id = 1;
        var pass = "12356";
        var profesorHelperJSON = new ProfesorHelperJSON("../profesores.json");
        profesorHelperJSON.EditarPassProfesor(id, pass);
        var profesorJSON = profesorHelperJSON.ListarProfesores().Find(p => p.ProfesorId == id);
        Assert.NotNull(profesorJSON);
        Assert.Equal(pass, EncriptacionHelper.Desencriptar(profesorJSON.PasswordEncriptada));

        var profesorHelperXML = new ProfesorHelperXML("../profesores.xml");
        profesorHelperXML.EditarPassProfesor(id, pass);
        var profesorXML = profesorHelperXML.ListarProfesores().Find(p => p.ProfesorId == id);
        Assert.NotNull(profesorXML);
        Assert.Equal(pass, EncriptacionHelper.Desencriptar(profesorXML.PasswordEncriptada));
    }

    [Fact]
    public void TestListarProfesores()
    {
        var profesorHelperJSON = new ProfesorHelperJSON("../profesores.json");
        var profesores = profesorHelperJSON.ListarProfesores();
        Assert.True(profesores.Count > 0);

        var profesorHelperXML = new ProfesorHelperXML("../profesores.xml");
        profesores = profesorHelperXML.ListarProfesores();
        Assert.True(profesores.Count > 0);
    }

    [Fact]
    public void TestGenerarReportePorNominaProfesor()
    {
        var reportesJSON = new ReporteProfesorJSON("../profesores.json");
        reportesJSON.GenerarReportePorNominaAscendente();
        Assert.True(File.Exists("reporte.json"));
        FileInfo fileInfo = new FileInfo("reporte.json");
        long tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.json"} está vacío.");

        var reportesXML = new ReporteProfesorXML("../profesores.xml");
        reportesXML.GenerarReportePorNominaAscendente();
        Assert.True(File.Exists("reporte.xml"));
        fileInfo = new FileInfo("reporte.xml");
        tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.xml"} está vacío.");
    }

    [Fact]
    public void TestGenerarReportePorIDProfesor()
    {
        var reportesJSON = new ReporteProfesorJSON("../profesores.json");
        reportesJSON.GenerarReportePorIDAscendente();
        Assert.True(File.Exists("reporte.json"));
        FileInfo fileInfo = new FileInfo("reporte.json");
        long tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.json"} está vacío.");

        var reportesXML = new ReporteProfesorXML("../profesores.xml");
        reportesXML.GenerarReportePorIDAscendente();
        Assert.True(File.Exists("reporte.xml"));
        fileInfo = new FileInfo("reporte.xml");
        tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.xml"} está vacío.");
    }

    [Fact]
    public void TestGenerarReportePorNombreProfesor()
    {
        var reportesJSON = new ReporteProfesorJSON("../profesores.json");
        reportesJSON.GenerarReportePorNombreAscendente();
        Assert.True(File.Exists("reporte.json"));
        FileInfo fileInfo = new FileInfo("reporte.json");
        long tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.json"} está vacío.");

        var reportesXML = new ReporteProfesorXML("../profesores.xml");
        reportesXML.GenerarReportePorNombreAscendente();
        Assert.True(File.Exists("reporte.xml"));
        fileInfo = new FileInfo("reporte.xml");
        tamañoArchivo = fileInfo.Length;
        Assert.True(tamañoArchivo > 0, $"El archivo {"reporte.xml"} está vacío.");
    }
}
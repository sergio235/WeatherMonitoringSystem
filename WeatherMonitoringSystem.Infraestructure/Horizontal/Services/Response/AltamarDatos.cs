using System;
using System.Collections.Generic;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response
{
    public class OrigenAltamar
    {
        public string Productor { get; set; }
        public string Web { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string NotaLegal { get; set; }
        public DateTime Elaborado { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }

    public class Aviso
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Texto { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
    }

    public class SituacionAltamar
    {
        public DateTime Analisis { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Texto { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ZonaAltamar
    {
        public string Texto { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class PrediccionAltamar
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public List<ZonaAltamar> Zona { get; set; }
    }

    public class Tendencia
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Texto { get; set; }
    }

    public class AltamarDatos : IDatos
    {
        public OrigenAltamar Origen { get; set; }
        public Aviso Aviso { get; set; }
        public SituacionAltamar Situacion { get; set; }
        public PrediccionAltamar Prediccion { get; set; }
        public Tendencia Tendencia { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}

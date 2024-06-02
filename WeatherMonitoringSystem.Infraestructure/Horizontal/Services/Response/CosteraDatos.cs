using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherMonitoringSystem.Infraestructure.Horizontal.Services.Response
{
    public class OrigenCostera
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

    public class SituacionCostera
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Texto { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
    }

    public class SubzonaCostera
    {
        public string Texto { get; set; }

        public int Id { get; set; }

        public string Nombre { get; set; }
    }

    public class ZonaCostera
    {
        public List<SubzonaCostera> Subzona { get; set; }

        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class PrediccionCostera
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public List<ZonaCostera> Zona { get; set; }
    }

    public class CosteraDatos : IDatos
    {
        public OrigenCostera Origen { get; set; }
        public SituacionCostera Situacion { get; set; }
        public PrediccionCostera Prediccion { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}

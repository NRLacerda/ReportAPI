using System.ComponentModel.DataAnnotations;

namespace ReportAPI.Models
{
    public class ReportModel
    {
        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public Guid UserGuid { get; set; }

        public string TipoProcedimento {  get; set; } = String.Empty;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Malshinon
{
    [Table("IntelReports")]
    public class IntelReport
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("reporter_id")]
        public int? ReporterId { get; set; }

        [Column("target_id")]
        public int? TargetId { get; set; }

        [Column("text")]
        public string Text { get; set; } = null!;

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

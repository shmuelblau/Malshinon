using System.ComponentModel.DataAnnotations.Schema;

namespace Malshinon
{
    


    [Table("People")]
    public class People
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Column("secret_code")]
        public string SecretCode { get; set; } = null!;

        [Column("type")]
        public string? Type { get; set; }

        [Column("num_reports")]
        public int NumReports { get; set; } = 0;

        [Column("num_mentions")]
        public int NumMentions { get; set; } = 0;
    }
}

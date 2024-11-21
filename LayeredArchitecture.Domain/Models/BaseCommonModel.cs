using System.ComponentModel.DataAnnotations.Schema;

namespace LayeredArchitecture.Domain.Models
{
    public class BaseCommonModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime created_at { get; set; }

        public string created_user { get; set; } = null!;

        public DateTime? updated_at { get; set; }

        public string? updated_user { get; set; }

        public bool? delete_flg { get; set; } = false;
    }
}

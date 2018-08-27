using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInfrastructure.Models
{
    [Table("t_user")]
    public class User
    {
        [StringLength(255)]
        public string Id { get; set; }
        [StringLength(255)]
        public string Username { get; set; }
        [StringLength(255)]
        public string Firstname { get; set; }
        [StringLength(255)]
        public string Familyname { get; set; }
    }
}

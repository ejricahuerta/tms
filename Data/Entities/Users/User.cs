using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Users
{
    [Table("users")]
    public class User : IEntity
    {
        [Key]
        [Column("uid")]

        public int Id { get; set; }
        [Column("uName")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [NotMapped]
        public string DateAdded { get; set; }
        [Column("firstName")]
        public string FirstName { get; set; }
        [Column("lastName")]
        public string LastName { get; set; }
        [NotMapped]
        public string Phone { get; set; }

        [NotMapped]
        public string Office { get; set; }
        [NotMapped]
        public string Department { get; set; }

        [NotMapped]
        public string Manager { get; set; }
    }
}

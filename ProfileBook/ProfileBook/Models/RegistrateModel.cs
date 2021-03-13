using SQLite;

namespace ProfileBook.Models
{
    public class RegistrateModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}

using SQLite;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        public string ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

    }
}

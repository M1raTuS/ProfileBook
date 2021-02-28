using SQLite;
using System;

namespace ProfileBook.Models
{
    public class UserModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        public string ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public int RegId { get; set; }
    }
}

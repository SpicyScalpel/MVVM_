using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM_app.Models
{
    [Table("Friends")]
    public class Friend
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        private string _photoPath;
        public string PhotoPath
        {
            get { return _photoPath; }
            set { _photoPath = value; }
        }
    }
}
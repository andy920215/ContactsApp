using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Models
{
    public class ContactItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        //public string Details => $"{Number} - {Email}";
    }
}
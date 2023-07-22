using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotecario.Data.Entities
{
    public class User
    {
        [MaxLength(10)]
        public string Id { get; set; }

        public int UserType { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public virtual ICollection<Loan> Loans { get; set;} = new List<Loan>(); 
    }
}

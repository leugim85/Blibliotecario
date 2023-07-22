using System;
using System.ComponentModel.DataAnnotations;

namespace Bibliotecario.Data.Entities
{
    public class Book
    {
        [Key]
        public Guid Isbn { get; set; }

        public string Title { get; set; }
    }
}

using System;

namespace Bibliotecario.Data.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }

        public DateTime MaximumReturnDate { get; set; }

        public Guid BookId { get; set; }

        public string UserId { get; set; }

        public int UserType { get; set; }
    }
}

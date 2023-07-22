using Bibliotecario.Business.ModelsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotecario.Data.ModelsDto
{
    public class LoanResponseDto: LoanDTO
    {
        public string Id { get; set; }

        public DateTime FechaMaximaDevolucion { get; set; }
    }
}

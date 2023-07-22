using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly ILoanAppService loanAppService;

        public PrestamoController(ILoanAppService loanAppService)
        {
            this.loanAppService = loanAppService ?? throw new ArgumentNullException(nameof(loanAppService));
        }

        [HttpPost]
        public async Task<IActionResult> Prestamo(LoanDTO loanDTO)
        {
            var result = await loanAppService.AddNewLoan(loanDTO);
            if (!result.IsSucces)
            {
                return BadRequest(new { mensaje = result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Prestamo(string id) 
        {
            var result = await loanAppService.GetLoanById(id);
            if(result.IsSucces)
                return Ok(result.Data);

            return NotFound(new { mensaje = result.Message });
        }
    }
}

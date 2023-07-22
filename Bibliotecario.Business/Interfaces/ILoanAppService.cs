using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Business.Utilities;
using Bibliotecario.Data.ModelsDto;
using System.Threading.Tasks;

namespace Bibliotecario.Business.Services
{
    public interface ILoanAppService
    {
       public Task<GenericResult<LoanPostResponeDto>> AddNewLoan(LoanDTO loanDTO);

        public Task<GenericResult<LoanResponseDto>> GetLoanById(string loanId);
    }
}
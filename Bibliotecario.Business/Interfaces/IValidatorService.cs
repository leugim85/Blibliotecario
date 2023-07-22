using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Business.Utilities;
using System.Threading.Tasks;

namespace Bibliotecario.Business.Validation
{
    public interface IValidatorService
    {
        public bool LoanDtoValidatorData(LoanDTO loanDTO);
        public Task<GenericResult<string>> GuestUserStatus(string userId);

        public GenericResult<string> ValidatorStringAsAGuid(string guid);
    }
}
using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Business.Utilities;
using Bibliotecario.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Bibliotecario.Business.Validation
{
    public class ValidatorService : IValidatorService
    {
        private readonly ILoanDomainService loanDomainService;
        public ValidatorService(ILoanDomainService loanDomainService)
        {
            this.loanDomainService = loanDomainService ?? throw new ArgumentNullException(nameof(loanDomainService));
        }
        public bool LoanDtoValidatorData(LoanDTO loanDTO)
        {
            if (!ValidatorStringAsAGuid(loanDTO.Isbn).IsSucces || loanDTO.TipoUsuario < 1 ||
                loanDTO.TipoUsuario > 3 || loanDTO.IdentificacionUsuario == string.Empty ||
                loanDTO.IdentificacionUsuario.Length > 10)
            {
                return false;
            }
            return true;
        }

        public async Task<GenericResult<string>> GuestUserStatus(string userId) 
        {
            var hasActiveLoan = await loanDomainService.ValidateLoanByUserId(userId);
            if (hasActiveLoan)
                return new GenericResult<string>(false, $"El usuario con identificacion {userId } ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");

            return new GenericResult<string>();
        }

        public GenericResult<string> ValidatorStringAsAGuid(string guid) 
        {
            if (string.IsNullOrWhiteSpace(guid))
                return new GenericResult<string>(false, "El id es invalido");

            Guid guidOutput;
            if (!Guid.TryParse(guid, out guidOutput))
                return new GenericResult<string>(false, $"EL id {guid} no cuenta con la estructura correcta");

            return new GenericResult<string>();
        }
    }
}

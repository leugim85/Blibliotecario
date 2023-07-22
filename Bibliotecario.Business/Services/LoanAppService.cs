using AutoMapper;
using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Business.Utilities;
using Bibliotecario.Business.Validation;
using Bibliotecario.Data.Entities;
using Bibliotecario.Data.Interfaces;
using Bibliotecario.Data.ModelsDto;
using System;
using System.Threading.Tasks;

namespace Bibliotecario.Business.Services
{
    public class LoanAppService : ILoanAppService
    {
        private readonly ILoanDomainService loanDomainService;
        private readonly IMapper mapper;
        private readonly IValidatorService validatorService;
        public LoanAppService(ILoanDomainService loanDomainService, IMapper mapper, IValidatorService validatorService)
        {
            this.loanDomainService = loanDomainService ?? throw new ArgumentNullException(nameof(loanDomainService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.validatorService = validatorService ?? throw new ArgumentNullException(nameof(validatorService));
        }

        public async Task<GenericResult<LoanPostResponeDto>> AddNewLoan(LoanDTO loanDTO)
        {
            if (loanDTO.TipoUsuario == 3)
            {
                var hasLoan = await validatorService.GuestUserStatus(loanDTO.IdentificacionUsuario);
                if (!hasLoan.IsSucces)
                    return new GenericResult<LoanPostResponeDto>(false, hasLoan.Message);
            }

            var isValid = validatorService.LoanDtoValidatorData(loanDTO);

            if (isValid)
            {
                var loan = mapper.Map<Loan>(loanDTO);
                loan.Id = Guid.NewGuid();
                loan.MaximumReturnDate = calculateMaximumReturnDate(loanDTO.TipoUsuario);
                var result = mapper.Map<LoanPostResponeDto>(await loanDomainService.AddNewLoan(loan));
                return new GenericResult<LoanPostResponeDto>(isValid, result);
            }

            return new GenericResult<LoanPostResponeDto>(false);
        }

        public async Task<GenericResult<LoanResponseDto>> GetLoanById(string loanId) 
        {
            var isValidId = validatorService.ValidatorStringAsAGuid(loanId);
            if (isValidId.IsSucces) 
            {
                var loan = await loanDomainService.GetLoanById(Guid.Parse(loanId));
                if (loan != null) 
                {
                    return new GenericResult<LoanResponseDto>(true,mapper.Map<LoanResponseDto>(loan));
                }

                return new GenericResult<LoanResponseDto>(false, $"El prestamo con id {loanId} no existe");
            }

            return new GenericResult<LoanResponseDto>(false, isValidId.Message);
        }

        private DateTime calculateMaximumReturnDate(int tipoUsuario)
        {
            var date = DateTime.Now;
            var days = 0;
            switch (tipoUsuario)
            {
                case 1:
                    days = 10;
                    break;
                case 2:
                    days = 8;
                    break;
                case 3:
                    days = 7;
                    break;
            }

            while (days > 0)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    days -= 1;
                }
            }

            return date;
        }
    }
}

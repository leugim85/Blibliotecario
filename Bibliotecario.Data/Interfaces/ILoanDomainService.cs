using Bibliotecario.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Bibliotecario.Data.Interfaces
{
    public  interface ILoanDomainService
    {
        public Task<Loan> AddNewLoan(Loan loan);

        public Task<Loan> GetLoanById(Guid loanId);

        public Task<bool> ValidateLoanByUserId(string userId);
    }
}

using Bibliotecario.Data.Entities;
using Bibliotecario.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotecario.Data.Services
{
    public class LoanDomainService: ILoanDomainService
    {
        private readonly PersistenceContext context;
        public LoanDomainService(PersistenceContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Loan> AddNewLoan(Loan loan) 
        {
            try
            {
                context.Loans.Add(loan);
                await context.SaveChangesAsync();
                return loan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Loan> GetLoanById(Guid loanId) 
        {
           return await context.Loans.Where(x => x.Id == loanId).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidateLoanByUserId(string userId) 
        {
            return await context.Loans.Where(x => x.UserId == userId && x.MaximumReturnDate > DateTime.Now).AnyAsync();            
        }
    }
}

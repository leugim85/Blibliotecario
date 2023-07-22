using Api.Test;
using AutoMapper;
using Bibliotecario.Business;
using Bibliotecario.Business.Services;
using Bibliotecario.Business.Utilities;
using Bibliotecario.Business.Validation;
using Bibliotecario.Data.Entities;
using Bibliotecario.Data.Interfaces;
using Bibliotecario.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace PruebaIngresoBibliotecario.Api.Tests
{
    public class LoanAppServiceTest: IntegrationTestBuilder
    {
        [Fact]
        public async void When_ValidatorService_IsNull_ThrowException()
        {
           var mockDomainService = new Mock<ILoanDomainService>();
            mockDomainService
                .Setup(x => x.GetLoanById(new Guid()))
                .ReturnsAsync(new Loan() { Id = new Guid(), BookId = new Guid(), UserId = "123456789", MaximumReturnDate = DateTime.Now, UserType = 1 });

            Assert.Throws<ArgumentNullException>(() => new LoanAppService(mockDomainService.Object, GetMapper(), null));
        }

        [Fact]
        public async void When_IdIsInvalidFormat_ThrowException()
        {
            var mockDomainService = new Mock<ILoanDomainService>();
            mockDomainService
                .Setup(x => x.GetLoanById(new Guid()))
                .ReturnsAsync(new Loan() { Id = new Guid(), BookId = new Guid(), UserId = "123456789", MaximumReturnDate = DateTime.Now, UserType = 1 });

            var mockValidatorService = new Mock<IValidatorService>();
            mockValidatorService
                .Setup(x => x.ValidatorStringAsAGuid("123-456-2156"))
                .Returns(new GenericResult<string>(false, $"EL id 123-456-2156 no cuenta con la estructura correcta"));

            var sut = new LoanAppService(mockDomainService.Object, GetMapper(), mockValidatorService.Object);
            var result = await sut.GetLoanById("123-456-2156");

            Assert.Equal($"EL id 123-456-2156 no cuenta con la estructura correcta", result.Message);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<AutomapperProfile>();
            });

            return config.CreateMapper();
        }
    }
}

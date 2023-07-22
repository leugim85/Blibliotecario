using AutoMapper;
using Bibliotecario.Business.ModelsDto;
using Bibliotecario.Data.Entities;
using Bibliotecario.Data.ModelsDto;

namespace Bibliotecario.Business
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<LoanDTO, Loan>()
                .ForMember(l => l.UserId, ld => ld.MapFrom(ld => ld.IdentificacionUsuario))
                .ForMember(l => l.BookId, ld => ld.MapFrom(ld => ld.Isbn))
                .ForMember(l => l.UserType, l => l.MapFrom(l => l.TipoUsuario));

            CreateMap<Loan, LoanPostResponeDto>()
                .ForMember(lp => lp.Id, l => l.MapFrom(l => l.Id))
                .ForMember(lp=> lp.fechaMaximaDevolucion, l => l.MapFrom(l => l.MaximumReturnDate.ToShortDateString()));

            CreateMap<Loan, LoanResponseDto>()
               .ForMember(lp => lp.Id, l => l.MapFrom(l => l.Id))
               .ForMember(lp => lp.FechaMaximaDevolucion, l => l.MapFrom(l => l.MaximumReturnDate))
               .ForMember(lp => lp.Isbn, l => l.MapFrom(l => l.BookId))
               .ForMember(lp => lp.IdentificacionUsuario, l => l.MapFrom(l => l.UserId))
               .ForMember(lp => lp.TipoUsuario, l => l.MapFrom(l => l.UserType));
        }
    }
}

using AutoMapper;
using WebAppEcommerce_Net8.Data;
using WebAppEcommerce_Net8.ViewModel;

namespace WebAppEcommerce_Net8.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
            // trường hợp khác tên thì chỉ rõ , còn không thì không cần
            //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen)) 
            //.ReverseMap();
        }
    }
}

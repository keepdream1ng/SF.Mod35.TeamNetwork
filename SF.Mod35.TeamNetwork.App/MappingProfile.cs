using AutoMapper;
using SF.Mod35.TeamNetwork.App.Views.Register;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(u => u.Email, opt => opt.MapFrom(vm => vm.EmailReg))
            .ForMember(u => u.ImageUrl, opt => opt.MapFrom(vm => $"https://i.pravatar.cc/200?u={vm.EmailReg}"))
            .ForMember(u => u.UserName, opt => opt.MapFrom(vm => vm.Login));
    }
}

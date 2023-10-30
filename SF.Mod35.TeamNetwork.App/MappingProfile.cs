using AutoMapper;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.App.Views.Register;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(u => u.Email,
                opt => opt.MapFrom(vm => vm.EmailReg))
            .ForMember(u => u.ImageUrl,
                opt => opt.MapFrom(vm => $"https://i.pravatar.cc/200?u={vm.EmailReg}"))
            .ForMember(u => u.DateOfBirth,
                opt => opt.MapFrom(vm => vm.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
            .ForMember(u => u.UserName,
                opt => opt.MapFrom(vm => vm.EmailReg));

        CreateMap<User, UserEditViewModel>()
            .ForMember(vm => vm.DateOfBirth,
                opt => opt.MapFrom(u => DateOnly.FromDateTime(u.DateOfBirth)));

        CreateMap<UserEditViewModel, User>()
            .ForMember(u => u.DateOfBirth,
                opt => opt.MapFrom(vm => vm.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
            .ForMember(u => u.UserName,
                opt => opt.MapFrom(vm => vm.Email));
    }
}

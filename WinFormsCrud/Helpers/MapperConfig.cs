using AutoMapper;
using WinFormsCrud.Dto;
using WinFormsCrud.Model;

namespace WinFormsCrud.Helpers
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Case, CaseDto>();
                cfg.CreateMap<CaseDto, Case>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}

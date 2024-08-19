using AutoMapper;
using CommonLibrary.Dto;
using SimpleWebApi.Model;

namespace SimpleWebApi.Helpers
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
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}

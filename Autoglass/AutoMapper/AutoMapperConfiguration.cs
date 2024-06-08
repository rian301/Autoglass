using AutoMapper;

namespace Autoglass.API.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainMappingProfile());
            });
        }
    }
}

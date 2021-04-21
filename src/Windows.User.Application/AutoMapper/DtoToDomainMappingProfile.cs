using AutoMapper;
namespace Windows.User.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DtoToDomainMappingProfile()
        {
            CreateMap<UserAddRequest,Domain.User>();
            CreateMap<UserModifyRequest, Domain.User>();
            CreateMap<UserInfo, Domain.User>();
            CreateMap<ProfileRequest, Domain.User>();
        }
    }
}

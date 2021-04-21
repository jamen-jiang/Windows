using AutoMapper;
using System.Linq;

namespace Windows.User.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToDtoMappingProfile()
        {
            //CreateMap<Domain.User, UserResponse>()
            //.ForMember(
            //dto => dto.OrganizationIds,
            //domain => domain.MapFrom(src => src.Organization_User.Select(s=>s.OrganizationId).ToList())
            //).ForMember(
            //dto => dto.RoleIds,
            //domain => domain.MapFrom(src => src.Role_User.Select(s => s.RoleId).ToList())
            //);
        }
    }
}

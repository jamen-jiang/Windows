using AutoMapper;
using Windows.Module.Domain;

namespace Windows.Module.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DtoToDomainMappingProfile()
        {
            CreateMap<OperateAddRequest, Operate>();
            CreateMap<OperateModifyRequest, Operate>();

            CreateMap<ModuleAddRequest, Domain.Module>();
            CreateMap<ModuleModifyRequest, Domain.Module>();

            
        }
    }
}

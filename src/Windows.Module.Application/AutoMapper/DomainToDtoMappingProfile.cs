using AutoMapper;
using Windows.Application.Shared.Dto;
using Windows.Infrastructure.Extensions;
using Windows.Module.Application.Enums;
using Windows.Module.Domain;

namespace Windows.Module.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToDtoMappingProfile()
        {
            CreateMap<Domain.Module, ModuleResponse>().ForMember(
               dto => dto.TypeName,
               domain => domain.MapFrom(src => src.Type.GetDescription<ModuleTypeEnum>())
            );
            CreateMap<Domain.Module, ModuleOperateResponse>().ForMember(
              dto => dto.TypeName,
              domain => domain.MapFrom(src => src.Type.GetDescription<ModuleTypeEnum>())
            );
            CreateMap<Domain.Module, ComboBoxTreeResponse>();

            CreateMap<Operate, OperateResponse>().ForMember(
               dto => dto.TypeName,
               domain => domain.MapFrom(src => src.Type.GetDescription<OperateTypeEnum>())
            ).ForMember(
                dto => dto.ModuleName,
                domain => domain.MapFrom(src => src.Module.Name)
            );
        }
    }
}

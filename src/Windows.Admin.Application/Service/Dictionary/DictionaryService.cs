using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Admin.Infrastructure.EFCore;
using Windows.Application.Shared.Service;
using Windows.Infrastructure.EFCore;
using Windows.Infrastructure.Extensions;

namespace Windows.Admin.Application
{
    public class DictionaryService : BaseService,IDictionaryService
    {
        private readonly IMapper _mapper;
        protected AdminDbContext _db;
        public DictionaryService(AdminDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxResponse>> GetCategorys()
        {
            using (_db)
            {
                List<Domain.Dictionary> data = await _db.Dictionary.AsNoTracking().Where(x => x.PId == null).ToListAsync();
                List<ComboBoxResponse> list = new List<ComboBoxResponse>();
                foreach (var d in data)
                {
                    ComboBoxResponse model = new ComboBoxResponse()
                    { 
                        Value = d.Id.ToString(),
                        Name = d.Name
                    };
                    list.Add(model);
                }
                return list;
            }
        }
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryResponse>> Query()
        {
            using (_db)
            {
                List<Domain.Dictionary> dictionarys = await _db.Dictionary.AsNoTracking().ToListAsync();
                List<DictionaryResponse> dtos =  _mapper.Map<List<DictionaryResponse>>(dictionarys);
                List<DictionaryResponse> list = new List<DictionaryResponse>();
                CreateTree(null, dtos, list);
                return list;
            }
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DictionaryResponse> Detail(int id)
        {
            using (_db)
            {
                Domain.Dictionary model = await _db.Dictionary.FindByIdAsync(id);
                return _mapper.Map<DictionaryResponse>(model);
            }
        }
        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Add(DictionaryAddRequest info)
        {
            using (_db)
            {
                Domain.Dictionary model = _mapper.Map<Domain.Dictionary>(info);
                if (info.PId != null)
                {
                    Domain.Dictionary pModel = await _db.Dictionary.FindByIdAsync(info.PId.ToInt());
                    model.Category = pModel.Category;
                    model.Name = pModel.Name;
                }
                await _db.AddEntityAsync(model);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(DictionaryModifyRequest info)
        {
            using (_db)
            {
                Domain.Dictionary model = await _db.Dictionary.FindByIdAsync(info.Id);
                _mapper.Map(info, model);
                //_db.ModifyEntity(model);
                if (info.PId != null)
                {
                    Domain.Dictionary pModel = await _db.Dictionary.FindByIdAsync(info.PId.ToInt());
                    model.Category = pModel.Category;
                    model.Name = pModel.Name;
                }
                await _db.SaveChangesAsync();
            }
        }
    }
}

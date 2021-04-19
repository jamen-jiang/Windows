using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class DictionaryService : BaseService,IDictionaryService
    {
        private readonly IMapper _mapper;
        public DictionaryService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ComboBoxResponse>> GetCategorys()
        {
            using (var db = NewDB())
            {
                var data = await db.Dictionary.AsNoTracking().Where(x => x.PId == null).ToListAsync();
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
            using (var db = NewDB())
            {
                var dictionarys = await db.Dictionary.AsNoTracking().ToListAsync();
                var dtos =  _mapper.Map<List<DictionaryResponse>>(dictionarys);
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
        public async Task<DictionaryResponse> Detail(Guid id)
        {
            using (var db = NewDB())
            {
                var model = await db.Dictionary.FindByIdAsync(id);
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
            using (var db = NewDB())
            {
                Dictionary model = _mapper.Map<Dictionary>(info);
                if (info.PId != null)
                {
                    var pModel = await db.Dictionary.FindByIdAsync(info.PId.ToGuid());
                    model.Category = pModel.Category;
                    model.Name = pModel.Name;
                }
                await db.AddEntityAsync(model);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task Modify(DictionaryModifyRequest info)
        {
            using (var db = NewDB())
            {
                var model = await db.Dictionary.FindByIdAsync(info.Id);
                _mapper.Map(info, model);
                db.ModifyEntity(model);
                if (info.PId != null)
                {
                    var pModel = await db.Dictionary.FindByIdAsync(info.PId.ToGuid());
                    model.Category = pModel.Category;
                    model.Name = pModel.Name;
                }
                await db.SaveChangesAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public interface IDictionaryService
    {
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <returns></returns>
        Task<List<ComboBoxResponse>> GetCategorys();
        /// <summary>
        /// 获取字典内容列表
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryResponse>> Query();
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DictionaryResponse> Detail(Guid id);
        /// <summary>
        /// 添加字典
        /// </summary>
        Task Add(DictionaryAddRequest info);
        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task Modify(DictionaryModifyRequest info);
    }
}

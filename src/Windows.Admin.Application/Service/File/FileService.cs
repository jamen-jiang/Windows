using Jyz.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Admin.Application
{
    public class FileService : BaseService, IFileService
    {
        private readonly IWebHostEnvironment _hostingEnv;
        public FileService(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        public async Task UploadAsync(IFormFile file)
        {
            string md5 = Utils.GetHash<MD5>(file.OpenReadStream());
            using (var db = NewDB())
            {
                var obj = await db.File.FirstOrDefaultAsync(x => x.Md5 == md5);
                if (obj == null)
                { 
                    
                }
            }
        }
        /// <summary>
        /// 本地文件上传，生成目录格式 {STORE_DIR}/{year}/{month}/{day}/{guid}.文件后缀
        /// assets/2020/05/12/fba73a0c-f2f7-499a-8ed8-5b10554d43b0.jpg
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<Tuple<string, long>> LocalUploadAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new ApiException("文件为空");
            }

            string saveFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            //得到 assets/2020/05/12
            string path = "";
            //string path = Path.Combine(_fileStorageOption.LocalFile.PrefixPath, DateTime.Now.ToString("yyy/MM/dd"));
            //得到wwwroot/assets/2020/05/12
            string createFolder = Path.Combine(_hostingEnv.WebRootPath, path);
            //创建这种不存在的目录
            if (!Directory.Exists(createFolder))
            {
                Directory.CreateDirectory(createFolder);
            }

            long len = 0;
            await using (FileStream fs = File.Create(Path.Combine(createFolder, saveFileName)))
            {
                await file.CopyToAsync(fs);
                len = fs.Length;
                await fs.FlushAsync();
            }

            //windows下Path.Combine,得到的\\，不符号路径的要求。替换一下
            //得到 路径与文件大小    assets/2020/05/12/fba73a0c-f2f7-499a-8ed8-5b10554d43b0.jpg
            return Tuple.Create(Path.Combine(path, saveFileName).Replace("\\", "/"), len);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Util
{
    public class UploadFile
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="uploadPath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="file">文件</param>
        /// <returns>是否成功</returns>
        public static bool UploadFiles(string uploadPath, string fileName, IFormFile file)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(uploadPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                uploadPath += fileName;
                var fs = new FileStream(uploadPath, FileMode.Create);
                file.CopyTo(fs);
                //fs.Write(msContent, 0, (int)msContent.Length);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e.Message);
                return false;
            }
        }
    }
}

using System;
using Lhs.Common;
using Lhs.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;
using LhsAPI.Dtos.Request.Common;
using LhsApi.Dtos.Request.Setting;
using LhsAPI.Dtos.Response.Setting;
using LhsAPI.Dtos.Response.User;
using Microsoft.AspNetCore.Http;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 公用接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AuthFilter]
    public class CommonController : PlatformControllerBase
    {
        private readonly IPositionRepository _positionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        /// <summary>
        /// 上传图片、视频
        /// </summary>
        [HttpPost("UploadFile")]
        public ResponseMessage UploadImg(ReqUploadImg req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            //根据type获取存放的文件夹
            var childPath = EnumHelper.GetDescription<CommonEnum.ImgChildPath>(req.Type);
            if (string.IsNullOrEmpty(childPath))
            {
                return result;
            }
            var uploadPath = string.Format(CommonConst.ImgUploadPath, childPath);//文件上传路径
            var name = $"{DateTime.Now.ToString(CommonConst.ImgDateFormat)}.png";//文件名
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            uploadPath += name;

            var msContent = Convert.FromBase64String(req.ImgEncode);
            var fs = new FileStream(uploadPath, FileMode.Create);
            fs.Write(msContent, 0, (int)msContent.Length);
            fs.Close();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = new
            {
                Url = $"{string.Format(CommonConst.ImgSavePath, childPath)}{name}"
            };
            return result;
        }
    }
}

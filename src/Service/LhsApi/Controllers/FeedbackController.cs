using System;
using System.Linq;
using Lhs.Common;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Interface;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI;
using LhsAPI.Dtos.Request.Approve;
using LhsAPI.Dtos.Request.Feedback;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 意见反馈相关的controller
    /// </summary>
    [Route("api/[controller]")]
    [AuthFilter]
    [ApiController]
    public class FeedbackController : PlatformControllerBase
    {
        private readonly ICustomerFeedbackRepository _customerFeedbackRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public FeedbackController(ICustomerFeedbackRepository customerFeedbackRepository)
        {
            _customerFeedbackRepository = customerFeedbackRepository;
        }

        /// <summary>
        /// 提交反馈-微信
        /// </summary>
        [HttpPost("submitFeedback")]
        public async Task<ResponseMessage> SubmitFeedback(ReqFeedback req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_CustomerFeedback()
            {
                UserId = req.UserId,
                ProjectId = req.ProjectId,
                Content = req.Content,
                Img = req.Img
            };
            await _customerFeedbackRepository.AddAsync(data);
            if (data.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
            }
            return result;
        }
    }
}

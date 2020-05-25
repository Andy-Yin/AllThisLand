using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Common;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    public class RespGetQualityApprovedList
    {
        public RespGetQualityApprovedList()
        {
        }

        public RespGetQualityApprovedList(T_ProjectQualityApprovalRecord itemInfo)
        {
            Id = itemInfo.Id;
            UserId = itemInfo.UserId;
            Result = itemInfo.Result;
            Remark = itemInfo.Remark;
            var imgs = itemInfo.Imgs.Split("|");
            if (imgs.Any())
            {
                foreach (string img in imgs)
                {
                    Imgs.Add(PicHelper.ConcatPicUrl(img));
                }
            }
            CreateTime = itemInfo.CreateTime.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 操作人的名字+角色名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 神品结果：0 同意 1 申诉
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 备注/验收说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片地址：|分隔
        /// </summary>
        public List<string> Imgs { get; set; } = new List<string>();

        /// <summary>
        /// 操作时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}

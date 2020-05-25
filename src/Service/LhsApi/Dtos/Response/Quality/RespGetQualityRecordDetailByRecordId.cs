using System;
using System.Collections.Generic;
using Core.Util;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    public class RespGetQualityRecordDetailByRecordId
    {
        public RespGetQualityRecordDetailByRecordId(T_ProjectQualityRecord record, ProjectUser user)
        {
            QualityNo = record.QualityNo;
            CategoryName = record.CategoryName;
            StandardAmmount = record.StandardAmmount;
            PunishedAmmount = record.PunishedAmmount;
            RectifyDate = record.RectifyDate.ToString("yyyy-MM-dd");
            Status = EnumHelper.GetDescription(typeof(EnumProjectQualityRecordStatus), record.Status);
            UserName = user.UserName;
            Remark = record.Remark;
        }

        /// <summary>
        /// 质检单号（APP显示）
        /// </summary>
        public string QualityNo { get; set; }

        /// <summary>
        /// 质检任务名称，一级分类的名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 标准金额
        /// </summary>
        public double StandardAmmount { get; set; }

        /// <summary>
        /// 已处罚金额
        /// </summary>
        public double PunishedAmmount { get; set; }

        /// <summary>
        /// 要求整改日期
        /// </summary>
        public string RectifyDate { get; set; }

        /// <summary>
        /// 质检管理里的状态包
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 整改责任人，默认项目经理
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 条目
        /// </summary>
        public List<QualityItem> QualityItemContent { get; set; } = new List<QualityItem>();
    }

    public class QualityItem
    {
        /// <summary>
        /// 处罚内容
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 处罚金额，标准
        /// </summary>
        public double StandardAmmount { get; set; }
    }
}

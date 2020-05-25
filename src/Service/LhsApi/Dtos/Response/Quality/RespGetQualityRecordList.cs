using System;
using System.Collections.Generic;
using System.Linq;
using Core.Util;
using Lhs.Common;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    public class RespGetQualityRecordList
    {
        public RespGetQualityRecordList()
        {
        }

        public RespGetQualityRecordList(T_ProjectQualityRecord itemInfo, T_Project project, string managerName, string supervisorName)
        {
            Id = itemInfo.Id;
            QualityNo = itemInfo.QualityNo;
            CategoryName = itemInfo.CategoryName;
            StandardAmmount = itemInfo.StandardAmmount;
            PunishedAmmount = itemInfo.PunishedAmmount;
            Status = itemInfo.Status;
            StatusName = EnumHelper.GetDescription(typeof(EnumProjectQualityRecordStatus), itemInfo.Status);
            RectifyDate = itemInfo.RectifyDate.ToString("yyyy-MM-dd HH:mm");
            ProjectName = project.ProjectName;
            ProjectNo = project.ProjectNo;
            ManagerName = managerName;
            SupervisorName = supervisorName;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 质检单号（APP显示）
        /// </summary>
        public string QualityNo { get; set; }

        /// <summary>
        /// 工长
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// 监理
        /// </summary>
        public string SupervisorName { get; set; }

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
        /// 质检管理里的状态
        /// </summary>
        public EnumProjectQualityRecordStatus Status { get; set; }

        public string StatusName { get; set; }
    }
}

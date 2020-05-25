using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Core.Util;
using Google.Protobuf.WellKnownTypes;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.ForeignDtos.Response.Project;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Response.Project
{
    /// <summary>
    /// 用户的项目
    /// </summary>
    public class ProjectInfo
    {

        public ProjectInfo() { }

        public ProjectInfo(UserProjectInfo project)
        {
            ProjectName = project.ProjectName;
            ProjectNo = project.ProjectNo;
            ContractNo = project.ContractNo;
            CustomerNo = project.CustomerNo;
            CustomerName = project.CustomerName;
            CustomerPhone = project.CustomerPhone;
            PlanStartDate = project.PlanStartDate.ToString(CommonMessage.DateFormatYMD);
            PlanEndDate = project.PlanEndDate?.ToString(CommonMessage.DateFormatYMD);
            ActualStartDate = project.ActualStartDate?.ToString(CommonMessage.DateFormatYMD);
            ActualEndDate = project.ActualEndDate?.ToString(CommonMessage.DateFormatYMD);
            if (project.Status != null && System.Enum.IsDefined(typeof(ProjectEnum.ProjectStatus), (int)project.Status))
            {
                Status = EnumHelper.GetDescription(typeof(ProjectEnum.ProjectStatus), (int)project.Status);
            }
            else
            {
                Status = EnumHelper.GetDescription(typeof(ProjectEnum.ProjectStatus), ProjectEnum.ProjectStatus.WaitStart);
            }
            DecorateType = project.DecorateType;
            Address = project.Address;
            ConstructionMasterName = project.ConstructionMasterName;
            ConstructionMasterPhone = project.ConstructionMasterPhone;
            SupervisionName = project.SupervisionName;
            SupervisionPhone = project.SupervisionPhone;
            DesignerName = project.DesignerName;
            DesignerPhone = project.DesignerPhone;
            QuotationId = project.QuotationId;
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 合同编码
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 计划开工日期
        /// </summary>
        public string PlanStartDate { get; set; }

        /// <summary>
        /// 计划完工日期
        /// </summary>
        public string PlanEndDate { get; set; }

        /// <summary>
        /// 实际开工日期
        /// </summary>
        public string ActualStartDate { get; set; }

        /// <summary>
        /// 实际完工日期
        /// </summary>
        public string ActualEndDate { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 装修类型
        /// </summary>
        public string DecorateType { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 项目经理、工长姓名
        /// </summary>
        public string ConstructionMasterName { get; set; }

        /// <summary>
        /// 项目经理、工长手机号
        /// </summary>
        public string ConstructionMasterPhone { get; set; }

        /// <summary>
        /// 监理姓名
        /// </summary>
        public string SupervisionName { get; set; }

        /// <summary>
        /// 监理手机号
        /// </summary>
        public string SupervisionPhone { get; set; }

        /// <summary>
        /// 设计师姓名
        /// </summary>
        public string DesignerName { get; set; }

        /// <summary>
        /// 设计师手机号
        /// </summary>
        public string DesignerPhone { get; set; }

        /// <summary>
        /// 领料单
        /// </summary>
        public string QuotationId { get; set; }
    }
}

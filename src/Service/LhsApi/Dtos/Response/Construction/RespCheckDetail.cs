using System.Collections.Generic;
using System.Linq;
using Core.Util;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 验收详情
    /// </summary>
    public class RespCheckDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public RespCheckDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public RespCheckDetail(T_ProjectConstructionCheckTask task)
        {
            PlanStartDate = task.PlanStartDate == null ? string.Empty : task.PlanStartDate?.ToString(CommonMessage.DateFormatYMD);
            PlanFinishDate = task.PlanEndDate == null ? string.Empty : task.PlanEndDate?.ToString(CommonMessage.DateFormatYMD);
            Status = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), task.Status);
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 计划开工日期
        /// </summary>
        public string PlanStartDate { get; set; }
        /// <summary>
        /// 计划完工日期
        /// </summary>
        public string PlanFinishDate { get; set; }

        /// <summary>
        /// 验收记录
        /// </summary>
        public List<CheckRecord> CheckRecordList { get; set; }
    }

    /// <summary>
    /// 验收记录
    /// </summary>
    public class CheckRecord
    {
        public CheckRecord()
        {
        }

        public CheckRecord(CheckRecordInfo record, T_User user)
        {
            Personnel = record.PositionName;
            OperatorId = user.Id;
            OperatorName = user.Name;
            Phone = user.Phone;
            Time = record.CreateTime.ToString(CommonMessage.DateFormatYMDHMS);
            Remarks = record.Remark;
            if (!string.IsNullOrEmpty(record.Imgs))
            {
                Enclosure = record.Imgs.Split(CommonConst.Separator).ToList().Select(PicHelper.ConcatPicUrl).ToList();
            }
        }

        /// <summary>
        /// 角色/岗位
        /// </summary>
        public string Personnel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public List<string> Enclosure { get; set; } = new List<string>();
    }
}

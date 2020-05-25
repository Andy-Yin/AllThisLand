using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Feedback
{
    public class ReqFeedback : ReqAuth
    {
        /// <summary>
        /// 
        /// </summary>
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(300)]
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Img { get; set; }
    }
}

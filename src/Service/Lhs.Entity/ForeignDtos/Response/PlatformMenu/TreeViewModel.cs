using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    public class TreeViewModel
    { 
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Value { get; set; }
            
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 父节点ModuleId
        /// </summary>
        public int ParentNodeId { get; set; }
        
        /// <summary>
        /// 子节点List
        /// </summary>
        public List<TreeViewModel> Children { get; set; }
    }
}

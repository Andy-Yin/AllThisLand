using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Request;

namespace LhsApi.Dtos.Response.Material
{
    public class RespBatchSyncMaterial
    {
        public RespBatchSyncMaterial(string materialNo, string space, string msg)
        {
            MaterialNo = materialNo;
            Space = space;
            Msg = msg;
        }
        public string MaterialNo { get; set; }
        public string Space { get; set; }
        public string Msg { get; set; }
    }
}

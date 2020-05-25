using Core.Data;
using Core.Util.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Request.User;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace Lhs.Service
{
    /// <summary>
    /// 派工
    /// </summary>
    public class ProjectWorkerRepository : PlatformBaseService<T_ProjectWorker>, IProjectWorkerRepository
    {
        public ProjectWorkerRepository(IConfiguration config)
        {
            Config = config;
        }
    }
}

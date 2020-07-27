using Lhs.Entity.DbEntity.DbModel;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    public class BattleLogRepository : PlatformBaseService<T_BattleLog>, IBattleLogRepository
    {
        public BattleLogRepository(IConfiguration config)
        {
            Config = config;
        }
    }
}

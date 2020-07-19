using Lhs.Entity.DbEntity.DbModel;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    public class HeroRepository : PlatformBaseService<T_Hero>, IHeroRepository
    {
        public HeroRepository(IConfiguration config)
        {
            Config = config;
        }
    }
}

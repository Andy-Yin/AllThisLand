using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Util;
using Dapper;
using Microsoft.Extensions.Options;

namespace Lhs.Service
{
    public abstract class PlatformBaseService<T> : BaseService<T> where T : class
    {
 
    }
}

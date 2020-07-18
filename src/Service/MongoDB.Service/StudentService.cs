using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Entity;

namespace MongoDB.Service
{
    public class StudentService : BaseService<Student>
    {
        public StudentService(IConfiguration config) : base(config, nameof(Student))
        {

        }
    }
}

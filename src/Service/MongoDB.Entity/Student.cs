using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Entity
{
    public class Student : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

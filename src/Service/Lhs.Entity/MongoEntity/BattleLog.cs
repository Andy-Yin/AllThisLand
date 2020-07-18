using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lhs.Entity.MongoEntity
{
    public class BattleLog : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

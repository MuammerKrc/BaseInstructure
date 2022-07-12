using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.BaseModels
{
    public class BaseModel<T>:BaseQueryModel
    {
        public T Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public long? CreaterUserId { get; set; }

    }
}

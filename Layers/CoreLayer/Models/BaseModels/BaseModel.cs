using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.BaseModels
{
    public class BaseModel<T>:TimeModel
    {
        public T Id { get; set; }
        public long? CreaterUserId { get; set; }
    }
}

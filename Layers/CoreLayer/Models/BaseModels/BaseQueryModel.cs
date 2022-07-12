using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.BaseModels
{
    public class BaseQueryModel
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}

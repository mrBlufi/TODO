using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public interface  IHasCreatedBy
    {
        UserData CreateBy { get; set; }
    }
}

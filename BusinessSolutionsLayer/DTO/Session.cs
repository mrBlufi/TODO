using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Models
{
    public class Session
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public DateTime ExperationTime { get; set; }

        public DateTime CreatingTime { get; set; }
    }
}

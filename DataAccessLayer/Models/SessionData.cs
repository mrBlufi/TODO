using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class SessionData
    {
        public Guid Id { get; set; }

        public UserData User { get; set; }

        public DateTime ExperationTime { get; set; }

        public DateTime CreatingTime { get; set; }
    }
}

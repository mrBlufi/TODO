using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class TaskData : IHasCreatedBy
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public UserData CreateBy { get; set; }
    }
}

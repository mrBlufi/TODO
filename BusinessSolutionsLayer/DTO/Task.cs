using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Models
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public User CreateBy { get; set; }
    }

}

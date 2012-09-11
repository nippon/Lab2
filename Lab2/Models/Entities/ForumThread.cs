using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2.Models.Entities.Abstract;
using System.ComponentModel;

namespace Lab2.Models.Entities
{
    public class ForumThread : IEntity
    {
        public Guid ID { get; set; }
        [DisplayName("Forum Thread Title")]
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
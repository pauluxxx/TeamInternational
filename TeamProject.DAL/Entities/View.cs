using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities.Interfaces;

namespace TeamProject.DAL.Entities
{
    public class View : IEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

       
        public int UserID { get; set; }
        public int MovieID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }
    }
}

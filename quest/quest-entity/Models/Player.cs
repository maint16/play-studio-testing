using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quest_entity.Models
{
    public class Player
    {
         public Guid Id { get; set; }
        public int EarnedPoints { get; set; }
        public int? LastMilestoneIndexCompleted { get; set; }
    }
}

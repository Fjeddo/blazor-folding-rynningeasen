using System.Collections.Generic;

namespace blazor_folding_rynningeasen.Models
{
    public class BoincHost
    {
        public Hardware Hardware { get; set; }
        public Project[] Projects { get; set; }
        public WorkUnit[] WorkUnits { get; set; }
        public Task[] Tasks { get; set; }
        public TimeStats TimeStats { get; set; }
    }

    public class Hardware
    {
        public double Temperature { get; set; }
    }
}

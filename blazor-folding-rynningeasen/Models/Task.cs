using System;

namespace blazor_folding_rynningeasen.Models
{
    public class Task
    {
        public string name { get; set; }
        public string WUname { get; set; }
        public string projectURL { get; set; }
        public DateTime received { get; set; }
        public DateTime reportdeadline { get; set; }
        public string readytoreport { get; set; }
        public string state { get; set; }
        public string schedulerstate { get; set; }
        public string active_task_state { get; set; }
        public string appversionnum { get; set; }
        public string resources { get; set; }
        public string CPUtimeatlastcheckpoint { get; set; }
        public string currentCPUtime { get; set; }
        public string estimatedCPUtimeremaining { get; set; }
        public string fractiondone { get; set; }
        public SwapSize swapsize { get; set; }
        public WorkingSetSize workingsetsize { get; set; }
        public string suspendedviaGUI { get; set; }
    }
}
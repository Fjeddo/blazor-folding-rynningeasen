namespace blazor_folding_rynningeasen.Models
{
    public class WorkUnit
    {
        public string name { get; set; }
        public string FPestimate { get; set; }
        public string FPbound { get; set; }
        public MemoryBound memorybound { get; set; }
        public DiskBound diskbound { get; set; }
    }
}
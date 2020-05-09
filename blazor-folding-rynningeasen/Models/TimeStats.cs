using System;

namespace blazor_folding_rynningeasen.Models
{
    public class TimeStats
    {
        public string now { get; set; }
        public string on_frac { get; set; }
        public string connected_frac { get; set; }
        public string cpu_and_network_available_frac { get; set; }
        public string active_frac { get; set; }
        public string gpu_active_frac { get; set; }
        public DateTime client_start_time { get; set; }
        public string previous_uptime { get; set; }
        public string session_active_duration { get; set; }
        public string session_gpu_active_duration { get; set; }
        public DateTime total_start_time { get; set; }
        public string total_duration { get; set; }
        public string total_active_duration { get; set; }
        public string total_gpu_active_duration { get; set; }
    }
}
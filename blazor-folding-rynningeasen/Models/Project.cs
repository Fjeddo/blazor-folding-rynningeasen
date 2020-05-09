using System;

namespace blazor_folding_rynningeasen.Models
{
    public class Project
    {
        public string name { get; set; }
        public string masterURL { get; set; }
        public string user_name { get; set; }
        public string team_name { get; set; }
        public string resourceshare { get; set; }
        public string user_total_credit { get; set; }
        public string user_expavg_credit { get; set; }
        public string host_total_credit { get; set; }
        public string host_expavg_credit { get; set; }
        public string nrpc_failures { get; set; }
        public string master_fetch_failures { get; set; }
        public string masterfetchpending { get; set; }
        public string schedulerRPCpending { get; set; }
        public string trickleuploadpending { get; set; }
        public string attachedviaAccountManager { get; set; }
        public string ended { get; set; }
        public string suspendedviaGUI { get; set; }
        public string dontrequestmorework { get; set; }
        public string diskusage { get; set; }
        public DateTime lastRPC { get; set; }
        public string projectfilesdownloaded { get; set; }
    }
}
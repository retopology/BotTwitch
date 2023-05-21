using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesModule
{
    public class rerportclass
    {
        public int id { get; set; }
        public int count_msg { get; set; }
        public int count_unic_users { get; set; }
        public string duration { get; set; }
        public int count_timeout { get; set; }
        public int count_ban { get; set; }
        public int msgs_per_min { get; set; }
        public int count_subs { get; set; }
        public string top_slovo { get; set; }
        public string top_user { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string streamerNick { get; set; }
    }
}

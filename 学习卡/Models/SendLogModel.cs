using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学习卡.Models
{
    public class SendLogModel
    {
        public String LogInfo { get; set; }
        public DateTime LogTime { get; set; }= DateTime.Now;
    }
}

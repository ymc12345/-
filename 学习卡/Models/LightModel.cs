using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 学习卡.Base;

namespace 学习卡.Models
{
    public class LightModel:NotifyBase
    {
        private bool _state;
        public bool State
        {
            get { return _state; }
            set { SetProperty<bool>(ref _state, value);
            
                LightColor = value ? "#f90" : "#888";
            }
        }

        private string _lightcolor="#888";
        public string LightColor
        {
            get { return _lightcolor; }
            set { SetProperty<string>(ref _lightcolor, value); }
        }

        public ushort Address { get; set; }
    }
}

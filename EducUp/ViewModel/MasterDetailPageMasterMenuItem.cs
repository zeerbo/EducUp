using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.ViewModel
{
    public class MasterDetailPageMasterMenuItem
    {
        public MasterDetailPageMasterMenuItem()
        {

        }

        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
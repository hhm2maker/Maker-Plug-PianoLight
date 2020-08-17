using Operation.Model;
using PlugLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoLight
{
    class PianoLightControl : IPageControl
    {
        public void GetResult(PageControlEnum.SendPage sendLight)
        {
            FileUtils.sendLight = sendLight;
        }
    }
}

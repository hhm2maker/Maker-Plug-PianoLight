using Operation.Model;
using PlugLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoLight
{
    class FileUtils
    {
        public static ProjectInfo projectInfo;
        public static string pageName;
        public static List<List<PageButtonModel>> pageButtonModels;

        public static List<IControl> iControls = new List<IControl>() { new PianoLightControl() };

        public static PageControlEnum.SendPage sendLight;
    }
}

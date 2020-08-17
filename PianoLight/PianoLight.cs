using Operation.Model;
using PlugLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PianoLight
{
    public class PianoLight : BasePlug
    {
        public override ImageSource GetIcon()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetAssembly(GetType());
            // 获取程序集中资源名称
            string resourceName = assembly.GetName().Name + ".g";
            // 资源管理器
            ResourceManager rm = new ResourceManager(resourceName, assembly);
            BitmapImage bitmap = new BitmapImage();
            using (ResourceSet set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                UnmanagedMemoryStream s;
                s = (UnmanagedMemoryStream)set.GetObject("Image/piano.png", true);

                // img在XAML声明的空间      
                return BitmapFrame.Create(s, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }

        public override List<PermissionsClass.Permissions> GetPermissions()
        {
            return new List<PermissionsClass.Permissions>() { PermissionsClass.Permissions.ProjectInfo, PermissionsClass.Permissions.Page };
        }

        private PianoLightUserControl pianoLightUserControl;
        public override System.Windows.Controls.UserControl GetView()
        {
            if (pianoLightUserControl == null) {
                pianoLightUserControl = new PianoLightUserControl();
            }
            return pianoLightUserControl;
        }

        public override List<IControl> GetControl()
        {
            return FileUtils.iControls;
        }

        public override PlugInfo GetInfo()
        {
            PlugInfo plugInfo = new PlugInfo
            {
                Title = "钢琴灯光",
                Author = "hhm",
                Version = "1.0.0",
                Describe = "制作钢琴单键灯。",
            };
            return plugInfo;
        }

        public override void SetProjectInfo(ProjectInfo projectInfo) {
            FileUtils.projectInfo = projectInfo;
        }
        public override void SetPageModel(string pageName,List<List<PageButtonModel>> pageButtonModels)
        {
            FileUtils.pageName = pageName;
            FileUtils.pageButtonModels = pageButtonModels;
        }
    }
}

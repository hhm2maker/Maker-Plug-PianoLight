using Maker.Business.Model.OperationModel;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PianoLight
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class PianoLightUserControl : UserControl
    {
        public PianoLightUserControl()
        {
            InitializeComponent();
        }

        LightGroup lights = new LightGroup();
        private void GetBaseLight(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.Filter = "midi文件|*.mid";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lights = FileBusiness.CreateInstance().ReadMidiFile(openFileDialog.FileName);
                tbFileName.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void Build(object sender, RoutedEventArgs e)
        {
            if (FileUtils.projectInfo != null)
            {
                if (lights.Count == 0)
                {
                    return;
                }

                List<int> positions = new List<int>();
                string[] strs = tbPosition.Text.Split(' ');
                try
                {
                    foreach (string s in strs)
                    {
                        positions.Add(int.Parse(s));
                    }
                }
                catch (Exception)
                {
                    return;
                }

                foreach (var item in positions)
                {
                    LightGroup lg = new LightGroup();
                    for (int i = 0; i < lights.Count; i++) {
                        lg.Add(new Light(lights[i].Time, lights[i].Action, item, lights[i].Color));
                    }

                    FileBusiness.CreateInstance().ReplaceControl(lg, FileBusiness.CreateInstance().midiArr);
                    FileBusiness.CreateInstance().WriteMidiFile(AppDomain.CurrentDomain.BaseDirectory + @"Project\" + FileUtils.projectInfo.Name+ @"\Light\" + System.IO.Path.GetFileNameWithoutExtension(FileUtils.pageName) + "_pl_"+ item+".mid",lg);

                    if (FileUtils.pageButtonModels[item].Count == 0)
                    {
                        Operation.Model.PageButtonModel pageButtonModel = new Operation.Model.PageButtonModel();
                        LightFilePlayModel lightFilePlayModel = new LightFilePlayModel();
                        lightFilePlayModel.FileName = System.IO.Path.GetFileNameWithoutExtension(FileUtils.pageName) + "_pl_" + item + ".mid";
                        lightFilePlayModel.Bpm = FileUtils.projectInfo.Bpm;
                        pageButtonModel._down.OperationModels.Add(lightFilePlayModel);
                        FileUtils.pageButtonModels[item].Add(pageButtonModel);
                    }
                    else {
                        //Operation.Model.PageButtonModel pageButtonModel = new Operation.Model.PageButtonModel();
                        LightFilePlayModel lightFilePlayModel = new LightFilePlayModel();
                        lightFilePlayModel.FileName = System.IO.Path.GetFileNameWithoutExtension(FileUtils.pageName) + "_pl_" + item + ".mid";
                        lightFilePlayModel.Bpm = FileUtils.projectInfo.Bpm;
                        //pageButtonModel._down.OperationModels.Add(lightFilePlayModel);
                        FileUtils.pageButtonModels[item][0]._down.OperationModels.Add(lightFilePlayModel);
                    }
                }

                FileUtils.sendLight(FileUtils.pageButtonModels);
            }
        }
    }
}

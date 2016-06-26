using Coft.ImageResizer.Models.Messanges;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Coft.ImageResizer.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ShowChoseFileDialogProceed>(this, OnShowChoseFileDialogProceed);
        }

        private void OnShowChoseFileDialogProceed(ShowChoseFileDialogProceed message)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zip Archive (*.zip)|*.zip";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true) {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ChosenNewFileProceed>(new ChosenNewFileProceed() { Filename = openFileDialog.FileName, SafeFilename = openFileDialog.SafeFileName }); 
            }
        }
    }
}

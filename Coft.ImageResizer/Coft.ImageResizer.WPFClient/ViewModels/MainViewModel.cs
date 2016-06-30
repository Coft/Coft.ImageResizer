using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using static Coft.ImageResizer.Models.Helpers.Enums;
using Coft.ImageResizer.Models.Services;
using Coft.ImageResizer.Models.Messanges;
using System.IO.Compression;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using Coft.ImageResizer.Models.Helpers;

namespace Coft.ImageResizer.WPFClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region props

        private ProcessingStatus processingStatus = ProcessingStatus.Initial;

        public ProcessingStatus ProcessingStatus
        {
            get { return processingStatus; }
            set { Set(ref processingStatus, value); }
        }

        private string createdFilename = string.Empty;

        public string CreatedFilename
        {
            get { return createdFilename; }
            set { Set(ref createdFilename, value); }
        }

        private string chosenFilename;

        public string ChosenFilename
        {
            get { return chosenFilename; }
            set { Set(ref chosenFilename, value); }
        }

        private string chosenSafeFilename;

        public string ChosenSafeFilename
        {
            get { return chosenSafeFilename; }
            set { Set(ref chosenSafeFilename, value); }
        }

        private bool isProcessing = false;

        public bool IsProcessing
        {
            get { return isProcessing; }
            set { Set(ref isProcessing, value); }
        }

        private int processingPercentage = 0;

        public int ProcessingPercentage
        {
            get { return processingPercentage; }
            set { Set(ref processingPercentage, value); }
        }

        private RelayCommand openFileCommand;

        public RelayCommand OpenFileCommand
        {
            get
            {
                if (openFileCommand == null)
                {
                    openFileCommand = new RelayCommand(
                        () => 
                        {
                            Messenger.Default.Send<ShowChoseFileDialogProceed>(new ShowChoseFileDialogProceed());
                        },
                        () => 
                        {
                            return !IsProcessing;
                        }
                    );
                }

                return openFileCommand;
            }
        }
            

        #endregion

        private ImageService ImageService;
        private ZipService ZipService;

        public MainViewModel() : this(new ImageService(), new ZipService())
        {
                
        }

        public MainViewModel(ImageService imageService, ZipService zipService)
        {
            ImageService = imageService;
            ZipService = zipService;

            MessengerInstance.Register<ChosenNewFileProceed>(this, OnChosenNewFileProceed);
        }

        #region messanges
        
        private void OnChosenNewFileProceed(ChosenNewFileProceed messange)
        {
            ChosenFilename = messange.Filename;
            ChosenSafeFilename = messange.SafeFilename;

            if (IsValidFile())
            {
                ProceedFileCommand();    
            }
            else
            {
                ShowError("Wybrano niepoprawny plik.");
            }
        }

        private void ShowError(string message)
        {
            System.Windows.MessageBox.Show(message);
        }

        #endregion

        #region commands

        public bool IsValidFile()
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(ChosenSafeFilename) && !string.IsNullOrEmpty(ChosenFilename))
            {
                isValid = true;
            }

            return isValid;
        }

        public void ProceedFileCommand()
        {
            IsProcessing = true;

            string newArchiveFilename = ChosenFilename.Replace(".zip", "_min.zip");

            using (FileStream zipToWrite = new FileStream(newArchiveFilename, FileMode.Create))
            using (FileStream zipToOpen = new FileStream(ChosenFilename, FileMode.Open))
            {
                ZipService.ParseZip(zipToOpen, zipToWrite);
            }

            IsProcessing = false;
        }

        #endregion

    }
}

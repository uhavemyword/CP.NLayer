namespace CP.NLayer.Client.WpfClient.Modules.Tools.Data
{
    using CP.NLayer.Client.WpfClient.Common;
    using CP.NLayer.Common;
    using CP.NLayer.Data;
    using CP.NLayer.Resources.UI;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class ViewModel : ViewModelBase, IRegionMemberLifetime
    {
        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }
            this.BackupCommand = new DelegateCommand(ExecuteBackupCommand, CanExecuteBackupCommand);
            this.RestoreCommand = new DelegateCommand(ExecuteRestoreCommand, CanExecuteRestoreCommand);
            this.BackupFileName = string.Format("{0:yyyyMMddHHmmss}.bak", DateTime.Now);
        }

        public IInteractionService InteractionService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IInteractionService>();
            }
        }

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime

        private string _backupLocation;
        public string BackupLocation
        {
            get { return _backupLocation; }
            set
            {
                if (!object.Equals(_backupLocation, value))
                {
                    _backupLocation = value;
                    this.OnPropertyChanged(() => this.BackupLocation);
                }
            }
        }

        private string _backupFileName;
        public string BackupFileName
        {
            get { return _backupFileName; }
            set
            {
                if (!object.Equals(_backupFileName, value))
                {
                    _backupFileName = value;
                    this.OnPropertyChanged(() => this.BackupFileName);
                    this.BackupCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _restoreFilePath;
        public string RestoreFilePath
        {
            get { return _restoreFilePath; }
            set
            {
                if (!object.Equals(_restoreFilePath, value))
                {
                    _restoreFilePath = value;
                    this.OnPropertyChanged(() => this.RestoreFilePath);
                    this.RestoreCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #region Backup

        public DelegateCommand BackupCommand { get; set; }

        public bool CanExecuteBackupCommand()
        {
            var temp = this.BackupFileName ?? string.Empty;
            return !string.IsNullOrEmpty(temp.Trim());
        }

        public void ExecuteBackupCommand()
        {
            BackupDatabase(false);
        }

        public bool BackupDatabase(bool closeProgressWindowOnCompleted)
        {
            if (!Directory.Exists(BackupLocation))
            {
                InteractionService.ShowError(string.Format(UiResources.FolderNotExist_1, BackupLocation), null);
                return false;
            }

            if (!IsFileNameValid(BackupFileName))
            {
                InteractionService.ShowError(UiResources.InvalidFileName, null);
                return false;
            }

            string filePath = Path.Combine(BackupLocation, BackupFileName);

            if (File.Exists(filePath))
            {
                bool overwrite = false;
                InteractionService.ShowConfirmation(UiResources.FileAlreadyExist, (confirmed) =>
                {
                    overwrite = confirmed;
                });

                if (!overwrite)
                {
                    return false;
                }
            }

            var progressViewModel = new ProgressViewModel();
            progressViewModel.Title = UiResources.Backup;
            progressViewModel.Message = string.Format(UiResources.BackupToFile_1, filePath);

            bool backupResult = true;
            InteractionService.ShowProgress(progressViewModel, (sender, e) =>
            {
                var dbManager = new DbManager();
                dbManager.BackupPercentComplete += progressViewModel.OnPercentChanged;
                dbManager.BackupComplete += progressViewModel.OnCompleted;
                new RadBusyModel().DoWorkAsync(() =>
                {
                    try
                    {
                        dbManager.BackupDatabase(MyDbUtility.GetConnection(), MyDbUtility.GetDatabaseName(), filePath);
                    }
                    catch (Exception ex)
                    {
                        backupResult = false;
                        progressViewModel.IsFinished = true;
                        progressViewModel.ValidationResults.Add(new ValidationResult(ex.MostInnerException().Message));
                    }
                });
            }, closeProgressWindowOnCompleted);

            return backupResult;
        }

        #endregion

        #region Restore

        public DelegateCommand RestoreCommand { get; set; }

        public bool CanExecuteRestoreCommand()
        {
            var temp = this.RestoreFilePath ?? string.Empty;
            return !string.IsNullOrEmpty(temp.Trim());
        }

        public void ExecuteRestoreCommand()
        {
            RestoreDatabase(false);
        }

        public bool RestoreDatabase(bool closeProgressWindowOnCompleted)
        {
            if (!File.Exists(RestoreFilePath))
            {
                InteractionService.ShowError(string.Format(UiResources.FileNotExist_1, RestoreFilePath), null);
                return false;
            }

            bool confirm = false;
            InteractionService.ShowConfirmation(string.Format(UiResources.RestoreConfirm_1, RestoreFilePath), (confirmed) =>
            {
                confirm = confirmed;
            });

            if (!confirm)
            {
                return false;
            }

            var progressViewModel = new ProgressViewModel();
            progressViewModel.Title = UiResources.Restore;

            progressViewModel.Message = string.Format(UiResources.RestoreFromFile_1, RestoreFilePath);

            bool restoreResult = true;
            InteractionService.ShowProgress(progressViewModel, (sender, e) =>
            {
                var dbManager = new DbManager();
                dbManager.RestorePercentComplete += progressViewModel.OnPercentChanged;
                dbManager.RestoreComplete += OnRestoreCompleted;
                dbManager.RestoreComplete += progressViewModel.OnCompleted;
                new RadBusyModel().DoWorkAsync(() =>
                    {
                        try
                        {
                            dbManager.RestoreDatabase(MyDbUtility.GetConnection(), MyDbUtility.GetDatabaseName(), RestoreFilePath);

                            // these two lines is used to fix the error raised after data restore
                            // error: "Resetting the connection results in a different state than the initial login."
                            MyDbUtility.GetConnection().Close();
                            MyDbUtility.GetConnection().Open();
                        }
                        catch (Exception ex)
                        {
                            restoreResult = false;
                            progressViewModel.IsFinished = true;
                            progressViewModel.ValidationResults.Add(new ValidationResult(ex.MostInnerException().Message));
                        }
                    });
            }, closeProgressWindowOnCompleted);

            return restoreResult;
        }

        #endregion

        private bool IsFileNameValid(string fileName)
        {
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }
            return true;
        }

        private void OnRestoreCompleted(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                // restore successfully

                // initial database again in case the database was restored to a old schema.
                GlobalCommands.InitializeDatabase(true);
            }
        }
    }
}
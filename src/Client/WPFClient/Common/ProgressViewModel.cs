namespace CP.NLayer.Client.WpfClient.Common
{
    public class ProgressViewModel : ViewModelBase
    {
        private bool _isFinished = false;

        /// <summary>
        /// Indicates whether the task is finished.
        /// No matter the task is completed successfully or aborted during processing,
        /// the value should be set to true to enable the UI.
        /// </summary>
        public bool IsFinished
        {
            get { return _isFinished; }
            set
            {
                if (!object.Equals(_isFinished, value))
                {
                    _isFinished = value;
                    if (_isFinished)
                    {
                        this.Percent = 100;
                    }
                    this.OnPropertyChanged(() => this.IsFinished);
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (!object.Equals(_title, value))
                {
                    _title = value;
                    this.OnPropertyChanged(() => this.Title);
                }
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (!object.Equals(_message, value))
                {
                    _message = value;
                    this.OnPropertyChanged(() => this.Message);
                }
            }
        }

        private string _completeMessage;
        public string CompleteMessage
        {
            get { return _completeMessage; }
            set
            {
                if (!object.Equals(_completeMessage, value))
                {
                    _completeMessage = value;
                    this.OnPropertyChanged(() => this.CompleteMessage);
                }
            }
        }

        private double _percent;
        public double Percent
        {
            get { return _percent; }
            set
            {
                if (!object.Equals(_percent, value))
                {
                    _percent = value;
                    this.OnPropertyChanged(() => this.Percent);
                    this.OnPropertyChanged(() => this.PercentFormatString);
                }
            }
        }

        public string PercentFormatString
        {
            get { return string.Format("Complete {0}%", (int)Percent); }
        }

        public delegate void PercentChangedEventHandler(double percent);

        public event PercentChangedEventHandler PercentChanged;

        public delegate void CompletedEventHandler(string message);

        public event CompletedEventHandler Completed;

        public void OnPercentChanged(double percent)
        {
            this.Percent = percent;
            var handler = PercentChanged;
            if (handler != null)
            {
                handler(percent);
            }
        }

        public void OnCompleted(string message)
        {
            this.CompleteMessage = message;
            this.IsFinished = true;
            var handler = Completed;
            if (handler != null)
            {
                handler(message);
            }
        }
    }
}
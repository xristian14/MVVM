using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MVVM.Models
{
    class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private int _Clicks;
        public int Clicks
        {
            get { return _Clicks; }
            set
            {
                _Clicks = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _dataSources = new ObservableCollection<string>(); //источники данных
        public ObservableCollection<string> DataSources
        {
            get { return _dataSources; }
            private set
            {
                _dataSources = value;
            }
        }
        
        public Model()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Task.Delay(1000).Wait();
                    Clicks++;
                }
            });
        }

        public void AddClicks()
        {
            Clicks++;
        }

        public void AddDataSource()
        {
            DataSources.Add(Clicks.ToString());
        }
    }
}

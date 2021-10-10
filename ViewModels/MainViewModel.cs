using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MVVM.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MVVM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Model _model;

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

        public MainViewModel()
        {
            _model = new Model();
            _model.PropertyChanged += Model_PropertyChanged;
            _model.DataSources.CollectionChanged += ModelDataSources_CollectionChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var fieldViewModel = this.GetType().GetProperty(e.PropertyName);
            var fieldModel = sender.GetType().GetProperty(e.PropertyName);
            fieldViewModel?.SetValue(this, fieldModel.GetValue(sender));
        }

        private ObservableCollection<string> _dataSources = new ObservableCollection<string>(); //источники данных
        public ObservableCollection<string> DataSources
        {
            get { return _dataSources; }
            private set
            {
                _dataSources = value;
                OnPropertyChanged();
            }
        }

        private void ModelDataSources_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataSources = (ObservableCollection<string>)sender;
        }
        
        public ICommand ClickAdd
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _model.AddDataSource();
                }, (obj) => Clicks < 100);
            }
        }
    }
}

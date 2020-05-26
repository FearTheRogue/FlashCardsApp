using MyAzureLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flash_Cards
{
    public class SingletonModel : INotifyPropertyChanged
    {
        private static SingletonModel model;
        private AzureLibrary _library;
        private ObservableCollection<CatagoryCell> _categories;

        public event PropertyChangedEventHandler PropertyChanged;

        // Stores DB info 
        public AzureLibrary Library
        {
            get => _library;
            set
            {
                if (_library == value) return;
                    _library = value;
                OnPropertyChanged();
            }
        }

        // Stores Data from DB to a collection
        public ObservableCollection<CatagoryCell> Categories 
        {
            get => _categories;
            set
            {
                if (_categories == value) return;
                _categories = value;

                OnPropertyChanged();
            }
        }

        public static SingletonModel SingletonInstance
        {
            get
            {
                if(model == null)
                {
                    model = new SingletonModel();
                }
                return model;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

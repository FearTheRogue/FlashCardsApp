using MyAzureLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flash_Cards
{
    public class SingletonModel : INotifyPropertyChanged
    {
        private static SingletonModel model;
        private AzureLibrary _library;

        public event PropertyChangedEventHandler PropertyChanged;

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

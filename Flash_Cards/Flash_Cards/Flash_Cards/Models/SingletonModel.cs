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
        private List<CatagoryCell> _categories;
        private Question _question;

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

        public List<CatagoryCell> Categories 
        {
            get => _categories;
            set
            {
                if (_categories == value) return;
                _categories = value;

                OnPropertyChanged();
            }
        }

        public Question Question
        {
            get => _question;
            set
            {
                if (_question == value) return;
                _question = value;
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

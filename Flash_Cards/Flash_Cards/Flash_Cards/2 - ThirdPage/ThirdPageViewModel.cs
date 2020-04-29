using System;

using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using MVVMBase;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private ObservableCollection<CardCell> _cards;
        private IMainPageHelper _viewHelper;

        public ObservableCollection<CardCell> QuestionCards
        {
            get => _cards;
            set
            {
                if (_cards == value) return;
                _cards = value;
                OnPropertyChanged();
            }
        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            QuestionCards = new ObservableCollection<CardCell>()
            {
                new CardCell("What is the name of a thing that walks","Humans")
            };
        }
    }
}

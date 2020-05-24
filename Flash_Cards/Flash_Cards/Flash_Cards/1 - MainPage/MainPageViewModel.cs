﻿using MVVMBase;
using MyAzureLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Flash_Cards
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMainPageHelper _viewHelper;
        public ObservableCollection<CatagoryCell> _catagory;
        private string _titleString = "Nothing Selected";
        private CatagoryCell _selectedString;
        private int _selectedRow = 0;

        public AzureLibrary azure;
        List<CardCatagories> _list = new List<CardCatagories>();
        List<CatagoryCell> _appCategories = new List<CatagoryCell>();

        public ObservableCollection<CatagoryCell> CatagoryCards
        {
            get => _catagory;
            set
            {
                if (_catagory == value)  { return;  }
                _catagory = value;
                OnPropertyChanged();
            }
        }

        public List<CardCatagories> Categories
        {
            get => _list;
            set
            {
                if(_list == value) { return; }
                _list = value;
                OnPropertyChanged();
            }
        }

        public List<CatagoryCell> AppCategories
        {
            get => _appCategories;
            set
            {
                if(_appCategories == value) { return; }
                _appCategories = value;
                OnPropertyChanged();
            }
        }

        public string TitleString
        {
            get => _titleString ?? "Nothing Selected";
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        public CatagoryCell SelectedString 
        {
            get => _selectedString;
            set
            {
                if (_selectedString == value) return;
                _selectedString = value;

                TitleString = _selectedString.Catagory ?? "Nothing Selected";
            }
        }

        public int SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (_selectedRow == value) return;
                _selectedRow = value;
                OnPropertyChanged();
            }
        }

        public async Task ItemSelectionChangedAsync (CatagoryCell card)
        {
            var nextPage = new ThirdPage();
            await Navigation.PushAsync(nextPage);
            nextPage.Title = card.Catagory + " Catagory Page";
            string temp = card.Id.ToString();
            MessagingCenter.Send(this, "new", temp);
        }

        public void AddNewCard(CatagoryCell newCardString)
        {
            CatagoryCards.Add(newCardString);
            _viewHelper.ScrollToObject(newCardString);
        }

        public async Task AddCardButtonAsync()
        {
            var addPage = new AddCardPage();
            await Navigation.PushAsync(addPage);
        }
        
        public ICommand DeleteCommand { get; private set; }

        public void DeleteItem(CatagoryCell c)  { CatagoryCards.Remove(c); }

        public async Task Temp()
        {
            await azure.QueryItemsAsync(Categories);
        }

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            azure = SingletonModel.SingletonInstance.Library;

            Task.Run(async () =>
            {
                await Temp();
            }).Wait();

            foreach (CardCatagories catagories in Categories)
            {
                AppCategories.Add(new CatagoryCell(catagories.Id, catagories.Catagory, catagories.CardCount, catagories.Questions, catagories.Answers));

                SingletonModel.SingletonInstance.Categories = AppCategories;
            }

            /*
            CatagoryCell catagoryCell = new CatagoryCell()
            {
                Catagory = "SOFT262",
                CardCount = 2,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "SOFT262 question 1"},
                    new Question{CardQuestion = "SOFT262 question 2"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnswer = "SOFT262 answer 1"},
                    new Answer{CardAnswer = "SOFT262 answer 2"},
                },
            };
            */

            //CatagoryCards = new ObservableCollection<CatagoryCell>()
            //{ 
            //    new CatagoryCell("SOFT262", 4),
            //    new CatagoryCell("AINT255", 4),
            //    new CatagoryCell("Dinosaurs", 4),
            //    new CatagoryCell("Food ", 4),
            //    new CatagoryCell("Netflix", 4),
            //    new CatagoryCell("Sport", 4),
            //    new CatagoryCell("Oop", 4)

            //    //new CatagoryCell("SOFT262", "question 1","answer 1"),
            //    //new CatagoryCell("AINT255", "question 2", "answer 2"),
            //    //new CatagoryCell("Dinosaurs", "question 3", "answer 3"),
            //    //new CatagoryCell("Food", "question 4", "answer 4"),

            //};

            DeleteCommand = new Command<CatagoryCell>(execute: (c) => DeleteItem(c));
        }
    }
}

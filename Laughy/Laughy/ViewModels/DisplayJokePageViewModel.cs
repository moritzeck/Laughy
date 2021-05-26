﻿using Laughy.Logic.Integration.LaughyWorkflow.Contracts;
using Laughy.Models.UiModels;
using Laughy.NavigationService.Interfaces;
using Laughy.ViewModels.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Laughy.ViewModels
{
    public class DisplayJokePageViewModel : ViewModelBase, IDisplayJokePageViewModel
    {
        //Fields
        private readonly IJokeWorkflow _jokeWorkflow;


        //Properties
        private JokeUiModel _joke;
        public JokeUiModel Joke
        {
            get { return _joke; }
            set
            {
                _joke = value;               
                OnPropertyChanged(nameof(Joke));
            }
        }       

        private string _firstHeadline;
        public string FirstHeadline
        {
            get { return _firstHeadline; }
            set
            {
                _firstHeadline = value;
                OnPropertyChanged(nameof(FirstHeadline));
            }
        }

        private string _secondHeadline;
        public string SecondHeadline
        {
            get { return _secondHeadline; }
            set
            {
                _secondHeadline = value;
                OnPropertyChanged(nameof(SecondHeadline));
            }
        }
        

        //Constructor
        public DisplayJokePageViewModel(INavigator navigator, IJokeWorkflow jokeWorkflow) : base(navigator)
        {
            //Assignments
            _jokeWorkflow = jokeWorkflow;


            //Commands
            GetJokeCommand = new AsyncCommand(GetJoke);         
        }


        //Private methods
        private void ManageHeadlines()
        {
            if (String.IsNullOrWhiteSpace(Joke.SecondPart))
            {
                FirstHeadline = "Joke:";
                SecondHeadline = "";
            }

            else
            {
                FirstHeadline = "First part:";
                SecondHeadline = "Second part:";
            }
        }

        private void SavePreviousJoke()
        {
            PreviousJokeToBeDisplayed = PreviousJokeToBeSaved;

            PreviousJokeToBeSaved = Joke;
        }


        //Public methods
        public async Task GetJoke()
        {
            Joke = await _jokeWorkflow.GetJokeByCategory(Category);

            ManageHeadlines();

            SavePreviousJoke();
        }

        public override void LikeJoke()
        {
            if(Joke != EmptyJoke || !Joke.Favourite)
            {
                Joke.Favourite = true;

                _jokeWorkflow.CreateOrLikeJoke(Joke);
            }        
        }

        public override void DisplayPreviousJoke()
        {
            if(PreviousJokeToBeDisplayed != null)
            {
                PreviousJokeToBeSaved = PreviousJokeToBeDisplayed;

                Joke = PreviousJokeToBeDisplayed;
            }

            else
            {
                Joke = PreviousJokeToBeSaved;               
            }          

            ManageHeadlines();
        }
        public override void SearchJoke()
        {
            
        }


        //Tasks
        public override Task BeforeFirstShown()
        {
            GetJoke().ConfigureAwait(false);

            return Task.CompletedTask;
        }

    }
}

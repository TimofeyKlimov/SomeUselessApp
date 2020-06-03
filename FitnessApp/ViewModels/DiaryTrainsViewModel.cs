using DevExpress.Mvvm;
using FitnessApp.Bus;
using FitnessApp.Events;
using FitnessApp.Message;
using FitnessApp.Models;
using FitnessApp.Pages;
using FitnessApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessApp.ViewModels
{
    public class DiaryTrainsViewModel : BindableBase
    {
        public User User { get; set; }
        public TrainPlan SelectedTrain { get; set; }

        private PageService _pageService;

        private readonly MessageBus messageBus;
        public DiaryTrainsViewModel(MessageBus messageBus,PageService pageService)
        {
            this.messageBus = messageBus;
            this._pageService = pageService;
            messageBus.Receive<UserMessage>(this, s =>
            {
                User = s.User;
                return Task.CompletedTask;
            });
        }

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.Back(this);
        });

        public ICommand AddDailyTrain => new AsyncCommand( async() =>
        {
            _pageService.Navigate(new AddDailyTrain());
            await messageBus.SendTo<AddDailyTrainViewModel>(new TrainMessage(SelectedTrain));
            await messageBus.SendTo<AddDailyTrainViewModel>(new UserMessage(User));
        },() => User?.TrainPlans.Count > 0 && SelectedTrain !=null);
    }
}

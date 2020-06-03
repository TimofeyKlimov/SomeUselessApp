using DevExpress.Mvvm;
using FitnessApp.Bus;
using FitnessApp.Events;
using FitnessApp.Models;
using FitnessApp.Pages;
using FitnessApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessApp.ViewModels
{
    public class StartViewModel : BindableBase
    {
        private readonly PageService pageService;

        private User _user;

        private readonly MessageBus messageBus;
        public StartViewModel(MessageBus messageBus, PageService pageService)
        {
            this.messageBus = messageBus;
            this.pageService = pageService;
            _= messageBus.Receive<UserMessage>(this, userMessage =>
            {
                 _user = userMessage.User;
                 return Task.CompletedTask;
            });
        }

        public ICommand GetTrainsPlan => new AsyncCommand( async() =>
        {
            pageService.Navigate(new TrainsPlan());
            await messageBus.SendTo<TrainsPlanViewModel>(new UserMessage(_user));
        });

        public ICommand GetTrains => new AsyncCommand(async () =>
        {
            pageService.Navigate(new TrainsDiary());
            await messageBus.SendTo<DiaryTrainsViewModel>(new UserMessage(_user));
        });
    }
}

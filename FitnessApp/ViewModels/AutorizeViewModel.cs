using DevExpress.Mvvm;
using FitnessApp.Bus;
using FitnessApp.Events;
using FitnessApp.Models;
using FitnessApp.Pages;
using FitnessApp.Services;
using System.Windows.Input;

namespace FitnessApp.ViewModels
{
    public class AutorizeViewModel : BindableBase
    {
        #region services
        private readonly Repository repository;
        private  PageService pageService;
        private readonly MessageBus messageBus;
        #endregion
        public string Name { get; set; }
        public AutorizeViewModel(MessageBus messageBus,Repository repository,PageService pageService)
        {
            this.repository = repository;
            this.pageService = pageService;
            this.messageBus = messageBus;
        }

        public ICommand Autorize => new DelegateCommand(() =>
        {
            var user = repository.FindEntity<User>(s => s.Name == Name);
            if (user != null)
            {
                pageService.Navigate(new StartPage());
                _= messageBus.SendTo<StartViewModel>(new UserMessage(user));
            }
            else
            {
                var registrateUser = new User(Name);
                repository.Save(registrateUser);
                pageService.Navigate(new StartPage());
                _=messageBus.SendTo<StartViewModel>(new UserMessage(registrateUser));
            }

        }, () => !string.IsNullOrEmpty(Name));
    }
}

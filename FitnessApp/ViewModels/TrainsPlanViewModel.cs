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
    public class TrainsPlanViewModel : BindableBase
    {
        private readonly PageService pageService;
        public User User { get; set; }

        private readonly MessageBus messageBus;
        private readonly Repository repository;
        public TrainsPlanViewModel(PageService pageService,MessageBus messageBus,Repository repository)
        {
            this.repository = repository;
            this.messageBus = messageBus;
            this.pageService = pageService;
            messageBus.Receive<UserMessage>(this, userMessage =>
             {
                 User = userMessage.User;
                 return Task.CompletedTask;
             });
        }
        public ICommand Back => new DelegateCommand(() =>
        {
            pageService.Back(this);
        });
        public ICommand Delete => new DelegateCommand<TrainPlan>((plan) =>
        {
            User.TrainPlans.Remove(plan);
            repository.Save(User);
        });
        public ICommand AddTrain => new AsyncCommand( async () =>
        {
            pageService.Navigate(new AddTrainPlan());
            await messageBus.SendTo<AddTrainPlanViewModel>(new UserMessage(User));
        });
    }
}

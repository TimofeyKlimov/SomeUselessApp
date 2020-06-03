using DevExpress.Mvvm;
using FitnessApp.Bus;
using FitnessApp.Events;
using FitnessApp.Models;
using FitnessApp.Pages;
using FitnessApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FitnessApp.ViewModels
{
    public class AddTrainPlanViewModel : BindableBase
    {
        private PageService _pageService;

        private Repository _repository;
        public string TrainName { get; set; }

        private readonly MessageBus messageBus;
        public string ExerciseName { get; set; }

        private List<PlanningExercise> planningExercises;

        private User _user;
        public AddTrainPlanViewModel(MessageBus messageBus,PageService pageService,Repository repository)
        {
            this.messageBus = messageBus;
            this._pageService = pageService;
            this._repository = repository;
            messageBus.Receive<UserMessage>(this, s =>
            {
                _user = s.User;
                return Task.CompletedTask;
            });
            planningExercises = new List<PlanningExercise>();
        }

        public ICommand AddExercise => new DelegateCommand(() =>
        {
            planningExercises.Add(new PlanningExercise() { Title = ExerciseName });
            var msg = string.Format("Упражнение {0} добавлено", ExerciseName);
            MessageBox.Show(msg); 
            ExerciseName = "";
        }, () => !string.IsNullOrEmpty(ExerciseName));

        public ICommand AddTrain => new AsyncCommand( async () =>
        {
            var train = new TrainPlan()
            {
                Title = TrainName,
                PlanningExercises = new ObservableCollection<PlanningExercise>(planningExercises)
            };
            _user.TrainPlans.Add(train);
            _repository.Save(_user);
            _pageService.Navigate(new TrainsPlan());
            await messageBus.SendTo<TrainsPlanViewModel>(new UserMessage(_user));
        }, () => planningExercises.Count > 0, !string.IsNullOrEmpty(TrainName));
    }
}

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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessApp.ViewModels
{
    public class AddDailyTrainViewModel : BindableBase
    {
        private User _user;
        public TrainPlan TrainPlan { get; set; }
        public string Weight { get; set; }
        public int Reps { get; set; }

        private IDictionary<string, List<WeightAndReps>> _dict;

        private readonly Repository repository;
        private readonly MessageBus messageBus;
        private readonly PageService pageService;
        private readonly EventBus eventBus;
        public AddDailyTrainViewModel(MessageBus messageBus,Repository repository,
            PageService pageService,EventBus eventBus)
        {
            this.eventBus = eventBus;
            messageBus.Receive<UserMessage>(this, s =>
            {
                _user = s.User;
                return Task.CompletedTask;
            });
            messageBus.Receive<TrainMessage>(this, s =>
            {
                TrainPlan = s.TrainPlan;
                return Task.CompletedTask;
            });
            _dict = new Dictionary<string, List<WeightAndReps>>();
            this.repository = repository;
            this.messageBus = messageBus;
            this.pageService = pageService;
        }

        public ICommand AddExercise => new DelegateCommand<PlanningExercise>((exercise) =>
        {
            var item = _dict.Where(s => s.Key == exercise.Title).FirstOrDefault();
            if(item.Key == null)
            {
                var itemsList = new List<WeightAndReps>()
                {
                    new WeightAndReps()
                    {
                        Weight = double.Parse(Weight),
                        Reps = Reps
                    }
                };
                _dict.Add(new KeyValuePair<string, List<WeightAndReps>>(exercise.Title, itemsList));
                MessageBox.Show(string.Format("Подход №1 в упражнении {0} добавлен", exercise.Title));
            }
            else
            {
                item.Value.Add(new WeightAndReps()
                {
                    Weight = double.Parse(Weight),
                    Reps = Reps
                });
                MessageBox.Show(string.Format("Подход №{0} в упражнении {1} добавлен", item.Value.Count, exercise.Title));
            }
        },(exercise) => !string.IsNullOrEmpty(Weight) || Reps > 0);

        public ICommand AddTrain => new AsyncCommand( async () =>
        {
            var title = TrainPlan.Title;
            var train = new DailyTrain()
            {
                Day = DateTime.Now.ToShortDateString(),
                Title = TrainPlan.Title
            };
            foreach (var exercise in _dict)
            {
                var ex = new Exercise()
                {
                    Title = exercise.Key,
                    WeightAndReps = exercise.Value
                };
                train.Exercises.Add(ex);
            }
            _user.DailyTrains.Add(train);
            _user.DailyTrains.OrderBy(s => s.Day);
             repository.Save(_user);
            pageService.Navigate(new TrainsDiary());
            await messageBus.SendTo<DiaryTrainsViewModel>(new UserMessage(_user));
        });
    }
}

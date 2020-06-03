using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime RegistrateTime { get; set; }

        public ObservableCollection<TrainPlan> TrainPlans { get; set; } = new ObservableCollection<TrainPlan>();

        public List<DailyTrain> DailyTrains { get; set; } = new List<DailyTrain>();

        public User(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            RegistrateTime = DateTime.Now;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class TrainPlan
    {
        public string Title { get; set; }

        public ObservableCollection<PlanningExercise> PlanningExercises { get; set; } = new ObservableCollection<PlanningExercise>(); 
    }

    public class PlanningExercise
    {
        public string Title { get; set; }
    }
}

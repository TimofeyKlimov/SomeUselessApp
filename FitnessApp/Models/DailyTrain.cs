using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class DailyTrain
    {
        public string Day { get; set; }

        public string Title { get; set; }

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }

    public class Exercise
    {
        public string Title { get; set; }

        public List<WeightAndReps> WeightAndReps { get; set; } = new List<WeightAndReps>();
    }

    public class WeightAndReps
    {
        public double Weight { get; set; }

        public int Reps { get; set; }
    }
}

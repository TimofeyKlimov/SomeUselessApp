using FitnessApp.Bus;
using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Message
{
    public class TrainMessage : IMessage
    {
        public TrainPlan TrainPlan { get; set; }
        public TrainMessage(TrainPlan trainPlan)
        {
            TrainPlan = trainPlan;
        }
    }
}

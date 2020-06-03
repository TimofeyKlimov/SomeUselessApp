using FitnessApp.DI;
using FitnessApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp
{
    public class Locator
    {
        public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();

        public AutorizeViewModel AutorizeViewModel => IoC.Resolve<AutorizeViewModel>();

        public StartViewModel StartViewModel => IoC.Resolve<StartViewModel>();

        public TrainsPlanViewModel TrainsPlanViewModel => IoC.Resolve<TrainsPlanViewModel>();

        public AddTrainPlanViewModel AddTrainPlanViewModel => IoC.Resolve<AddTrainPlanViewModel>();

        public DiaryTrainsViewModel DiaryTrainsViewModel => IoC.Resolve<DiaryTrainsViewModel>();

        public AddDailyTrainViewModel AddDailyTrainViewModel => IoC.Resolve<AddDailyTrainViewModel>();
    }
}

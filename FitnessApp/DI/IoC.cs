using FitnessApp.Bus;
using FitnessApp.Services;
using FitnessApp.ViewModels;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.DI
{
    public static class IoC
    {
        private static readonly IServiceProvider _provider;
        static IoC()
        {
            var services = new ServiceCollection();

            ///ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<AutorizeViewModel>();
            services.AddSingleton<StartViewModel>();
            services.AddTransient<TrainsPlanViewModel>();
            services.AddTransient<AddTrainPlanViewModel>();
            services.AddTransient<DiaryTrainsViewModel>();
            services.AddTransient<AddDailyTrainViewModel>();
            ///Services
            services.AddSingleton<EventBus>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<PageService>();
            services.AddSingleton(new LiteDatabase("MyDatabase.db"));
            services.AddSingleton<Repository>();
           

            _provider = services.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            return _provider.GetRequiredService<T>();
        }
    }
}

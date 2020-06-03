using DevExpress.Mvvm;
using FitnessApp.Pages;
using FitnessApp.Services;
using System;
using System.Windows.Controls;

namespace FitnessApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public Page CurrentPage { get; set; }

        public MainViewModel(PageService pageService)
        {
            pageService.PageChanged += PageService_PageChanged;
            pageService.Navigate(new AutorizePage());
        }

        private void PageService_PageChanged(Type obj)
        {
            CurrentPage = (Page)Activator.CreateInstance(obj);
        }
    }
}

using FitnessApp.Pages;
using FitnessApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FitnessApp.Services
{
    public class PageService
    {
        public event Action<Type> PageChanged;
        private Stack<Page> _pages;
        public PageService()
        {
            _pages = new Stack<Page>();
        }
        public void Navigate(Page page)
        {
            _pages.Push(page);
            this.PageChanged?.Invoke(page.GetType());
        }

        public void Back(object page = null)
        {
            if(page is TrainsPlanViewModel || page is DiaryTrainsViewModel)
            {
                PageChanged?.Invoke(typeof(StartPage));
                return;

            }
            _ = _pages.Pop();
            var previouspage = _pages.Peek();
            PageChanged?.Invoke(previouspage.GetType());
        }

    }
}

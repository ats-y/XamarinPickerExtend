using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace ExtendControl.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        public ViewModelBase()
        {
        }

        public void Destroy()
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

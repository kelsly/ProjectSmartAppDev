using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class SplashViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        public SplashViewModel(IMvxNavigationService NavigationService)
        {
            _navigationService = NavigationService;
        }

        public MvxCommand GoToApp
        {
            get
            {
                return new MvxCommand(() => ToApp());
            }
        }

        public void ToApp()
        {
            try
            {

                _navigationService.Navigate<BooksTabViewModel>();
            }
            catch (Exception e) { throw e; }
        }
    }
}

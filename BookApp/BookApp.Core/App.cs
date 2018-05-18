using BookApp.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using System;

namespace BookApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            try
            {
                base.Initialize();

                CreatableTypes()
                    .EndingWith("Repository")
                    .AsInterfaces()
                    .RegisterAsLazySingleton();
                CreatableTypes()
                   .EndingWith("Service")
                   .AsInterfaces()
                   .RegisterAsLazySingleton();

                RegisterNavigationServiceAppStart<SplashViewModel>();

                //RegisterAppStart<BestsellersTableViewModel>();
            } catch (Exception e)
            {
                throw e;
            }
        }
    }
}

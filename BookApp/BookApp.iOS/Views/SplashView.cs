using Airbnb.Lottie;
using BookApp.Core.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.iOS.Views;
using System;
using UIKit;

namespace BookApp.iOS
{
    [MvxFromStoryboard(StoryboardName = "Splash")]
    public partial class SplashView : MvxViewController<SplashViewModel>
    {
        public SplashView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            try
            {
                base.ViewDidLoad();
                LOTAnimationView animation = LOTAnimationView.AnimationNamed("data");
                animation.Frame = new CGRect(0, 0, this.View.Bounds.Size.Width, this.View.Bounds.Size.Height);
                animation.ContentMode = UIViewContentMode.ScaleAspectFit;
                this.View.AddSubview(animation);
                animation.AnimationProgress = 0;
                animation.PlayWithCompletion((animationFinished) =>
                {
                    ViewModel.ToApp();
                });
                //animation.Play();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
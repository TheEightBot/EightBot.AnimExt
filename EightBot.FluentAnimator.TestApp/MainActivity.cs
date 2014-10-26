using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Animation;
using Android.Views.Animations;

namespace EightBot.FluentAnimator.TestApp
{
	[Activity (Label = "EightBot.FluentAnimator.TestApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var fadeIn = FindViewById<Button> (Resource.Id.fadeIn);
			var fadeOut = FindViewById<Button> (Resource.Id.fadeOut);

			var wiggle = FindViewById<Button> (Resource.Id.wiggle);

			var flip = FindViewById<ToggleButton> (Resource.Id.flip);

			var spin = FindViewById<ToggleButton> (Resource.Id.spin);

			var pulsate = FindViewById<ToggleButton> (Resource.Id.pulsate);

			var image = FindViewById<ImageView> (Resource.Id.androidy);
			
			fadeIn.Click += delegate {
				image.FadeIn();
			};

			fadeOut.Click += delegate {
				image.FadeOut();
			};

			wiggle.Click += delegate {
				image.Wiggle(10);
			};

			flip.CheckedChange += (s,e) => {
				if(e.IsChecked)
					image.Flip(EightBot.FluentAnimator.FluentPropertyAnimation.FlipDirection.LeftToRight, interpolator: new AnticipateInterpolator());
				else
					image.FlipReturn(EightBot.FluentAnimator.FluentPropertyAnimation.FlipDirection.LeftToRight, interpolator: new AnticipateInterpolator());
			};

			ValueAnimator spinAnimation = null;
			spin.CheckedChange += (sender, e) => {
				if(e.IsChecked)
					spinAnimation = image.Spin(FluentPropertyAnimation.DefaultDuration * 2, interpolator: new AnticipateOvershootInterpolator());
				else
					spinAnimation.End();
			};


			ValueAnimator pulsateAnimation = null;
			pulsate.CheckedChange += (sender, e) => {
				if(e.IsChecked)
					pulsateAnimation = image.Pulsate(1.5f);
				else
					pulsateAnimation.End();
			};
		}
	}
}



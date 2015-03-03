using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Animation;
using Android.Views.Animations;
using EightBot.AnimExt.Droid;

namespace EightBot.AnimExt.TestApp.Droid
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
			var fade = FindViewById<ToggleButton> (Resource.Id.fade);

			var wiggle = FindViewById<Button> (Resource.Id.wiggle);

			var squish = FindViewById<Button> (Resource.Id.squish);

			var jiggle = FindViewById<Button> (Resource.Id.jiggle);

			var flip = FindViewById<ToggleButton> (Resource.Id.flip);

			var spin = FindViewById<ToggleButton> (Resource.Id.spin);

			var pulsate = FindViewById<ToggleButton> (Resource.Id.pulsate);

			var slide = FindViewById<Button> (Resource.Id.slide);

			var reset = FindViewById<Button> (Resource.Id.reset);

			var image = FindViewById<ImageView> (Resource.Id.androidy);
			
			fade.CheckedChange += (s,e) => {
				if(e.IsChecked)
					image.Fade(AnimExt.FadeType.In);
				else
					image.Fade(AnimExt.FadeType.Out);
			};

			wiggle.Click += delegate {
				image.Wiggle(EightBot.AnimExt.Direction.Horizontal, 15f);
			};

			squish.Click += delegate {
				image.Squish(EightBot.AnimExt.Direction.Vertical, .3f);
			};

			jiggle.Click += delegate {
				image.JiggleBilly(scaleAmount: 1.6f, jiggleCount: 6, duration: 2400);
			};

			flip.CheckedChange += (s,e) => {
				if(e.IsChecked)
					image.Flip(FlipDirection.LeftToRight, interpolator: new AnticipateInterpolator());
				else
					image.Flip(FlipDirection.LeftToRight, true, interpolator: new AnticipateInterpolator());
			};

			ValueAnimator spinAnimation = null;
			spin.CheckedChange += (sender, e) => {

				var randomDirection = new Random();

				if(e.IsChecked)
					spinAnimation = image.Spin(AnimExt.SpinDirection.CounterClockwise, AnimationExtensions.DefaultAnimationDuration * 2, interpolator: new AnticipateOvershootInterpolator());
				else
					spinAnimation.End();
			};
				
			slide.Click += (sender, e) => {
				var enumValue = (SlideDirection)((new Random()).Next(0, Enum.GetNames(typeof(SlideDirection)).Length));
				image.Slide(enumValue, AnimationExtensions.DefaultAnimationDuration * 2);
			};

			reset.Click += (sender, e) => {
				image.ResetAnimation();
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



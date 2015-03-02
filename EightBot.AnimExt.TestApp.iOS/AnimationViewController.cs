using System;
using UIKit;
using System.Drawing;
using EightBot.AnimExt.iOS;

namespace EightBot.AnimExt.TestApp.iOS
{
	public class AnimationViewController : UIViewController
	{
		private const float ControlHeight = 32f, Padding = 8f;

		public AnimationViewController ()
		{
			View.BackgroundColor = UIColor.White;


			var imageHeight = (float)View.Bounds.Width;

			var image = new UIImageView (UIImage.FromBundle ("Culumnodingus"));

			image.Frame = new RectangleF (0f, (float)View.Bounds.Height - imageHeight, (float)View.Bounds.Width, imageHeight);
			image.ContentMode = UIViewContentMode.ScaleAspectFit;

			Add (image);

			var fadeOut = new UIButton (UIButtonType.System){ };
			fadeOut.Frame = new RectangleF (0f, 20f, (float)View.Bounds.Width, ControlHeight);

			fadeOut.SetTitle ("Fade Out", UIControlState.Normal);

			fadeOut.TouchUpInside += (sender, e) => { image.FadeOut(); };

			Add (fadeOut);

			var fadeIn = new UIButton (UIButtonType.System){ };
			fadeIn.Frame = new RectangleF (0f, (float)fadeOut.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			fadeIn.SetTitle ("Fade In", UIControlState.Normal);

			fadeIn.TouchUpInside += (sender, e) => { image.FadeIn(); };

			Add (fadeIn);

			var spin = new UIButton (UIButtonType.System){ };
			spin.Frame = new RectangleF (0f, (float)fadeIn.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			spin.SetTitle ("Spin", UIControlState.Normal);

			spin.TouchUpInside += async (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Spin Started");
				await image.Spin (SpinDirection.Clockwise, .6);
				System.Diagnostics.Debug.WriteLine("Spin Completed");
			};

			Add (spin);

			var flip = new UIButton (UIButtonType.System){ };
			flip.Frame = new RectangleF (0f, (float)spin.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			flip.SetTitle ("Flip", UIControlState.Normal);

			flip.TouchUpInside += async (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Flip Started");
				await image.Flip (FlipDirection.LeftToRight, .6);
				await image.Flip (FlipDirection.LeftToRight, .6);
				System.Diagnostics.Debug.WriteLine("Flip Completed");
			};

			Add (flip);

			var scale = new UIButton (UIButtonType.System){ };
			scale.Frame = new RectangleF (0f, (float)flip.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			scale.SetTitle ("Scale", UIControlState.Normal);

			scale.TouchUpInside += async (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Scale Started");
				await image.Scale (.5f);
				await image.Scale (1.0f);
				await image.Scale (1.5f);
				await image.Scale (2.0f);
				System.Diagnostics.Debug.WriteLine("Scale Completed");
			};

			Add (scale);

			var slide = new UIButton (UIButtonType.System){ };
			slide.Frame = new RectangleF (0f, (float)scale.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			slide.SetTitle ("Slide", UIControlState.Normal);

			slide.TouchUpInside += async (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Slide Started");
				await image.Slide(SlideDirection.FromBottom, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.FromLeft, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.FromRight, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.FromTop, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.ToBottom, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.ToLeft, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.ToRight, .6d);
				await image.Reset();
				await image.Slide(SlideDirection.ToTop, .6d);
				await image.Reset();
				System.Diagnostics.Debug.WriteLine("Slide Completed");
			};

			Add (slide);

			var reset = new UIButton (UIButtonType.System){ };
			reset.Frame = new RectangleF (0f, (float)slide.Frame.Bottom + Padding, (float)View.Bounds.Width, ControlHeight);

			reset.SetTitle ("Reset", UIControlState.Normal);

			reset.TouchUpInside += async (sender, e) => {
				System.Diagnostics.Debug.WriteLine("Reset Started");
				await image.Reset();
				System.Diagnostics.Debug.WriteLine("Reset Completed");
			};

			Add (reset);
		}
	}
}


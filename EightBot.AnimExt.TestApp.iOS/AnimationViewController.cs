using System;
using UIKit;
using System.Drawing;
using EightBot.AnimExt.iOS;

namespace EightBot.AnimExt.TestApp.iOS
{
	public class AnimationViewController : UIViewController
	{
		public AnimationViewController ()
		{
			View.BackgroundColor = UIColor.White;


			var imageHeight = (float)View.Bounds.Width;

			var image = new UIImageView (UIImage.FromBundle ("Culumnodingus"));

			image.Frame = new RectangleF (0f, (float)View.Bounds.Height - imageHeight, (float)View.Bounds.Width, imageHeight);
			image.ContentMode = UIViewContentMode.ScaleAspectFit;

			Add (image);

			var fadeOut = new UIButton (UIButtonType.System){ };
			fadeOut.Frame = new RectangleF (0f, 20f, (float)View.Bounds.Width, 44f);

			fadeOut.SetTitle ("Fade Out", UIControlState.Normal);

			fadeOut.TouchUpInside += (sender, e) => { image.FadeOut(); };

			Add (fadeOut);

			var fadeIn = new UIButton (UIButtonType.System){ };
			fadeIn.Frame = new RectangleF (0f, (float)fadeOut.Frame.Bottom + 8f, (float)View.Bounds.Width, 44f);

			fadeIn.SetTitle ("Fade In", UIControlState.Normal);

			fadeIn.TouchUpInside += (sender, e) => { image.FadeIn(); };

			Add (fadeIn);

			var spin = new UIButton (UIButtonType.System){ };
			spin.Frame = new RectangleF (0f, (float)fadeIn.Frame.Bottom + 8f, (float)View.Bounds.Width, 44f);

			spin.SetTitle ("Spin", UIControlState.Normal);

			spin.TouchUpInside += async (sender, e) => await image.Spin ();

			Add (spin);
		}
	}
}


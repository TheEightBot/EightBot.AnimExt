using System;
using UIKit;
using System.Threading.Tasks;
using CoreGraphics;
using System.Linq;
using CoreAnimation;

namespace EightBot.AnimExt.iOS
{

	public static class AnimationExtensions
	{
		public const double DefaultAnimationDuration = .2d;

		public static Task Fade(this UIView view, FadeType fadeType = FadeType.In, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){
			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					switch (fadeType) {
						case FadeType.In:
							if(view.Alpha != 1f)
								view.Alpha = 1f;
							break;
						case FadeType.Out:
							if(view.Alpha != 0f)
								view.Alpha = 0f;
							break;
					}


				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;

		}
			
		public static async Task Spin(this UIView view, SpinDirection spinDirection = SpinDirection.Clockwise, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){
			var splitDuration = duration / 4f;
			for (int i = 0; i < 4; i++) {
				await Rotate (view, 90f, spinDirection, splitDuration, animationOptions);	
			}
			await Rotate (view, 0f, spinDirection, 0d);	
		}

		public static Task Rotate(this UIView view, float degrees, SpinDirection spinDirection = SpinDirection.Clockwise, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){
	
			System.Diagnostics.Debug.WriteLine ("degrees: {0}", degrees);
			var rotation = CGAffineTransform.Rotate (view.Transform, (spinDirection == SpinDirection.Clockwise ? 1 : -1) * DegreesToRadians(degrees - .00001f));

			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					view.Transform = rotation;
				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;
		}
			
		public static Task Flip (this UIView view, FlipDirection flipDirection, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear)
		{
			var m34 = (nfloat)(-1 * 0.001);
			view.Layer.AnchorPoint = new CGPoint ((nfloat)0.5, (nfloat)0.5f);

			var transform = view.Layer.Transform;
			transform.m34 = m34;

			switch (flipDirection) {
				case FlipDirection.TopToBottom:
					transform = transform.Rotate (DegreesToRadians(-180), 1.0f, 0.0f, 0.0f);
					break;
				case FlipDirection.BottomToTop:
					transform = transform.Rotate (DegreesToRadians(180), 1.0f, 0.0f, 0.0f);
					break;
				case FlipDirection.LeftToRight:
					transform = transform.Rotate (DegreesToRadians(180), 0.0f, -1.0f, 0.0f);
					break;
				case FlipDirection.RightToLeft:
					transform = transform.Rotate (DegreesToRadians(180), 0.0f, 1.0f, 0.0f);
					break;
			}

			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					view.Layer.Transform = transform;
				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;	
		}

		public static Task Scale(this UIView view, float scaleAmount, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){

			System.Diagnostics.Debug.WriteLine ("Scale Amount: {0}", scaleAmount);
			var scale = CGAffineTransform.Scale (view.Transform, scaleAmount, scaleAmount);

			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					view.Transform = scale;
				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;
		}

		public static Task Slide(this UIView view, SlideDirection slideDirection, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){

			System.Diagnostics.Debug.WriteLine ("Slide Direction: {0}", slideDirection);

			CGAffineTransform startingTransform, endingTransform;

			switch (slideDirection) {
			case SlideDirection.FromBottom:
				startingTransform = CGAffineTransform.MakeTranslation (0f, view.Bounds.Height);
				endingTransform = CGAffineTransform.MakeIdentity ();
				break;
			case SlideDirection.FromLeft:
				startingTransform = CGAffineTransform.MakeTranslation (-view.Bounds.Width, 0f);
				endingTransform = CGAffineTransform.MakeIdentity ();
				break;
			case SlideDirection.FromRight:
				startingTransform = CGAffineTransform.MakeTranslation (view.Bounds.Width, 0f);
				endingTransform = CGAffineTransform.MakeIdentity ();
				break;
			case SlideDirection.FromTop:
				startingTransform = CGAffineTransform.MakeTranslation (0f, -view.Bounds.Height);
				endingTransform = CGAffineTransform.MakeIdentity ();
				break;
			case SlideDirection.ToBottom:
				startingTransform = CGAffineTransform.MakeIdentity ();
				endingTransform = CGAffineTransform.MakeTranslation (0f, view.Bounds.Height);
				break;
			case SlideDirection.ToLeft:
				startingTransform = CGAffineTransform.MakeIdentity ();
				endingTransform = CGAffineTransform.MakeTranslation (-view.Bounds.Width, 0f);
				break;
			case SlideDirection.ToRight:
				startingTransform = CGAffineTransform.MakeIdentity ();
				endingTransform = CGAffineTransform.MakeTranslation (view.Bounds.Width, 0f);
				break;
			case SlideDirection.ToTop:
				startingTransform = CGAffineTransform.MakeIdentity ();
				endingTransform = CGAffineTransform.MakeTranslation (0f, -view.Bounds.Height);
				break;
			default:
				startingTransform = CGAffineTransform.MakeIdentity ();
				endingTransform = CGAffineTransform.MakeIdentity ();
				break;
			}

			view.Transform = startingTransform;

			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					view.Transform = endingTransform;
				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;
		}

		public static Task ResetAnimation(this UIView view, double duration = DefaultAnimationDuration, UIViewAnimationOptions animationOptions = UIViewAnimationOptions.CurveLinear){

			System.Diagnostics.Debug.WriteLine ("Restart Started");

			var animationCompleted = new TaskCompletionSource<object> ();
			UIView.Animate (duration, 0, animationOptions,
				() => {
					if(animationCompleted.Task.IsCanceled)
						return;

					view.Transform = CGAffineTransform.MakeIdentity();
				},
				() => animationCompleted.TrySetResult(null)
			);

			return animationCompleted.Task;
		}


		private static float DegreesToRadians(float degrees){
			return (degrees / 180f) * (float)Math.PI;
		}
	}
}


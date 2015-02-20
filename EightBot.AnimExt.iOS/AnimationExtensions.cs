using System;
using UIKit;
using System.Threading.Tasks;
using CoreGraphics;
using System.Linq;

namespace EightBot.AnimExt.iOS
{
	public enum FlipDirection
	{
		RightToLeft,
		LeftToRight,
		TopToBottom,
		BottomToTop
	}

	public enum Direction
	{
		Horizontal,
		Vertical
	}

	public static class AnimationExtensions
	{
		public const double DefaultAnimationDuration = .2d;

		public static Task FadeOut(this UIView view, double duration = DefaultAnimationDuration){

			return UIView.AnimateAsync(duration, () => {
				if(view.Alpha != 0f)
					view.Alpha = 0f;
			});

		}

		public static Task FadeIn(this UIView view, double duration = DefaultAnimationDuration){
			return UIView.AnimateAsync(duration, () => {
				if(view.Alpha != 1f)
					view.Alpha = 1f;
			});
		}

		public static async Task Spin(this UIView view, double duration = DefaultAnimationDuration){
			//var tcs = new TaskCompletionSource<object> ();

			await UIView.AnimateAsync (duration / 4, () => {
				view.Transform = CGAffineTransform.Rotate (view.Transform, (float)Math.PI / 2f);
			});
				
			var identity = CGAffineTransform.MakeIdentity ();

			if(view.Transform.Equals(identity))
				await Spin(view);

//			UIView.AnimateKeyframes (1d, 0d, UIViewKeyframeAnimationOptions.CalculationModePaced,
//				() => {
//					UIView.AddKeyframeWithRelativeStartTime (0d, 0d, () => {
//						view.Transform = CGAffineTransform.Rotate (view.Transform, (float)Math.PI * 2f / 3f);
//					});
//
//					UIView.AddKeyframeWithRelativeStartTime (0d, 0d, () => {
//						view.Transform = CGAffineTransform.Rotate (view.Transform, (float)Math.PI * 4f / 3f);
//					});
//
//					UIView.AddKeyframeWithRelativeStartTime (0d, 0d, () => {
//						view.Transform = CGAffineTransform.MakeIdentity();
//					});
//
//				},
//				ch => tcs.TrySetResult (null));
//
//			await tcs.Task;
		}
	}
}


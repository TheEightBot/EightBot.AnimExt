using System;
using Android.Views.Animations;
using Android.Views;
using Android.Animation;

namespace EightBot.FluentAnimator
{
	public static class FluentPropertyAnimation
	{

		public const long DefaultDuration = 1200;

		public static ValueAnimator FadeOut(this View view, long duration = DefaultDuration){

			var animation = ObjectAnimator.OfFloat (view, "alpha", 1f, 0f);
			animation.SetDuration (duration);

			if(view.Alpha != 0f)
				animation.Start ();

			return animation;
		}

		public static ValueAnimator FadeIn(this View view, long duration = DefaultDuration){
			var animation = ObjectAnimator.OfFloat (view, "alpha", 0f, 1f);
			animation.SetDuration (duration);

			if(view.Alpha != 1f)
				animation.Start ();

			return animation;
		}

		public static ValueAnimator Spin(this View view, long duration = DefaultDuration, ITimeInterpolator interpolator = null){
			var rotation = ObjectAnimator.OfFloat (view, "rotation", 0, 360);
			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());
			rotation.RepeatCount = ValueAnimator.Infinite;

			rotation.Start ();

			return rotation;
		}

		public enum FlipDirection
		{
			RightToLeft,
			LeftToRight,
			TopToBottom,
			BottomToTop
		}

		public static ValueAnimator Flip(this View view, FlipDirection flipDirection, long duration = DefaultDuration, ITimeInterpolator interpolator = null){

			var flipProperty = "rotationY";
			var flipDegrees = -180f;

			switch (flipDirection) {
			case FlipDirection.LeftToRight:
				flipDegrees = -flipDegrees;
				break;
			case FlipDirection.RightToLeft:
				break;
			case FlipDirection.TopToBottom:
				flipProperty = "rotationX";
				break;
			case FlipDirection.BottomToTop:
				flipProperty = "rotationX";
				flipDegrees = -flipDegrees;
				break;
			}

			var rotation = ObjectAnimator.OfFloat (view, flipProperty, 0, flipDegrees);
			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());

			rotation.Start ();

			return rotation;
		}

		public static ValueAnimator FlipReturn(this View view, FlipDirection flipDirection, long duration = DefaultDuration, ITimeInterpolator interpolator = null){
		
			var flipProperty = "rotationY";
			var flipDegrees = -180f;

			switch (flipDirection) {
			case FlipDirection.LeftToRight:
				flipDegrees = -flipDegrees;
				break;
			case FlipDirection.RightToLeft:
				break;
			case FlipDirection.TopToBottom:
				flipProperty = "rotationX";
				break;
			case FlipDirection.BottomToTop:
				flipProperty = "rotationX";
				flipDegrees = -flipDegrees;
				break;
			}

			var rotation = ObjectAnimator.OfFloat (view, flipProperty, flipDegrees, 0);

			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());

			rotation.Start ();

			return rotation;
		}

		public static ValueAnimator Pulsate(this View view, float pulsateSize = .9f, long duration = DefaultDuration){
			var scale = ObjectAnimator.OfPropertyValuesHolder (
				view,
				PropertyValuesHolder.OfFloat ("scaleX", 1, pulsateSize),
				PropertyValuesHolder.OfFloat ("scaleY", 1, pulsateSize)
			);
			scale.RepeatMode = ValueAnimatorRepeatMode.Reverse;
			scale.SetDuration (duration / 2);
			scale.RepeatCount = ValueAnimator.Infinite;
			scale.Start ();
			return scale;
		}

		public static ValueAnimator Wiggle(this View view, float wiggleAmount = .1f, long duration = DefaultDuration){

			var viewWiggleAmount = wiggleAmount < 1f ? wiggleAmount * view.Width : wiggleAmount;

			var scale = ObjectAnimator.OfPropertyValuesHolder (
				view,
				//PropertyValuesHolder.OfFloat ("scaleX", 1, pulsateSize),
				PropertyValuesHolder.OfFloat ("translationX", viewWiggleAmount, -viewWiggleAmount)
			);
			scale.RepeatMode = ValueAnimatorRepeatMode.Reverse;
			scale.SetDuration (duration / 2);
			scale.RepeatCount = ValueAnimator.Infinite;
			scale.Start ();
			return scale;
		}
	}
}


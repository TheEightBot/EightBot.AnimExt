using System;
using Android.Views.Animations;
using Android.Views;
using Android.Animation;
using System.Collections.Generic;

namespace EightBot.AnimExt.Droid
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

		public const long DefaultAnimationDuration = 1200;

		public static ValueAnimator FadeOut(this View view, long duration = DefaultAnimationDuration){

			var animation = ObjectAnimator.OfFloat (view, "alpha", 1f, 0f);

			animation.SetDuration (duration);

			if(view.Alpha != 0f)
				animation.Start ();

			return animation;
		}

		public static ValueAnimator FadeIn(this View view, long duration = DefaultAnimationDuration){
			var animation = ObjectAnimator.OfFloat (view, "alpha", 0f, 1f);
			animation.SetDuration (duration);

			if(view.Alpha != 1f)
				animation.Start ();

			return animation;
		}

		public static ValueAnimator Spin(this View view, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){
			var rotation = ObjectAnimator.OfFloat (view, "rotation", 0, 360);
			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());
			rotation.RepeatCount = ValueAnimator.Infinite;

			rotation.Start ();

			return rotation;
		}

		public static ValueAnimator Flip(this View view, FlipDirection flipDirection, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){

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

		public static ValueAnimator FlipReturn(this View view, FlipDirection flipDirection, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){
		
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

		public static ValueAnimator Pulsate(this View view, float pulsateSize = .9f, long duration = DefaultAnimationDuration){
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

		public static ValueAnimator Wiggle(this View view, Direction direction, float wiggleAmount = .1f, long duration = DefaultAnimationDuration){

			var viewWiggleAmount = wiggleAmount < 1f 
				? wiggleAmount * (direction == Direction.Horizontal ? view.Width : view.Height)
				: wiggleAmount;

			var scale = ObjectAnimator.OfFloat (view,
				direction == Direction.Horizontal ? "translationX" : "translationY", 
				0f, viewWiggleAmount, -viewWiggleAmount, 0f);
				
			scale.SetInterpolator (new DecelerateInterpolator ());
			scale.SetDuration (duration);
			scale.Start ();
			return scale;
		}

		public static ValueAnimator Squish(this View view, Direction direction, float squishAmount = .1f, long duration = DefaultAnimationDuration){
		
			var scale = ObjectAnimator.OfFloat (view,
				direction == Direction.Horizontal ? "scaleX" : "scaleY", 
				squishAmount);

			scale.RepeatMode = ValueAnimatorRepeatMode.Reverse;
			scale.RepeatCount = 1;
			scale.SetDuration (duration / 2);
			scale.Start ();
			return scale;
		}

		public static ValueAnimator JiggleBilly(this View view, float scaleAmount = 1.3f, float jiggleDegrees = 15f, int jiggleCount = 3, long duration = DefaultAnimationDuration){

			var jiggleRotation = new List<float>();
			jiggleRotation.Add (0f);
			for (int i = 0; i < jiggleCount; i++) {
				jiggleRotation.Add (jiggleDegrees);
				jiggleRotation.Add (-jiggleDegrees);
			}
			jiggleRotation.Add (0f);


			var scale = ObjectAnimator.OfPropertyValuesHolder (
				view,
				PropertyValuesHolder.OfFloat ("scaleX", 1, scaleAmount, 1),
				PropertyValuesHolder.OfFloat ("scaleY", 1, scaleAmount, 1),
				PropertyValuesHolder.OfFloat ("rotation", jiggleRotation.ToArray())
			);
			scale.RepeatMode = ValueAnimatorRepeatMode.Reverse;
			scale.SetInterpolator (new AccelerateDecelerateInterpolator ());
			scale.SetDuration (duration);
			scale.RepeatCount = 0;
			scale.Start ();
			return scale;
		}
	}
}


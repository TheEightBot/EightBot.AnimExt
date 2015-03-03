using System;
using Android.Views.Animations;
using Android.Views;
using Android.Animation;
using System.Collections.Generic;

namespace EightBot.AnimExt.Droid
{
	public static class AnimationExtensions
	{

		public const long DefaultAnimationDuration = 1200;

		public static ValueAnimator Fade(this View view, FadeType fadeType = FadeType.In, long duration = DefaultAnimationDuration){

			ObjectAnimator animation = null;

			switch (fadeType) {
			case FadeType.In:
				animation = ObjectAnimator.OfFloat (view, "alpha", 1f, 0f);
				break;
			case FadeType.Out:
				animation = ObjectAnimator.OfFloat (view, "alpha", 1f, 0f);
				break;
			}

			animation.SetDuration (duration);

			animation.Start ();

			return animation;
		}

		public static ValueAnimator Spin(this View view, SpinDirection spinDirection = SpinDirection.Clockwise, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){


			ObjectAnimator rotation = ObjectAnimator.OfFloat (view, "rotation", 0, 360);

			switch (spinDirection) {
			case SpinDirection.Clockwise:
				rotation = ObjectAnimator.OfFloat (view, "rotation", 0, 360);
				break;
			case SpinDirection.CounterClockwise:
				rotation = ObjectAnimator.OfFloat (view, "rotation", 0, -360);
				break;
			}

			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());
			rotation.RepeatCount = ValueAnimator.Infinite;

			rotation.Start ();

			return rotation;
		}

		public static ValueAnimator Flip(this View view, FlipDirection flipDirection, bool returnToOriginalPosition = false, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){

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

			var rotation = ObjectAnimator.OfFloat (
				view, 
				flipProperty, 
				returnToOriginalPosition ? flipDegrees : 0, 
				returnToOriginalPosition ? 0 : flipDegrees);

			rotation.SetDuration (duration);
			rotation.SetInterpolator (interpolator ?? new LinearInterpolator());

			rotation.Start ();

			return rotation;
		}

		public static ValueAnimator Scale(this View view, float scaleAmount, long duration = DefaultAnimationDuration){
			var scale = ObjectAnimator.OfPropertyValuesHolder (
				view,
				PropertyValuesHolder.OfFloat ("scaleX", 1, scaleAmount),
				PropertyValuesHolder.OfFloat ("scaleY", 1, scaleAmount)
			);
			scale.SetDuration (duration);
			scale.RepeatCount = 0;
			scale.Start ();
			return scale;
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

		public static ValueAnimator Slide(this View view, SlideDirection slideDirection, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){

			System.Diagnostics.Debug.WriteLine ("Slide Direction: {0}", slideDirection);

			ObjectAnimator slide = null;

			switch (slideDirection) {
				case SlideDirection.FromBottom:
					slide = ObjectAnimator.OfFloat (view, "translationY", view.MeasuredHeight, 0f);
					break;
				case SlideDirection.FromLeft:
					slide = ObjectAnimator.OfFloat (view, "translationX", -view.MeasuredWidth, 0f);
					break;
				case SlideDirection.FromRight:
					slide = ObjectAnimator.OfFloat (view, "translationX", view.MeasuredWidth, 0f);
					break;
				case SlideDirection.FromTop:
					slide = ObjectAnimator.OfFloat (view, "translationY", -view.MeasuredHeight, 0f);
					break;
				case SlideDirection.ToBottom:
					slide = ObjectAnimator.OfFloat (view, "translationY", 0f, view.MeasuredHeight);
					break;
				case SlideDirection.ToLeft:
					slide = ObjectAnimator.OfFloat (view, "translationX", 0f, -view.MeasuredWidth);
					break;
				case SlideDirection.ToRight:
					slide = ObjectAnimator.OfFloat (view, "translationX", 0f, view.MeasuredWidth);
					break;
				case SlideDirection.ToTop:
				default:
					slide = ObjectAnimator.OfFloat (view, "translationY", 0f, -view.MeasuredHeight);
					break;
			}

			slide.SetInterpolator (interpolator ?? new DecelerateInterpolator ());
			slide.SetDuration (duration);
			slide.Start ();
			return slide;
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
	
		public static ValueAnimator ResetAnimation(this View view, long duration = DefaultAnimationDuration, ITimeInterpolator interpolator = null){

			System.Diagnostics.Debug.WriteLine ("Reset Values");

			var reset = ObjectAnimator.OfPropertyValuesHolder (
				            view,
				            PropertyValuesHolder.OfFloat ("translationY", view.TranslationY, 0f),
				            PropertyValuesHolder.OfFloat ("translationX", view.TranslationX, 0f),
				            PropertyValuesHolder.OfFloat ("scaleX", view.ScaleX, 1.0f),
							PropertyValuesHolder.OfFloat ("scaleY", view.ScaleY, 1.0f),
							PropertyValuesHolder.OfFloat ("rotation", view.Rotation, 0.0f),
							PropertyValuesHolder.OfFloat ("rotationX", view.RotationX, 0.0f),
							PropertyValuesHolder.OfFloat ("rotationY", view.RotationY, 0.0f),
							PropertyValuesHolder.OfFloat ("alpha", view.Alpha, 1.0f));
				
			reset.SetInterpolator (interpolator ?? new AccelerateDecelerateInterpolator ());
			reset.SetDuration (duration);
			reset.RepeatCount = 0;
			reset.Start ();
			return reset;
		}
	}
}


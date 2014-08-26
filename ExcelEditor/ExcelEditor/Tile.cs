using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace ExcelEditor
{
    public class Tile : Button
    {

        #region Properties

        FrameworkElement backContent = null;
        FrameworkElement frontContent = null;
        Storyboard StoryPress = null;
        Storyboard StoryRelease = null;
        bool Pressed = false;

        #endregion

        #region Constructor

        static Tile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile),
                new FrameworkPropertyMetadata(typeof(Tile)));
        }

        #endregion

        #region Template

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // find the "mask" control 
            backContent = GetTemplateChild("backContent") as FrameworkElement;
            if (backContent == null)
            {
                throw new ArgumentNullException("backContent", "No element with the specified name found in control template.");
            }

            frontContent = GetTemplateChild("frontContent") as FrameworkElement;
            if (frontContent == null)
            {
                throw new ArgumentNullException("frontContent", "No element with the specified name found in control template.");
            }

            PrepareStory();
        }

        #endregion

        #region Story

        private void PrepareStory()
        {
            /****** Story Press******/
            StoryPress = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = null;
            EasingDoubleKeyFrame keyFrame;

            /*back scale x*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, backContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
            keyFrame.Value = 1.5;
            animation.KeyFrames.Add(keyFrame);

            StoryPress.Children.Add(animation);

            /*back scale y*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, backContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
            keyFrame.Value = 1.5;
            animation.KeyFrames.Add(keyFrame);

            StoryPress.Children.Add(animation);

            /*back opacity*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, backContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Opacity)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 0.5;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3));
            keyFrame.Value = 0;
            animation.KeyFrames.Add(keyFrame);

            StoryPress.Children.Add(animation);

            /*front scale x*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, frontContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1));
            keyFrame.Value = 0.9;
            animation.KeyFrames.Add(keyFrame);

            StoryPress.Children.Add(animation);

            /*front scale y*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, frontContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1));
            keyFrame.Value = 0.9;
            animation.KeyFrames.Add(keyFrame);

            StoryPress.Children.Add(animation);

            /****** Story Release******/
            StoryRelease = new Storyboard();

            /*front scale x*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, frontContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 0.9;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            StoryRelease.Children.Add(animation);

            /*front scale y*/
            animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, frontContent);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0));
            keyFrame.Value = 0.9;
            animation.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame();
            keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.1));
            keyFrame.Value = 1;
            animation.KeyFrames.Add(keyFrame);

            StoryRelease.Children.Add(animation);

        }

        #endregion

        #region Click

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            StoryPress.Begin(this);
            Pressed = true;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnClick()
        {
            TryTurnNormal();
            base.OnClick();
        }


        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            TryTurnNormal();
            base.OnMouseLeave(e);
        }

        private void TryTurnNormal()
        {
            if (Pressed)
            {
                StoryRelease.Begin(this);
                Pressed = false;
            }
        }

        #endregion

    }
}

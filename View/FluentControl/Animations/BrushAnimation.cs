using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Calc.View.FluentControl.Animations
{
    public class BrushAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(Brush);

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            if(!animationClock.CurrentProgress.HasValue) return Brushes.Transparent;

            // From/To
            var originValue = From ?? defaultOriginValue as Brush;
            var dstValue = To ?? defaultDestinationValue as Brush;

            var progress = animationClock.CurrentProgress.Value;
            if(progress == 0) return originValue;
            if(progress == 1) return dstValue;

            // Easing
            var easingFunction = EasingFunction;
            if(easingFunction != null) progress = easingFunction.Ease(progress);

            return new VisualBrush(new Border { Width = 1, Height = 1, Background = originValue, Child = new Border { Background = dstValue, Opacity = progress } });
        }

        protected override Freezable CreateInstanceCore() => new BrushAnimation();


        private Brush From => (Brush)GetValue(FromProperty);

        // Using a DependencyProperty as the backing store for From.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));


        public Brush To
        {
            private get => (Brush)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        // Using a DependencyProperty as the backing store for To.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));


        public IEasingFunction EasingFunction
        {
            private get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(BrushAnimation), new PropertyMetadata(null));
    }
}

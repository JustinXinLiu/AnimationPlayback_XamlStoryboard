using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AnimationPlayback_XamlStoryboard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // The SlideUpAnimation Storyboard contains a number of keyframes that do the following -
        // * Change the Offset Y of the TopPane;
        // * Change the Opacity of the TopPane;
        // * Change the Scale X & Y of the Marker;
        // * Change the Color of the Marker.

        private void LayoutRoot_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            // 1. Start and pause the animation immediately.
            SlideUpAnimation.Begin();
            SlideUpAnimation.Pause();
        }

        private void LayoutRoot_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // 2. Get the % value of how much the finger has moved based on the height of the root Grid.
            var ratio = Math.Abs(e.Cumulative.Translation.Y) / LayoutRoot.ActualHeight;
            var currentTime = SlideUpAnimation.Duration.TimeSpan.TotalMilliseconds * ratio;

            // 3. 'Scrub' the animation - This is the Seek method that I think is missing in 
            // Composition's KeyFrameAnimation & CompositionScopedBatch.
            SlideUpAnimation.Seek(TimeSpan.FromMilliseconds(currentTime));
        }

        private void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // 4. Resume the animation to finish it off smoothly.
            SlideUpAnimation.Resume();
        }
    }
}

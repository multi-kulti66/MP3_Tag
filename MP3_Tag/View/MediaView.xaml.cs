// ///////////////////////////////////
// File: MediaView.xaml.cs
// Last Change: 07.11.2016  23:43
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;



    /// <summary>
    ///     Interaction logic for MediaView.xaml
    /// </summary>
    public partial class MediaView : UserControl
    {
        #region Fields

        private bool userIsDraggingSlider = false;

        #endregion



        #region Constructors

        public MediaView()
        {
            this.InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += this.timer_Tick;
            timer.Start();
        }

        #endregion



        #region Methods

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((this.myMediaElement.Source != null) && this.myMediaElement.NaturalDuration.HasTimeSpan && (!this.userIsDraggingSlider))
            {
                this.timelineSlider.Minimum = 0;
                this.timelineSlider.Maximum = this.myMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                this.timelineSlider.Value = this.myMediaElement.Position.TotalSeconds;
            }
            else if ((this.myMediaElement.Source == null) && (!this.userIsDraggingSlider))
            {
                this.timelineSlider.Value = 0;
            }
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.userIsDraggingSlider = false;
            this.myMediaElement.Position = TimeSpan.FromSeconds(this.timelineSlider.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.lblProgressStatus.Text = TimeSpan.FromSeconds(this.timelineSlider.Value).ToString(@"hh\:mm\:ss");
        }

        // Play the media.
        private void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            // The Play method will begin the media if it is not currently active or 
            // resume media if it is paused. This has no effect if the media is
            // already running.
            this.myMediaElement.Play();

            // Initialize the MediaElement property values.
            this.InitializePropertyValues();
        }

        // Pause the media.
        private void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            this.myMediaElement.Pause();
        }

        // Stop the media.
        private void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            this.myMediaElement.Stop();
        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            this.myMediaElement.Volume = (double)this.volumeSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            this.timelineSlider.Maximum = this.myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            this.myMediaElement.Stop();
        }

        private void InitializePropertyValues()
        {
            // Set the media's starting Volume to the current value of the respective slider control.
            this.myMediaElement.Volume = (double)this.volumeSlider.Value;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.myMediaElement.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        #endregion
    }
}
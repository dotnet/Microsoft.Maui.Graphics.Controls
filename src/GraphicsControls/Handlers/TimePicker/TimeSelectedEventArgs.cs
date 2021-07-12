using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class TimeSelectedEventArgs : EventArgs
    {
        public TimeSelectedEventArgs(TimeSpan selectedTime)
        {
            SelectedTime = selectedTime;
        }

        public TimeSpan SelectedTime { get; set; }
    }
}

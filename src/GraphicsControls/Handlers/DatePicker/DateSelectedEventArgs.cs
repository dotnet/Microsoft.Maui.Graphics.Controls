using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class DateSelectedEventArgs : EventArgs
    {
        public DateSelectedEventArgs(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
        }

        public DateTime SelectedDate { get; set; }
    }
}

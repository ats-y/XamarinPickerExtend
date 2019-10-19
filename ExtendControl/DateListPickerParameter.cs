using System;
namespace ExtendControl
{
    public class DateListPickerParameter
    {
        public int PastDays { get; set; }
        public int FutureDays { get; set; }
        public DateTime StandardDate { get; set; } = DateTime.Now;

        public DateListPickerParameter()
        {
        }
    }
}

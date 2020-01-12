using System;
namespace ExtendControl.Models
{
    public class SelectableDateRange
    {
        public DateTime To { get; }
        public DateTime From { get; }
        public DateTime Select { get; }

        public SelectableDateRange()
        {
            DateTime now = DateTime.Now;
            To = now.Date;
            From = now.Date;
            Select = now.Date;
        }

        public SelectableDateRange(DateTime standard, uint pastDays, uint futureDays)
        {
            From = standard.AddDays(pastDays * -1).Date;
            To = standard.AddDays(futureDays).Date;
            Select = standard.Date;
        }

        public SelectableDateRange(DateTime standard, uint pastDays, uint futureDays, DateTime select)
        {
            From = standard.AddDays(pastDays * -1).Date;
            if (select.Date < From) From = select.Date;

            To = standard.AddDays(futureDays).Date;
            if (select.Date > To) To = select.Date;

            Select = select.Date;
        }
    }
}

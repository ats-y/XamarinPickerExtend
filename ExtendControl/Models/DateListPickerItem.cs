using System;
namespace ExtendControl.Models
{
    /// <summary>
    /// DateListPickerの選択肢
    /// </summary>
    public class DateListPickerItem
    {
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Date.ToString("yyyy/MM/dd");
        }
    }
}

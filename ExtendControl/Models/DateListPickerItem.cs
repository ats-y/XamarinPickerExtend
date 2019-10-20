using System;
namespace ExtendControl.Models
{
    /// <summary>
    /// DateListPickerの選択肢
    /// </summary>
    public class DateListPickerItem
    {
        /// <summary>
        /// 表示文字列
        /// </summary>
        public string DispText
        {
            get
            {
                return Date.ToString("yyyy/MM/dd");
            }
        }

        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }
    }
}

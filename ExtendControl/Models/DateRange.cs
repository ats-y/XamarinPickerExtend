using System;
namespace ExtendControl.Models
{
    /// <summary>
    /// 日付範囲を表すクラス
    /// </summary>
    public class DateRange
    {
        /// <summary>
        /// 基準日
        /// </summary>
        public DateTime StandardDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// 日付範囲の開始日を基準日の何日前とするか。
        /// </summary>
        public uint PastDays { get; set; } = 0;

        /// <summary>
        /// 日付範囲の終了日を基準日の何日後とするか。
        /// </summary>
        public uint FutureDays { get; set; } = 0;
    }
}

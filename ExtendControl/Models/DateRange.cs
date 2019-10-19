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
        public DateTime StandardDate { get; private set; }

        /// <summary>
        /// 日付範囲の開始日を基準日の何日前とするか。
        /// </summary>
        public uint PastDays { get; private set; }

        /// <summary>
        /// 日付範囲の終了日を基準日の何日後とするか。
        /// </summary>
        public uint FutureDays { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="standard">基準日</param>
        /// <param name="past">日付範囲の開始日を基準日の何日前とするか</param>
        /// <param name="future">日付範囲の終了日を基準日の何日後とするか</param>
        public DateRange(DateTime standard, uint past, uint future)
        {
            StandardDate = standard;
            PastDays = past;
            FutureDays = future;
        }
    }
}

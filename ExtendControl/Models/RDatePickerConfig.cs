using System;
namespace ExtendControl.Models
{
    /// <summary>
    /// RDatePickerの設定情報
    /// </summary>
    public class RDatePickerConfig
    {
        /// <summary>
        /// 選択可能な日付範囲の開始日。
        /// </summary>
        public DateTime To { get; }

        /// <summary>
        /// 選択可能な日付範囲の終了日。
        /// </summary>
        public DateTime From { get; }

        /// <summary>
        /// 初期選択日。
        /// </summary>
        public DateTime InitialSelectDate { get; }

        /// <summary>
        /// コンストラクタ。<br/>
        /// 選択可能範囲はシステム日のみ。初期選択日もシステム日。
        /// </summary>
        public RDatePickerConfig()
        {
            DateTime now = DateTime.Now;
            To = now.Date;
            From = now.Date;
            InitialSelectDate = now.Date;
        }

        /// <summary>
        /// コンストラクタ。<br/>
        /// 基準日と選択可能過去・未来日数から選択可能範囲を決定する。
        /// 初期選択日は基準日。
        /// </summary>
        /// <param name="standard">基準日</param>
        /// <param name="pastDays">選択可能過去日数</param>
        /// <param name="futureDays">選択可能未来日数</param>
        public RDatePickerConfig(DateTime standard, uint pastDays, uint futureDays)
        {
            From = standard.AddDays(pastDays * -1).Date;
            To = standard.AddDays(futureDays).Date;
            InitialSelectDate = standard.Date;
        }

        /// <summary>
        /// コンストラクタ。<br/>
        /// 基準日と選択可能過去未来日数から選択可能範囲を決定する。
        /// 初期選択日は指定された日付。<br/>
        /// 指定された初期選択日が選択範囲外であれば初期選択日を選択できるよう選択可能範囲を拡張する。<br/>
        /// 例）基準日：1/10、選択可能過去日数：2、選択可能未来日数：1、初期選択日：1/5とした場合。<br/>
        /// 　　→基準日・選択可能過去未来日数での選択可能範囲は1/8〜1/11だが、<br/>
        /// 　　　初期選択日が含まれないので初期選択日を選択できるよう1/5〜1/11に補正する。
        /// </summary>
        /// <param name="standard">基準日</param>
        /// <param name="pastDays">選択可能過去日数</param>
        /// <param name="futureDays">選択可能未来日数</param>
        /// <param name="select">初期選択日</param>
        public RDatePickerConfig(DateTime standard, uint pastDays, uint futureDays, DateTime select)
        {
            From = standard.AddDays(pastDays * -1).Date;
            if (select.Date < From) From = select.Date;

            To = standard.AddDays(futureDays).Date;
            if (select.Date > To) To = select.Date;

            InitialSelectDate = select.Date;
        }
    }
}

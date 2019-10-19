using System;
using System.Diagnostics;
using ExtendControl.Models;
using Reactive.Bindings;

namespace ExtendControl.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 適用ボタンコマンド
        /// </summary>
        public ReactiveCommand ApplyCｍd { get; set; } = new ReactiveCommand();

        /// <summary>
        /// 日付範囲プロパティ
        /// </summary>
        public ReactiveProperty<DateRange> DateRangeProp { get; set; } = new ReactiveProperty<DateRange>();

        public MainPageViewModel()
        {
            // 適用ボタンコマンドの処理。
            ApplyCｍd.Subscribe(_ =>
            {
                Debug.WriteLine("適用ボタンタップ");

                // 日付範囲をシステム日の4日前から2日後までとする。
                DateRange range = new DateRange(DateTime.Now, 4, 2);
                DateRangeProp.Value = range;
            });
        }

    }
}

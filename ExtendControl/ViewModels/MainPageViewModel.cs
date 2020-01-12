using System;
using System.Diagnostics;
using ExtendControl.Models;
using Prism.Navigation;
using Prism.Services;
using Reactive.Bindings;

namespace ExtendControl.ViewModels
{
    public class MainPageViewModel : INavigationAware, IDestructible
    {
        private IPageDialogService _dialogService;

        /// <summary>
        /// 適用ボタンコマンド
        /// </summary>
        public ReactiveCommand ApplyCｍd { get; set; } = new ReactiveCommand();

        /// <summary>
        /// 今日ボタンコマンド
        /// </summary>
        public ReactiveCommand TodayCmd { get; set; } = new ReactiveCommand();

        /// <summary>
        /// // 選択日付表示ボタンコマンド
        /// </summary>
        public ReactiveCommand ShowCmd { get; set; } = new ReactiveCommand();

        /// <summary>
        /// 日付範囲プロパティ
        /// </summary>
        public ReactiveProperty<DateRange> DateRangeProp { get; set; } = new ReactiveProperty<DateRange>();

        /// <summary>
        /// 日付選択の選択項目プロパティ
        /// </summary>
        public ReactiveProperty<DateListPickerItem> SelectedDate { get; set; } = new ReactiveProperty<DateListPickerItem>();

        public ReactiveProperty<double> PastDays { get; } = new ReactiveProperty<double>();
        public ReactiveProperty<double> FutureDays { get; } = new ReactiveProperty<double>();

        public ReactiveProperty<RDatePickerConfig> RDatePickerDateAtt { get; } = new ReactiveProperty<RDatePickerConfig>();
        public ReactiveProperty<DateTime> RDatePickerDateSelectedItem { get; } = new ReactiveProperty<DateTime>();

        public ReactiveProperty<DateTime> InitialDate { get; } = new ReactiveProperty<DateTime>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogService"></param>
        public MainPageViewModel(IPageDialogService dialogService)
        {
            TraceUtility.Trace();

            _dialogService = dialogService;

            // 日付範囲を更新する。
            DateRangeProp.Value = new DateRange
            {
                StandardDate = DateTime.Now.Date,
                PastDays = 1,
                FutureDays = 2,
            };

            // 適用ボタンコマンドの処理。
            ApplyCｍd.Subscribe(_ =>
            {
                OnApplyCmd();
            });

            // 今日ボタンコマンドの処理。
            TodayCmd.Subscribe(_ =>
            {
                Debug.WriteLine("今日ボタンタップ");
                SelectedDate.Value = new DateListPickerItem { Date = DateTime.Now.Date };
            });

            // 選択日付表示ボタンコマンドの処理。
            ShowCmd.Subscribe(_ =>
            {
                string msg;
                if (SelectedDate.Value == null)
                {
                    msg = "日付は選択されていません";
                } else
                {
                    string dateText = SelectedDate.Value?.Date.ToString("yyyy/MM/dd HH:mm:ss");
                    msg = $"選択日付変更！" + Environment.NewLine  + $"{dateText}";
                }

                Debug.WriteLine(msg);
                _dialogService.DisplayAlertAsync("日付", msg, "OK");
            });
        }

        private void OnApplyCmd()
        {
            Debug.WriteLine("適用ボタンタップ");

            DateTime std = DateTime.Now.Date;
            uint past = (uint)PastDays.Value;
            uint future = (uint)FutureDays.Value;

            // 日付範囲を更新する。
            DateRangeProp.Value = new DateRange
            {
                StandardDate = std,
                PastDays = past,
                FutureDays = future,
            };

            // RDatePickerの候補、選択肢を更新する。
            RDatePickerDateAtt.Value = new RDatePickerConfig(std, past, future, InitialDate.Value);
        }

        
        public void OnItemSelected(object selectItem)
        {
            _dialogService.DisplayAlertAsync("選択", "選択されました" + Environment.NewLine + selectItem.ToString(), "OK");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            TraceUtility.Trace();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            TraceUtility.Trace();

            InitialDate.Value = DateTime.Now;
        }

        public void Destroy()
        {
            TraceUtility.Trace();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExtendControl.Models;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    /// <summary>
    /// 日付範囲選択ピッカー<br/>
    /// 選択可能は日付範囲および初期選択日はConfigプロパティで指定する（この場合、変更確認イベントは発声しない）
    /// </summary>
    public class RDatePicker : UndoablePicker
    {
        /// <summary>
        /// 日付範囲・初期選択日付設定。
        /// </summary>
        public RDatePickerConfig Config
        {
            get
            {
                return (RDatePickerConfig)GetValue(ConfigProperty);
            }

            set
            {
                SetValue(ConfigProperty, value);
            }
        }

        /// <summary>
        /// 日付範囲・初期選択日付設定プロパティのバインディングプロパティ。
        /// このプロパティで選択日付が変更されても変更確認イベントは起こさない。
        /// </summary>
        public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
            nameof(Config), // 紐づけるプロパティ名。
            typeof(RDatePickerConfig), // プロパティの型。
            typeof(RDatePicker), // このプロパティを追加するコントロールの型。
            null, // 初期値。
            BindingMode.TwoWay, // バインディング方向。
            propertyChanged: OnChangeConfig // 値が変更された後に実行するデリゲート。
        );

        /// <summary>
        /// 日付範囲・初期選択日付設定プロパティ変更処理。
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnChangeConfig(BindableObject bindable, object oldValue, object newValue)
        {
            // 引数チェック。
            RDatePickerConfig value = newValue as RDatePickerConfig;
            if (value == null) return;
            RDatePicker myself = bindable as RDatePicker;
            if (myself == null) return;

            // 変更確認タスクを実施しないようにする。
            // あとで戻すために退避する。
            var shuntedChangeTask  = myself.BeforeChageTask;
            myself.BeforeChageTask = null;

            // 選択範囲、選択項目を設定する。
            myself.SetDateList(value);

            // 退避していた変更確認タスクを戻す。
            myself.BeforeChageTask = shuntedChangeTask;
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public RDatePicker()
        {
        }

        /// <summary>
        /// 選択肢を設定する。
        /// </summary>
        /// <param name="range">選択肢の日付範囲</param>
        private void SetDateList(RDatePickerConfig range)
        {
            TraceUtility.Trace();

            // 現在の選択項目を退避。
            DateTime selected = (DateTime)SelectedItem;

            // スピナー選択肢をクリアする。
            if (ItemsSource != null) ItemsSource.Clear();

            // 引数で指定された日付範囲どおりに選択肢を設定する。
            List<DateTime> list = new List<DateTime>();
            for (DateTime i = range.From; i <= range.To; i = i.AddDays(1))
            {
                list.Add(i.Date);
            }
            ItemsSource = list;

            // 選択肢の決定。
            if (range.InitialSelectDate != DateTime.MinValue)
            {
                SelectedItem = range.InitialSelectDate.Date;
            }
            else
            {
                SelectedItem = selected;
            }
        }
    }
}

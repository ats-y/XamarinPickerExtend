using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ExtendControl.Models;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    /// <summary>
    /// Pickerを拡張した指定した日付範囲を選択するピッカー。
    /// 選択肢例：
    ///   2019/10/17
    ///   2019/10/18
    ///   2019/10/19
    ///   2019/10/20
    /// </summary>
    public class DateListPicker : UndoablePicker
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DateListPicker()
        {
        }

        /// <summary>
        /// 日付範囲プロパティ。
        /// </summary>
        public DateRange DateRange
        {
            get
            {
                Debug.WriteLine("DateRange getter");
                return (DateRange)GetValue(DateRangeProperty);
            }

            set
            {
                Debug.WriteLine("DateRange setter");
                SetValue(DateRangeProperty, value);
            }
        }

        /// <summary>
        /// 日付範囲バインディングプロパティ。
        /// ※バインディングするプロパティ名＋"Property"という名前にする必要がある。
        /// </summary>
        public static readonly BindableProperty DateRangeProperty = BindableProperty.Create(
            nameof(DateRange), // 紐づけるプロパティ名。
            typeof(DateRange), // プロパティの型。
            typeof(DateListPicker), // このプロパティを追加するコントロールの型。
            null, // 初期値。
            BindingMode.TwoWay, // バインディング方向。
            propertyChanged: OnChangeDateRange // 値が変更された後に実行するデリゲート。
            );

        /// <summary>
        /// 日付範囲バインディングプロパティが変更された後に実行する処理。
        /// </summary>
        /// <remarks>
        /// BindableProperty.BindingPropertyChangingDelegateの実装。
        /// </remarks>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnChangeDateRange(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine("OnChangeDateRange");

            DateRange range = newValue as DateRange;
            if (range == null) return;

            DateListPicker myself = bindable as DateListPicker;
            if (myself == null) return;

            myself.SetDateList(range);
        }

        /// <summary>
        /// 選択肢を設定する。
        /// </summary>
        /// <param name="range">選択肢の日付範囲</param>
        private void SetDateList(DateRange range)
        {
            // スピナー選択肢をクリアする。
            if (ItemsSource != null) ItemsSource.Clear();

            // 引数で指定された日付範囲どおりに選択肢を設定する。
            List<DateListPickerItem> list = new List<DateListPickerItem>();
            for (int i = (int)range.PastDays * -1; i <= range.FutureDays; i++)
            {
                list.Add(
                    new DateListPickerItem
                    {
                        Date = range.StandardDate.AddDays(i).Date,
                    });
            }
            ItemsSource = list;
        }

        /// <summary>
        /// 指定した日付を選択状態にする。
        /// </summary>
        /// <param name="target">選択する日付</param>
        public void SelectItemForce(DateTime target)
        {
            for(int i = 0; i < ItemsSource.Count; i++)
            {
                DateListPickerItem item = ItemsSource[i] as DateListPickerItem;
                if (item != null
                    && item.Date.Date == target.Date)
                {
                    SelectIndexForce(i);
                    break;
                }
            }
        }
    }
}

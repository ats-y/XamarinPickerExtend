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
    public class DateListPicker : Picker
    {
        /// <summary>
        /// 変更確認処理。
        /// falseで変更を元に戻す。
        /// </summary>
        public Func<Task<bool>> ConfirmingChageTask;

        /// <summary>
        /// 確定されたSelectIndex。
        /// </summary>
        private int _fixedSelectIndex;

        /// <summary>
        /// 変更確認で否定された直後のSelectedIndexを戻す時にtrueとし
        /// SelectedIndexChanged内の変更確認を行わないようにする。
        /// TODO:フラグじゃなくしたい。
        /// </summary>
        private bool _undoing;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DateListPicker()
        {
            // TODO:警告でちゃう。
            this.SelectedIndexChanged += async (s, e) =>
            {
                Debug.WriteLine("SelectedIndexChanged()");

                // 変更確認で否定され選択項目を元に戻すときは何もしない。
                if (_undoing)
                {
                    Debug.WriteLine("undoing select item");
                    return;
                }

                // 変更確認タスクがあればこれを実行。
                if (ConfirmingChageTask != null)
                {
                    bool ret = await ConfirmingChageTask.Invoke();
                    if (!ret)
                    {
                        // 変更を否定されたら選択項目を元に戻す。
                        _undoing = true;
                        SelectedIndex = _fixedSelectIndex;
                        _undoing = false;
                        return;
                    }
                }

                // 変更が確定したら選択項目のインデックスを保存しておく。
                _fixedSelectIndex = SelectedIndex;
            };
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
    }
}

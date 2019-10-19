using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExtendControl.Models;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    /// <summary>
    /// Pickerを拡張した指定した日付範囲を選択するピッカー。
    /// 選択肢：
    ///   2019/10/17
    ///   2019/10/18
    ///   2019/10/19
    ///   2019/10/20
    /// </summary>
    public class DateListPicker : Picker
    {
        public DateRange DateRange
        {
            get
            {
                return (DateRange)GetValue(DateRangeProperty);
            }

            set
            {
                SetValue(DateRangeProperty, value);
            }
        }

        public static readonly BindableProperty DateRangeProperty = BindableProperty.Create(
            nameof(DateRange),
            typeof(DateRange),
            typeof(DateListPicker),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnChangeDateRange);

        private static void OnChangeDateRange(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine("OnChangeDateRange");

            DateRange range = newValue as DateRange;
            if (range == null) return;

            DateListPicker myself = bindable as DateListPicker;
            if (myself == null) return;

            myself.SetDateList(range);
        }

        public DateListPicker()
        {
        }

        private void SetDateList(DateRange range)
        {
            if (ItemsSource != null) ItemsSource.Clear();

            List<DateListPickerItem> list = new List<DateListPickerItem>();
            for (int i = (int)range.PastDays * -1; i <= range.FutureDays; i++)
            {
                list.Add(
                    new DateListPickerItem
                    {
                        Date = range.StandardDate.AddDays(i),
                    });
            }

            ItemsSource = list;
        }

        public class DateListPickerItem
        {
            public string DispText
            {
                get
                {
                    return Date.ToString("yyyy/MM/dd");
                }
            }
            public DateTime Date { get; set; }
        }
    }
}

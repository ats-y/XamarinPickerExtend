using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <summary>
        /// 選択可能未来日数
        /// </summary>
        public int FutureDays { get; set; }

        /// <summary>
        /// 選択可能過去日数
        /// </summary>
        public int PastDays
        {
            get
            {
                return (int)this.GetValue(PastDaysProperty);
            }

            set
            {
                this.SetValue(PastDaysProperty, value);
            }
        }

        /// <summary>
        /// 選択可能過去日数のバインダブルプロパティ。
        /// </summary>
        public static readonly BindableProperty PastDaysProperty = BindableProperty.Create(
            nameof(PastDays),
            typeof(int),
            typeof(DateListPicker),
            defaultValue: (int)0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnChangePastDays);

        public DateListPicker()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnChangePastDays(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine($"OnChangePastDays");

            DateListPicker myself = bindable as DateListPicker;
            if (myself == null) return;

            myself.PastDays = (int)newValue;

            myself.SetDateList();
        }

        private void SetDateList()
        {
            if(ItemsSource != null) ItemsSource.Clear();

            int past = 3;
            int future = 1;
            DateTime standard = DateTime.Now.Date;
            List<DateListPickerItem> list = new List<DateListPickerItem>();
            for (int i = past * -1; i <= future; i++)
            {
                list.Add(
                    new DateListPickerItem
                    {
                        Date = standard.AddDays(i),
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

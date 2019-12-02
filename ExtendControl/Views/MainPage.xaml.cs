using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Taskの保存しあとで実行できるかどうかのテスト。
            _task = CreateCofirmingTask();
            ConfirmButton.Clicked += async (s, e) =>
            {
                Task<bool> task = CreateCofirmingTask();
                await task;
                // TODO:↓だと動かない。なんで？
                //await _task;
            };

            // 日付ピッカーの変更を確認するタスクをセット。
            MyDatePicker.ConfirmingChageTask = CreateCofirmingTask;
            MyUndoablePicker.ConfirmingChageTask = CreateCofirmingTask;
        }

        /// <summary>
        /// タスクを保存（しておきたい）。
        /// </summary>
        public Task<bool> _task;

        /// <summary>
        /// 変更確認タスクを生成。
        /// </summary>
        /// <returns></returns>
        public Task<bool> CreateCofirmingTask()
        {
            return DisplayAlert("確認", "変更していい？", "いいよ", "だめ");
        }

        /// <summary>
        /// リセットボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnResetClicked(object sender, EventArgs args)
        {
            //MyUndoablePicker.SelectIndexForce(0);
            MyUndoablePicker.SelectItemForce("1st");
        }

        public void OnTodayClicked(object sender, EventArgs args)
        {
            MyDatePicker.SelectItemForce(DateTime.Now);
        }
    }
}

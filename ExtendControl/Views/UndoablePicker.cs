using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ExtendControl.Models.EventHandler;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    /// <summary>
    /// 取り消し可能なピッカー
    /// </summary>
    public class UndoablePicker : Picker
    {
        /// <summary>
        /// 選択項目変更前に実行するタスク。
        /// falseで変更を元に戻す。
        /// </summary>
        public Func<Task<bool>> BeforeChageTask;

        /// <summary>
        /// 選択確定イベント。
        /// </summary>
        public event EventHandler<FixedSelectionEventArgs> FixedSelectionEvent;

        /// <summary>
        /// 確定されたSelectIndex。
        /// 選択肢を元に戻すために使用する。
        /// </summary>
        private int _fixedSelectIndex;

        /// <summary>
        /// 選択取り消し中かどうか。
        /// </summary>
        private bool _isUndoing;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UndoablePicker()
        {
            this.SelectedIndexChanged += async (s, e) =>
            {
                Debug.WriteLine("SelectedIndexChanged");

                // 変更確認タスクがあればこれを実行。
                if (BeforeChageTask != null)
                {
                    bool ret = await BeforeChageTask.Invoke();
                    if (!ret)
                    {
                        // 変更を否定されたら選択項目を元に戻す。
                        _isUndoing = true;
                        SelectIndexForce(_fixedSelectIndex);
                        _isUndoing = false;
                        return;
                    }
                }

                // 選択項目を確定する。
                if (!_isUndoing)
                {
                    // 変更が確定したら選択項目のインデックスを保存しておく。
                    _fixedSelectIndex = SelectedIndex;

                    // 選択確定イベントを発生させる。
                    FixedSelectionEventArgs args;
                    args = new FixedSelectionEventArgs(_fixedSelectIndex, SelectedItem);
                    FixedSelectionEvent?.Invoke(this, args);
                }

                Debug.WriteLine($"Selected. index=[{SelectedIndex}] item=[{SelectedItem}]");
            };
        }

        /// <summary>
        /// 変更確認処理を呼び出さずに選択項目を設定する。
        /// </summary>
        /// <param name="index">選択する項目のインデックス</param>
        public void SelectIndexForce(int index)
        {
            Func<Task<bool>> tmp = BeforeChageTask;
            BeforeChageTask = null;
            SelectedIndex = index;
            BeforeChageTask = tmp;
        }

        /// <summary>
        /// 指定した項目を選択状態にする。
        /// </summary>
        /// <param name="target">選択する項目</param>
        public void SelectItemForce(object target)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Equals(target))
                {
                    SelectIndexForce(i);
                    break;
                }
            }
        }
    }
}

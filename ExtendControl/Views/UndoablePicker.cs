using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExtendControl.Views
{
    /// <summary>
    /// 取り消し可能なピッカー
    /// </summary>
    public class UndoablePicker : Picker
    {
        /// <summary>
        /// 変更確認処理。
        /// falseで変更を元に戻す。
        /// </summary>
        public Func<Task<bool>> ConfirmingChageTask;

        /// <summary>
        /// 確定されたSelectIndex。
        /// </summary>
        public int _fixedSelectIndex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UndoablePicker()
        {
            // TODO:警告でちゃう。
            this.SelectedIndexChanged += async (s, e) =>
            {
                Debug.WriteLine("SelectedIndexChanged");

                // 変更確認タスクがあればこれを実行。
                if (ConfirmingChageTask != null)
                {
                    bool ret = await ConfirmingChageTask.Invoke();
                    if (!ret)
                    {
                        // 変更を否定されたら選択項目を元に戻す。
                        SelectIndexForce(_fixedSelectIndex);
                        return;
                    }
                }

                // 変更が確定したら選択項目のインデックスを保存しておく。
                _fixedSelectIndex = SelectedIndex;
            };
        }

        /// <summary>
        /// 変更確認処理を呼び出さずに選択項目を設定する。
        /// </summary>
        /// <param name="index"></param>
        public void SelectIndexForce(int index)
        {
            Func<Task<bool>> tmp = ConfirmingChageTask;
            ConfirmingChageTask = null;
            SelectedIndex = index;
            ConfirmingChageTask = tmp;
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

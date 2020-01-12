using System;
namespace ExtendControl.Models.EventHandler
{
    /// <summary>
    /// 選択確定イベントデータ
    /// </summary>
    public class FixedSelectionEventArgs : EventArgs
    {
        /// <summary>
        /// 確定した選択肢のインデックス
        /// </summary>
        public int SelectIndex { get; private set; }

        /// <summary>
        /// 確定した選択肢
        /// </summary>
        public object SelectItem { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public FixedSelectionEventArgs(int index, object item)
        {
            SelectIndex = index;
            SelectItem = item;
        }
    }
}

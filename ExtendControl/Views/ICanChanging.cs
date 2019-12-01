using System;
using System.Threading.Tasks;

namespace ExtendControl.Views
{
    /// <summary>
    /// ページ遷移直前の確認インタフェイス
    /// </summary>
    public interface ICanChanging
    {
        /// <summary>
        /// 変更可能かどうか確認するタスク。
        /// </summary>
        /// <returns></returns>
        Task<bool> CanChanging();
    }
}

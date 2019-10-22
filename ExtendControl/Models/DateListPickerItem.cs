using System;
namespace ExtendControl.Models
{
    /// <summary>
    /// DateListPickerの選択肢。
    /// DateListPickerのItemSourceの一要素。
    /// </summary>
    public class DateListPickerItem
    {
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// DateListPickerの表示文字列はこのメソッドの戻り値が使われる。
        /// </summary>
        /// <returns>日付をyyyy/MM/ddの形式で返す。</returns>
        public override string ToString()
        {
            return Date.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// DateListPickerのSelectedItemに値を指定した際にItemSource各要素との比較で使われる。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Dateプロパティの日付部が一致していれば同じと判断する。
            DateListPickerItem src = obj as DateListPickerItem;
            if (src == null) return false;
            return Date.Date == src.Date;
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
    }
}

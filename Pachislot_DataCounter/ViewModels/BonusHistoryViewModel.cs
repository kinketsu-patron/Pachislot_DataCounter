/**
 * =============================================================
 * File         :BonusHistoryViewModel.cs
 * Summary      :BonusHistoryViewModelのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/07/15
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.Models.Entity;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Pachislot_DataCounter.ViewModels
{
    public class BonusHistoryViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private BonusHistoryList m_BonusHistoryList;

        // =======================================================
        // プロパティ
        // =======================================================
        /// <summary>
        /// 現在のゲーム数に合わせたバー表示リスト
        /// </summary>
        public List<GamesBar> CurrentGameBar
        {
            get { return m_BonusHistoryList.CurrentGameBar; }
            set { m_BonusHistoryList.CurrentGameBar = value; }
        }
        /// <summary>
        /// 1回前から10回前のボーナス履歴のバー表示リスト
        /// </summary>
        public List<GamesBar> BonusHistory
        {
            get { return m_BonusHistoryList.BonusHistory; }
            set { m_BonusHistoryList.BonusHistory = value; }
        }

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="p_BonusHistoryList">ボーナス履歴リスト</param>
        public BonusHistoryViewModel( BonusHistoryList p_BonusHistoryList )
        {
            m_BonusHistoryList = p_BonusHistoryList;
            m_BonusHistoryList.PropertyChanged += ( sender, e ) => RaisePropertyChanged( e.PropertyName );
        }
    }
}

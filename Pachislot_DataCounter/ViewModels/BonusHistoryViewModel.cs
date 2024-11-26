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
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
    public class BonusHistoryViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private DataManager m_DataManager;
        private BonusHistoryList m_BonusHistoryList;

        // =======================================================
        // プロパティ
        // =======================================================
        /// <summary>
        /// 現在のゲーム数に合わせたバー表示リスト
        /// </summary>
        public ObservableCollection<GamesBar> CurrentGameBar { get; set; }
        /// <summary>
        /// 1回前から10回前のボーナス履歴のバー表示リスト
        /// </summary>
        public ObservableCollection<GamesBar> BonusHistory { get; set; }

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BonusHistoryViewModel( BonusHistoryList p_BonusHistoryList, DataManager p_DataManager )
        {
            m_BonusHistoryList = p_BonusHistoryList;
            m_DataManager = p_DataManager;


            CurrentGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.CurrentGame ).AddTo( m_Disposables );
            CurrentGame.Subscribe( _ => UpdateGames( ) );
            DuringRB = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringRB ).AddTo( m_Disposables );
            DuringRB.Skip( 1 ).Subscribe( isbonus =>
            {
                if ( isbonus == false )
                {
                    UpdateBonusHistory( "REGULAR_BONUS" );
                }
            } );
            DuringBB = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBB ).AddTo( m_Disposables );
            DuringBB.Skip( 1 ).Subscribe( isbonus =>
            {
                if ( isbonus == false )
                {
                    UpdateBonusHistory( "BIG_BONUS" );
                }
            } );
        }
    }
}

/**
 * =============================================================
 * File         :BonusHistoryList.cs
 * Summary      :ボーナス履歴リストクラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/11/26
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models.Entity;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Pachislot_DataCounter.Models
{
    public class BonusHistoryList : BindableBase
    {
        // =======================================================
        // 列挙型
        // =======================================================
        private enum BonusType
        {
            BIG_BONUS,
            REGULAR_BONUS,
            NONE
        }

        // =======================================================
        // メンバ変数
        // =======================================================
        private DataManager m_DataManager;

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
        /// <param name="p_DataManager">データ管理クラスオブジェクト</param>
        public BonusHistoryList( DataManager p_DataManager )
        {
            m_DataManager = p_DataManager;

            CurrentGameBar = new ObservableCollection<GamesBar>( );
            CurrentGameBar.Add( new GamesBar
            {
                Bar_Ten = Visibility.Hidden,
                Bar_Nine = Visibility.Hidden,
                Bar_Eight = Visibility.Hidden,
                Bar_Seven = Visibility.Hidden,
                Bar_Six = Visibility.Hidden,
                Bar_Five = Visibility.Hidden,
                Bar_Four = Visibility.Hidden,
                Bar_Three = Visibility.Hidden,
                Bar_Two = Visibility.Hidden,
                Bar_One = Visibility.Visible,
                TimesAgo = "現在",
                KindOfBonus = "NONE",
                Games = 0
            } );

            BonusHistory = new ObservableCollection<GamesBar>( );
            for ( int i = 0; i < 10; i++ )
            {
                BonusHistory.Add( new GamesBar
                {
                    Bar_Ten = Visibility.Hidden,
                    Bar_Nine = Visibility.Hidden,
                    Bar_Eight = Visibility.Hidden,
                    Bar_Seven = Visibility.Hidden,
                    Bar_Six = Visibility.Hidden,
                    Bar_Five = Visibility.Hidden,
                    Bar_Four = Visibility.Hidden,
                    Bar_Three = Visibility.Hidden,
                    Bar_Two = Visibility.Hidden,
                    Bar_One = Visibility.Visible,
                    TimesAgo = i + 1 + "回前",
                    KindOfBonus = "NONE",
                    Games = 0
                } );
            }

            m_DataManager.PropertyChanged += ( sender, e ) =>
            {
                if ( e.PropertyName == "CurrentGame" )
                {
                    update_currentgames( m_DataManager.CurrentGame );
                    RaisePropertyChanged( "CurrentGameBar" );
                }
                if ( e.PropertyName == "DuringRB" && m_DataManager.DuringRB == false )
                {
                    update_bonushistory( BonusType.REGULAR_BONUS );
                    RaisePropertyChanged( "BonusHistory" );
                }
                if ( e.PropertyName == "DuringBB" && m_DataManager.DuringBB == false )
                {
                    update_bonushistory( BonusType.BIG_BONUS );
                    RaisePropertyChanged( "BonusHistory" );
                }
            };
        }

        // =======================================================
        // 非公開メソッド
        // =======================================================
        /// <summary>
        /// ゲーム数が更新されたときに現在ゲーム数のバー表示も更新する
        /// </summary>
        /// <param name="p_GameCount">現在のゲーム数</param>
        private void update_currentgames( int p_GameCount )
        {
            CurrentGameBar[ 0 ].Games = p_GameCount;

            if ( p_GameCount < 100 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 100 <= p_GameCount && p_GameCount < 200 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 200 <= p_GameCount && p_GameCount < 300 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 300 <= p_GameCount && p_GameCount < 400 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 400 <= p_GameCount && p_GameCount < 500 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 500 <= p_GameCount && p_GameCount < 600 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 600 <= p_GameCount && p_GameCount < 700 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 700 <= p_GameCount && p_GameCount < 800 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else if ( 800 <= p_GameCount && p_GameCount < 900 )
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Hidden;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            } else
            {
                CurrentGameBar[ 0 ].Bar_Ten = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Nine = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Eight = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Seven = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Six = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Five = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Four = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Three = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_Two = Visibility.Visible;
                CurrentGameBar[ 0 ].Bar_One = Visibility.Visible;
            }
        }
        /// <summary>
        /// ボーナス終了時に呼び出されてBonusHistoryの中のBonusHistoryVisibleオブジェクトをシフトする
        /// </summary>
        /// <param name="p_BonusType">ボーナス種別</param>
        private void update_bonushistory( BonusType p_BonusType )
        {
            BonusHistory.Remove( BonusHistory.Last( ) );
            switch ( p_BonusType )
            {
                case BonusType.REGULAR_BONUS:
                    CurrentGameBar[ 0 ].KindOfBonus = "RB";
                    break;
                case BonusType.BIG_BONUS:
                    CurrentGameBar[ 0 ].KindOfBonus = "BB";
                    break;
                default:
                    CurrentGameBar[ 0 ].KindOfBonus = "NONE";
                    break;
            }

            BonusHistory.Insert( 0, CurrentGameBar.First( ).Clone( ) );               // CurrentGameBarに入っているオブジェクトのディープコピーをBonusHisotyの先頭に追加する

            for ( int i = 0; i < BonusHistory.Count; i++ )
            {
                BonusHistory[ i ].TimesAgo = ( i + 1 ).ToString( ) + "回前";      // X回前の表示を+1して過去のものにする
            }
        }
    }
}

/**
 * =============================================================
 * File         :MainWindowViewModel.cs
 * Summary      :MainWindowのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/18
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings.Extensions;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region メンバ変数
        // =======================================================
        // メンバ変数
        // =======================================================
        private SerialCom m_SerialCom;
        private DataManager m_DataManager;
        private MetroWindow m_MetroWindow;
        private WpfPlot m_SlumpGraph;
        private List<uint> m_GamesList;
        private List<int> m_CoinDiffList;
        #endregion

        #region コマンド
        // =======================================================
        // コマンド
        // =======================================================
        /// <summary>
        /// Connectボタンクリックコマンド
        /// </summary>
        public DelegateCommand Click_Connect { get; }
        /// <summary>
        /// Exitボタンクリックコマンド
        /// </summary>
        public DelegateCommand<MainWindow> Click_Exit { get; }
        #endregion

        #region プロパティ
        // =======================================================
        // プロパティ
        // =======================================================
        /// <summary>
        /// ビッグボーナス中フラグ
        /// </summary>
        public bool DuringBigBonus
        {
            get { return m_CalcModel.DuringBigBonus; }
            set { m_CalcModel.DuringBigBonus = value; }
        }
        /// <summary>
        /// レギュラーボーナス中フラグ
        /// </summary>
        public bool DuringRegularBonus
        {
            get { return m_CalcModel.DuringRegularBonus; }
            set { m_CalcModel.DuringRegularBonus = value; }
        }
        /// <summary>
        /// ボーナス中フラグ
        /// </summary>
        public bool DuringBonus
        {
            get { return m_CalcModel.DuringBonus; }
            set { m_CalcModel.DuringBonus = value; }
        }
        /// <summary>
        /// 累計ゲーム数
        /// </summary>
        public uint AllGame
        {
            get { return m_CalcModel.AllGame; }
            set { m_CalcModel.AllGame = value; }
        }
        /// <summary>
        /// コイン差枚数
        /// </summary>
        public int Diff
        {
            get { return m_CalcModel.Diff; }
            set { m_CalcModel.Diff = value; }
        }
        /// <summary>
        /// スランプグラフ
        /// </summary>
        public WpfPlot SlumpGraph
        {
            get { return m_CalcModel.SlumpGraph; }
            set { m_CalcModel.SlumpGraph = value; }
        }
        /// <summary>
        /// ゲーム数(スランプグラフ表示用)
        /// </summary>
        public List<uint> GamesList
        {
            get { return m_CalcModel.GamesList; }
            set { m_CalcModel.GamesList = value; }
        }
        /// <summary>
        /// コイン差枚数(スランプグラフ表示用)
        /// </summary>
        public List<int> CoinDiffList
        {
            get { return m_CalcModel.CoinDiffList; }
            set { m_CalcModel.CoinDiffList = value; }
        }

        public List<string> PortList
        {
            get { return m_SerialCom.PortList; }
            set { m_SerialCom.PortList = value; }
        }

        public string SelectedPort
        {
            get { return m_SerialCom.SelectedPort; }
            set { m_SerialCom.SelectedPort = value; }
        }
        #endregion

        #region 公開メソッド
        // =======================================================
        // メソッド
        // =======================================================
        /// <summary>
        /// MainWindowのビューモデルのコンストラクタ
        /// </summary>
        public MainWindowViewModel( IRegionManager p_RegionManager, DataManager p_DataManager )
        {
            m_SerialCom = new SerialCom( );
            m_MetroWindow = Application.Current.MainWindow as MetroWindow;
            m_DataManager = p_DataManager;

            m_SlumpGraph = new WpfPlot( );
            m_GamesList = new List<uint>( ) { 0 };
            m_CoinDiffList = new List<int>( ) { 0 };

            m_SerialCom.PropertyChanged += ( sender, e ) => RaisePropertyChanged( e.PropertyName );
            Click_Connect = new DelegateCommand( OnConnectClicked );
            Click_Exit = new DelegateCommand<MainWindow>( OnExitClicked );

            p_RegionManager.RegisterViewWithRegion( "BigBonusCounter", typeof( BBCounter ) );
            p_RegionManager.RegisterViewWithRegion( "RegularBonusCounter", typeof( RBCounter ) );
            p_RegionManager.RegisterViewWithRegion( "AllGameCounter", typeof( AllGameCounter ) );
            p_RegionManager.RegisterViewWithRegion( "CurrentGameCounter", typeof( CurrentGameCounter ) );
            p_RegionManager.RegisterViewWithRegion( "InCoinCounter", typeof( InCoinCounter ) );
            p_RegionManager.RegisterViewWithRegion( "OutCoinCounter", typeof( OutCoinCounter ) );
            p_RegionManager.RegisterViewWithRegion( "BonusHistory", typeof( BonusHistory ) );
            p_RegionManager.RegisterViewWithRegion( "SlumpGraph", typeof( SlumpGraph ) );

            DuringBigBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBB ).AddTo( m_Disposables );
            DuringRegularBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringRB ).AddTo( m_Disposables );
            DuringBonus = m_DataManager.ToReactivePropertyAsSynchronized( m => m.DuringBonus ).AddTo( m_Disposables );
            Diff = m_DataManager.ToReactivePropertyAsSynchronized( m => m.Diff ).AddTo( m_Disposables );
            AllGame = m_DataManager.ToReactivePropertyAsSynchronized( m => m.AllGame ).AddTo( m_Disposables );
            // 最初のコンストラクタが走った時を除き、10の倍数回のときにDrawGraphを呼ぶ
            AllGame.Skip( 1 ).Where( game => game % 10 == 0 ).Subscribe( game => DrawGraph( game, Diff.Value ) );
        }
        #endregion

        #region 非公開メソッド
        /// <summary>
        /// 接続ボタンクリック時の処理
        /// </summary>
        private void OnConnectClicked( )
        {
            try
            {
                m_SerialCom.ComStart( ); // シリアル通信を開始する
            } catch ( Exception ex )
            {
                ShowMessageBox( "エラー", ex.Message );
            }
        }
        /// <summary>
        /// 終了ボタンクリック時の処理
        /// </summary>
        private void OnExitClicked( MainWindow p_Window )
        {
            m_SerialCom.ComStop( ); // シリアル通信を停止する
            p_Window?.Close( );     // nullでなければウィンドウを閉じる
        }
        /// <summary>
        /// メッセージダイアログを表示
        /// </summary>
        /// <param name="p_Title">メッセージボックスタイトル</param>
        /// <param name="p_Message">メッセージ</param>
        private async void ShowMessageBox( string p_Title, string p_Message )
        {
            await m_MetroWindow.ShowMessageAsync( p_Title, p_Message );
        }
        #endregion
    }
}
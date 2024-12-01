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
using MahApps.Metro.Controls.Dialogs;
using Pachislot_DataCounter.Models;
using Pachislot_DataCounter.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.ObjectModel;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private SerialCom m_SerialCom;
        private DataManager m_DataManager;
        private readonly IDialogCoordinator m_DialogCoordinator;

        // =======================================================
        // コマンド
        // =======================================================
        /// <summary>
        /// Connectトグルボタンチェックコマンド
        /// </summary>
        public DelegateCommand Checked_Connect { get; }
        /// <summary>
        /// Connectトグルボタンチェック解除コマンド
        /// </summary>
        public DelegateCommand Unchecked_Connect { get; }
        /// <summary>
        /// 接続状態を示すバッジ
        /// </summary>
        public string ConnectBadge
        {
            get { return m_SerialCom.ConnectBadge; }
            set { m_SerialCom.ConnectBadge = value; }
        }
        /// <summary>
        /// Exitボタンクリックコマンド
        /// </summary>
        public DelegateCommand<MainWindow> Click_Exit { get; }
        /// <summary>
        /// COMポートリスト
        /// </summary>
        public ObservableCollection<string> PortList
        {
            get { return m_SerialCom.PortList; }
            set { m_SerialCom.PortList = value; }
        }
        /// <summary>
        /// 選択中のCOMポート
        /// </summary>
        public string SelectedPort
        {
            get { return m_SerialCom.SelectedPort; }
            set { m_SerialCom.SelectedPort = value; }
        }

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// MainWindowのビューモデルのコンストラクタ
        /// </summary>
        public MainWindowViewModel( IRegionManager p_RegionManager, IDialogCoordinator p_DialogCoordinator, SerialCom p_SerialCom, DataManager p_DataManager )
        {
            m_DataManager = p_DataManager;
            m_SerialCom = p_SerialCom;
            m_DialogCoordinator = p_DialogCoordinator;

            m_SerialCom.PropertyChanged += ( sender, e ) => RaisePropertyChanged( e.PropertyName );

            Checked_Connect = new DelegateCommand( checked_connect );
            Unchecked_Connect = new DelegateCommand( unchecked_connect );
            Click_Exit = new DelegateCommand<MainWindow>( exit_clicked );

            p_RegionManager.RegisterViewWithRegion( "BigBonusCounter", typeof( BBCounter ) );
            p_RegionManager.RegisterViewWithRegion( "RegularBonusCounter", typeof( RBCounter ) );
            p_RegionManager.RegisterViewWithRegion( "AllGameCounter", typeof( AllGameCounter ) );
            p_RegionManager.RegisterViewWithRegion( "CurrentGameCounter", typeof( CurrentGameCounter ) );
            p_RegionManager.RegisterViewWithRegion( "DiffCoinCounter", typeof( DiffCoinCounter ) );
            p_RegionManager.RegisterViewWithRegion( "BonusHistory", typeof( BonusHistory ) );
            p_RegionManager.RegisterViewWithRegion( "SlumpGraph", typeof( SlumpGraph ) );
        }

        // =======================================================
        // 非公開メソッド
        // =======================================================
        /// <summary>
        /// 接続ボタンクリック(チェック)時の処理
        /// </summary>
        private async void checked_connect( )
        {
            try
            {
                m_SerialCom.ComStart( ); // シリアル通信を開始する
            } catch ( Exception ex )
            {
                await m_DialogCoordinator.ShowMessageAsync( this, "エラー", ex.Message );
            }
        }
        /// <summary>
        /// 接続ボタンクリック(チェック解除)時の処理
        /// </summary>
        private async void unchecked_connect( )
        {
            try
            {
                m_SerialCom.ComStop( ); // シリアル通信を停止する
            } catch ( Exception ex )
            {
                await m_DialogCoordinator.ShowMessageAsync( this, "エラー", ex.Message );
            }
        }
        /// <summary>
        /// 終了ボタンクリック時の処理
        /// </summary>
        private void exit_clicked( MainWindow p_Window )
        {
            m_SerialCom.ComStop( ); // シリアル通信を停止する
            p_Window?.Close( );     // nullでなければウィンドウを閉じる
        }
    }
}
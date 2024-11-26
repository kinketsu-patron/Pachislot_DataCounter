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
using Prism.Navigation.Regions;
using System;
using System.Windows;

namespace Pachislot_DataCounter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private SerialCom m_SerialCom;
        private DataManager m_DataManager;
        private MetroWindow m_MetroWindow;

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

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// MainWindowのビューモデルのコンストラクタ
        /// </summary>
        public MainWindowViewModel( IRegionManager p_RegionManager, SerialCom p_SerialCom, DataManager p_DataManager )
        {
            m_MetroWindow = Application.Current.MainWindow as MetroWindow;
            m_DataManager = p_DataManager;
            m_SerialCom = p_SerialCom;

            Click_Connect = new DelegateCommand( connect_clicked );
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
        /// 接続ボタンクリック時の処理
        /// </summary>
        private void connect_clicked( )
        {
            try
            {
                m_SerialCom.ComStart( ); // シリアル通信を開始する
            } catch ( Exception ex )
            {
                show_messagebox( "エラー", ex.Message );
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
        /// <summary>
        /// メッセージダイアログを表示
        /// </summary>
        /// <param name="p_Title">メッセージボックスタイトル</param>
        /// <param name="p_Message">メッセージ</param>
        private async void show_messagebox( string p_Title, string p_Message )
        {
            await m_MetroWindow.ShowMessageAsync( p_Title, p_Message );
        }
    }
}
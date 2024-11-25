/**
 * =============================================================
 * File         :SerialCom.cs
 * Summary      :シリアル通信クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/22
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Pachislot_DataCounter.Models
{
    public class SerialCom : BindableBase
    {
        #region メンバ変数
        private SerialPort m_SerialPort;
        #endregion

        #region プロパティ
        private List<string> m_PortList = new List<string>();
        public List<string> PortList
        {
            get { return m_PortList; }
            set { SetProperty( ref m_PortList, value ); }
        }

        private string m_SelectedPort;
        public string SelectedPort
        {
            get { return m_SelectedPort; }
            set { SetProperty( ref m_SelectedPort, value ); }
        }
        #endregion

        #region 公開メソッド
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SerialCom( )
        {
            m_SerialPort = new SerialPort( );
            m_SerialPort.BaudRate = 115200;
            m_SerialPort.DataBits = 8;
            m_SerialPort.Parity = Parity.None;
            m_SerialPort.Encoding = Encoding.UTF8;
            m_SerialPort.WriteTimeout = 5000;
            m_SerialPort.ReadTimeout = 5000;
            m_SerialPort.DtrEnable = true;

            //foreach ( var port in SerialPort.GetPortNames( ) )
            //{
            //    PortList.Add( port );
            //}
            //SelectedPort = PortList.FirstOrDefault( );
            m_SerialPort.DataReceived += MessageReceived;
        }

        /// <summary>
        /// 通信スタート
        /// </summary>
        public void ComStart( )
        {
            try
            {
                //m_SerialPort.PortName = SelectedPort;
                m_SerialPort.PortName = "COM4";
                m_SerialPort.Open( );
            } catch
            {
                throw;
            }
        }

        /// <summary>
        /// 通信ストップ
        /// </summary>
        public void ComStop( )
        {
            try
            {
                if ( m_SerialPort.IsOpen )
                {
                    m_SerialPort.Close( );
                }
            } catch
            {
                throw;
            }
        }

        private void MessageReceived( object sender, SerialDataReceivedEventArgs e )
        {
            string message;
            GameInfo gameInfo = null;

            try
            {
                message = m_SerialPort.ReadLine( );
                gameInfo = JsonSerializer.Deserialize<GameInfo>( message );

                Application.Current.Dispatcher.Invoke( ( ) =>
                {
                    m_DataManager.Store( gameInfo );
                } );
            } catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }
        #endregion
    }
}

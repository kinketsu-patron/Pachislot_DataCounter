﻿/**
 * =============================================================
 * File         :AllGameCounterViewModel.cs
 * Summary      :AllGameCounterのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/07/05
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.ViewModels
{
    public class AllGameCounterViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private NumCounter m_NumCounter;
        private DataManager m_DataManager;

        // =======================================================
        // プロパティ
        // =======================================================
        /// <summary>
        /// 5桁目の数値
        /// </summary>
        public BitmapImage FifthDigit
        {
            get { return m_NumCounter.FifthDigit; }
            set { m_NumCounter.FifthDigit = value; }
        }
        /// <summary>
        /// 4桁目の数値
        /// </summary>
        public BitmapImage ForthDigit
        {
            get { return m_NumCounter.ForthDigit; }
            set { m_NumCounter.ForthDigit = value; }
        }
        /// <summary>
        /// 3桁目の数値
        /// </summary>
        public BitmapImage ThirdDigit
        {
            get { return m_NumCounter.ThirdDigit; }
            set { m_NumCounter.ThirdDigit = value; }
        }
        /// <summary>
        /// 2桁目の数値
        /// </summary>
        public BitmapImage SecondDigit
        {
            get { return m_NumCounter.SecondDigit; }
            set { m_NumCounter.SecondDigit = value; }
        }
        /// <summary>
        /// 1桁目の数値
        /// </summary>
        public BitmapImage FirstDigit
        {
            get { return m_NumCounter.FirstDigit; }
            set { m_NumCounter.FirstDigit = value; }
        }

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="p_NumCounter">DIコンテナ内のNumCounterオブジェクト</param>
        /// <param name="p_DataManager">DIコンテナ内のDataManagerオブジェクト</param>
        public AllGameCounterViewModel( NumCounter p_NumCounter, DataManager p_DataManager )
        {
            m_NumCounter = p_NumCounter;
            m_NumCounter.PropertyChanged += ( sender, e ) => RaisePropertyChanged( e.PropertyName );
            m_DataManager = p_DataManager;
            m_DataManager.PropertyChanged += ( sender, e ) =>
            {
                if ( e.PropertyName == "AllGame" )
                {
                    m_NumCounter.SetNumber( m_DataManager.AllGame );
                }
            };
        }
    }
}

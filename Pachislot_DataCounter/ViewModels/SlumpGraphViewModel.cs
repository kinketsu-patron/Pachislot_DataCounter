/**
 * =============================================================
 * File         :SlumpGraphViewModel.cs
 * Summary      :SlumpGraphのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/11/28
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Pachislot_DataCounter.Models;
using Prism.Mvvm;
using ScottPlot.WPF;

namespace Pachislot_DataCounter.ViewModels
{
    public class SlumpGraphViewModel : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private GraphDrawer m_GraphDrawer;

        // =======================================================
        // プロパティ
        // =======================================================
        /// <summary>
        /// ScottPlotオブジェクト
        /// ※ScottPlotはオブジェクトそのものをプロパティにするほかない。
        /// </summary>
        public WpfPlot ScottPlot
        {
            get { return m_GraphDrawer.ScottPlot; }
            set { m_GraphDrawer.ScottPlot = value; }
        }

        // =======================================================
        // コンストラクタ
        // =======================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="p_GraphDrawer">グラフ描画クラスのオブジェクト</param>
        public SlumpGraphViewModel( GraphDrawer p_GraphDrawer )
        {
            m_GraphDrawer = p_GraphDrawer;
        }
    }
}

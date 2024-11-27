/**
 * =============================================================
 * File         :GraphDrawer.cs
 * Summary      :グラフ描画クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/11/26
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Prism.Mvvm;
using ScottPlot;
using ScottPlot.WPF;
using System.Collections.Generic;
using System.Linq;

namespace Pachislot_DataCounter.Models
{
    public class GraphDrawer : BindableBase
    {
        // =======================================================
        // メンバ変数
        // =======================================================
        private DataManager m_DataManager;
        private List<int> m_GamesList;
        private List<int> m_CoinDiffList;

        // =======================================================
        // プロパティ
        // =======================================================
        public WpfPlot ScottPlot { set; get; }

        // =======================================================
        // コンストラクタ
        // =======================================================
        public GraphDrawer( DataManager p_DataManager )
        {
            setting_graph( );
            m_DataManager = p_DataManager;
            m_DataManager.PropertyChanged += ( sender, e ) =>
            {
                if ( e.PropertyName == "AllGame" && m_DataManager.AllGame % 10 == 0 )
                {
                    draw_graph( m_DataManager.AllGame, m_DataManager.DiffCoin );
                }
            };
        }

        // =======================================================
        // 非公開メソッド
        // =======================================================
        /// <summary>
        /// グラフ設定
        /// </summary>
        private void setting_graph( )
        {
            ScottPlot = new WpfPlot( );
            m_GamesList = new List<int>( ) { 0 };
            m_CoinDiffList = new List<int>( ) { 0 };

            // Y軸
            ScottPlot.Plot.Axes.Left.Label.Text = "差枚数";
            ScottPlot.Plot.Axes.Left.Label.FontName = "BIZ UDゴシック";
            ScottPlot.Plot.Axes.Left.Label.ForeColor = Colors.Azure;
            ScottPlot.Plot.Axes.Left.Label.FontSize = 20;
            ScottPlot.Plot.Axes.Left.Label.Bold = true;
            ScottPlot.Plot.Axes.Left.MajorTickStyle.Color = Colors.GoldenRod;
            ScottPlot.Plot.Axes.Left.MinorTickStyle.Color = Colors.Transparent;
            ScottPlot.Plot.Axes.Left.MinorTickStyle.Length = 0;
            ScottPlot.Plot.Axes.Left.TickLabelStyle.ForeColor = Colors.Azure;
            ScottPlot.Plot.Axes.Left.TickLabelStyle.FontSize = 20;
            ScottPlot.Plot.Axes.Left.TickLabelStyle.Bold = true;
            ScottPlot.Plot.Axes.Left.FrameLineStyle.Color = Colors.Azure;
            ScottPlot.Plot.Axes.Left.FrameLineStyle.Width = 4;

            ScottPlot.TickGenerators.NumericAutomatic l_TickGenY = new ScottPlot.TickGenerators.NumericAutomatic();
            l_TickGenY.TargetTickCount = 5;
            ScottPlot.Plot.Axes.Left.TickGenerator = l_TickGenY;


            // X軸
            ScottPlot.Plot.Axes.Bottom.Label.Text = "ゲーム数";
            ScottPlot.Plot.Axes.Bottom.Label.FontName = "BIZ UDゴシック";
            ScottPlot.Plot.Axes.Bottom.Label.ForeColor = Colors.Azure;
            ScottPlot.Plot.Axes.Bottom.Label.FontSize = 20;
            ScottPlot.Plot.Axes.Bottom.Label.Bold = true;
            ScottPlot.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.GoldenRod;
            ScottPlot.Plot.Axes.Bottom.MinorTickStyle.Color = Colors.Transparent;
            ScottPlot.Plot.Axes.Bottom.MinorTickStyle.Length = 0;
            ScottPlot.Plot.Axes.Bottom.TickLabelStyle.ForeColor = Colors.Azure;
            ScottPlot.Plot.Axes.Bottom.TickLabelStyle.FontSize = 20;
            ScottPlot.Plot.Axes.Bottom.TickLabelStyle.Bold = true;
            ScottPlot.Plot.Axes.Bottom.FrameLineStyle.Color = Colors.Azure;
            ScottPlot.Plot.Axes.Bottom.FrameLineStyle.Width = 4;

            ScottPlot.TickGenerators.NumericAutomatic l_TickGenX = new ScottPlot.TickGenerators.NumericAutomatic();
            l_TickGenX.TargetTickCount = 6;
            ScottPlot.Plot.Axes.Bottom.TickGenerator = l_TickGenX;

            // グリッド
            ScottPlot.Plot.Grid.MajorLineColor = Colors.Azure.WithOpacity( 0.2 );

            // 全体
            ScottPlot.Plot.FigureBackground.Color = Colors.Transparent;
            ScottPlot.Plot.DataBackground.Color = Color.FromHex( "#1F1F1F" );

            // 最初の軸最大最小を設定
            ScottPlot.Plot.Axes.SetLimits( 0, 1000, -1000, 1000 );

            // 初期データを設定
            var l_Line = ScottPlot.Plot.Add.ScatterLine( m_GamesList, m_CoinDiffList );
            l_Line.Color = Colors.Gold;
            l_Line.LineWidth = 6;
            l_Line.MarkerSize = 0;
            ScottPlot.Refresh( );
        }

        /// <summary>
        /// 新しい点をグラフにプロットして描画
        /// </summary>
        /// <param name="p_Game">新しい点の累計ゲーム数</param>
        /// <param name="p_CoinDiff">差枚数</param>
        private void draw_graph( int p_Game, int p_CoinDiff )
        {
            m_GamesList.Add( p_Game );
            m_CoinDiffList.Add( p_CoinDiff );
            var l_Line = ScottPlot.Plot.Add.ScatterLine( m_GamesList, m_CoinDiffList );
            l_Line.Color = Colors.Gold;
            l_Line.LineWidth = 6;
            l_Line.MarkerSize = 0;

            AxisLimits l_Limits = ScottPlot.Plot.Axes.GetLimits( );
            double l_Min_X = l_Limits.Left;
            double l_Max_X = l_Limits.Right;
            double l_Min_Y = l_Limits.Bottom;
            double l_Max_Y = l_Limits.Top;

            if ( p_Game >= ( l_Max_X * 0.8 ) )
            {
                l_Max_X = l_Max_X * 2.0;
            }

            if ( m_CoinDiffList.Min( ) <= ( l_Min_Y * 0.8 ) )
            {
                l_Min_Y = l_Min_Y * 2.0;
            }

            if ( m_CoinDiffList.Max( ) >= ( l_Max_Y * 0.8 ) )
            {
                l_Max_Y = l_Max_Y * 2.0;
            }

            ScottPlot.Plot.Axes.SetLimits( l_Min_X, l_Max_X, l_Min_Y, l_Max_Y );
            ScottPlot.Refresh( );
        }
    }
}

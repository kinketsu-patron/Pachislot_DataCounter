using Pachislot_DataCounter.Views;
using Prism.Mvvm;
using ScottPlot;
using System;

namespace Pachislot_DataCounter.ViewModels
{
    public class SlumpGraphViewModel : BindableBase
    {
        public SlumpGraphViewModel( )
        {
            // Y軸
            SlumpGraph.Plot.Axes.Left.Label.Text = "差枚数";
            SlumpGraph.Plot.Axes.Left.Label.FontName = "BIZ UDゴシック";
            SlumpGraph.Plot.Axes.Left.Label.ForeColor = Colors.Azure;
            SlumpGraph.Plot.Axes.Left.Label.FontSize = 20;
            SlumpGraph.Plot.Axes.Left.Label.Bold = true;
            SlumpGraph.Plot.Axes.Left.MajorTickStyle.Color = Colors.GoldenRod;
            SlumpGraph.Plot.Axes.Left.MinorTickStyle.Color = Colors.Transparent;
            SlumpGraph.Plot.Axes.Left.MinorTickStyle.Length = 0;
            SlumpGraph.Plot.Axes.Left.TickLabelStyle.ForeColor = Colors.Azure;
            SlumpGraph.Plot.Axes.Left.TickLabelStyle.FontSize = 20;
            SlumpGraph.Plot.Axes.Left.TickLabelStyle.Bold = true;
            SlumpGraph.Plot.Axes.Left.FrameLineStyle.Color = Colors.Azure;
            SlumpGraph.Plot.Axes.Left.FrameLineStyle.Width = 4;

            ScottPlot.TickGenerators.NumericAutomatic l_TickGenY = new ScottPlot.TickGenerators.NumericAutomatic();
            l_TickGenY.TargetTickCount = 5;
            SlumpGraph.Plot.Axes.Left.TickGenerator = l_TickGenY;


            // X軸
            SlumpGraph.Plot.Axes.Bottom.Label.Text = "ゲーム数";
            SlumpGraph.Plot.Axes.Bottom.Label.FontName = "BIZ UDゴシック";
            SlumpGraph.Plot.Axes.Bottom.Label.ForeColor = Colors.Azure;
            SlumpGraph.Plot.Axes.Bottom.Label.FontSize = 20;
            SlumpGraph.Plot.Axes.Bottom.Label.Bold = true;
            SlumpGraph.Plot.Axes.Bottom.MajorTickStyle.Color = Colors.GoldenRod;
            SlumpGraph.Plot.Axes.Bottom.MinorTickStyle.Color = Colors.Transparent;
            SlumpGraph.Plot.Axes.Bottom.MinorTickStyle.Length = 0;
            SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.ForeColor = Colors.Azure;
            SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.FontSize = 20;
            SlumpGraph.Plot.Axes.Bottom.TickLabelStyle.Bold = true;
            SlumpGraph.Plot.Axes.Bottom.FrameLineStyle.Color = Colors.Azure;
            SlumpGraph.Plot.Axes.Bottom.FrameLineStyle.Width = 4;

            ScottPlot.TickGenerators.NumericAutomatic l_TickGenX = new ScottPlot.TickGenerators.NumericAutomatic();
            l_TickGenX.TargetTickCount = 6;
            SlumpGraph.Plot.Axes.Bottom.TickGenerator = l_TickGenX;

            // グリッド
            SlumpGraph.Plot.Grid.MajorLineColor = Colors.Azure.WithOpacity( 0.2 );

            // 全体
            SlumpGraph.Plot.FigureBackground.Color = Colors.Transparent;
            SlumpGraph.Plot.DataBackground.Color = Color.FromHex( "#1F1F1F" );

            // 最初の軸最大最小を設定
            SlumpGraph.Plot.Axes.SetLimits( 0, 1000, -1000, 1000 );

            // 初期データを設定
            var l_Line = SlumpGraph.Plot.Add.ScatterLine( GamesList, CoinDiffList );
            l_Line.Color = Colors.Gold;
            l_Line.LineWidth = 6;
            l_Line.MarkerSize = 0;
            SlumpGraph.Refresh( );
        }

        /// <summary>
        /// グラフを描画・更新
        /// </summary>
        /// <param name="p_Game">追加するゲーム数情報</param>
        /// <param name="p_CoinDiff">追加する差枚数情報</param>
        private void DrawGraph( uint p_Game, int p_CoinDiff )
        {
            GamesList.Add( p_Game );
            CoinDiffList.Add( p_CoinDiff );
            var l_Line = SlumpGraph.Plot.Add.ScatterLine( GamesList, CoinDiffList );
            l_Line.Color = Colors.Gold;
            l_Line.LineWidth = 6;
            l_Line.MarkerSize = 0;

            AxisLimits l_Limits = SlumpGraph.Plot.Axes.GetLimits( );
            double l_Min_X = l_Limits.Left;
            double l_Max_X = l_Limits.Right;
            double l_Min_Y = l_Limits.Bottom;
            double l_Max_Y = l_Limits.Top;

            if ( p_Game >= ( l_Max_X * 0.8 ) )
            {
                l_Max_X = l_Max_X * 2.0;
            }

            if ( CoinDiffList.Min( ) <= ( l_Min_Y * 0.8 ) )
            {
                l_Min_Y = l_Min_Y * 2.0;
            }

            if ( CoinDiffList.Max( ) >= ( l_Max_Y * 0.8 ) )
            {
                l_Max_Y = l_Max_Y * 2.0;
            }

            SlumpGraph.Plot.Axes.SetLimits( l_Min_X, l_Max_X, l_Min_Y, l_Max_Y );
            SlumpGraph.Refresh( );
        }
    }
}

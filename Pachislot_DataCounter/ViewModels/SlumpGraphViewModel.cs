using Pachislot_DataCounter.Models;
using Prism.Mvvm;
using ScottPlot.WPF;

namespace Pachislot_DataCounter.ViewModels
{
    public class SlumpGraphViewModel : BindableBase
    {
        private GraphDrawer m_GraphDrawer;

        public WpfPlot ScottPlot
        {
            get { return m_GraphDrawer.ScottPlot; }
            set { m_GraphDrawer.ScottPlot = value; }
        }

        public SlumpGraphViewModel( GraphDrawer p_GraphDrawer )
        {
            m_GraphDrawer = p_GraphDrawer;
        }
    }
}

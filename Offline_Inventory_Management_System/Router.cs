using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Inventory_Management_System
{
    public static class Router
    {
        private static Panel _mainPanel;

        public static void Initialize(Panel mainPanel)
        {
            _mainPanel = mainPanel;
        }

        public static void Navigate(UserControl newPage)
        {
            if (_mainPanel == null)
                throw new InvalidOperationException("Router not initialized with a content panel.");

            _mainPanel.Controls.Clear();
            newPage.Dock = DockStyle.Fill;
            _mainPanel.Controls.Add(newPage);
        }
    }
}

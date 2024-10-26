//using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;  // For Android-specific configuration
//using MauiTabbedPage = Microsoft.Maui.Controls.TabbedPage;  // Alias for TabbedPage to avoid ambiguity

namespace BikeSharingApp.Pages
{
    public partial class CustomTabbedPage : TabbedPage  // Use the alias here
    {
        public CustomTabbedPage()
        {
            InitializeComponent();

            //// Explicitly call the Android-specific configuration for TabbedPage
            //this.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
            //    .SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}
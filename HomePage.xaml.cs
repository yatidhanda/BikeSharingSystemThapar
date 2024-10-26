using BikeSharingApp.Models;
using BikeSharingApp.Services;
namespace BikeSharingApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        LblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);
        GetCategories();
        GetTrendingProperties();
    }

    private async void GetTrendingProperties()
    {
        var properties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
    {
        var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;
    }
    void CvCategories_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection == null) return;
        Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    } 

}
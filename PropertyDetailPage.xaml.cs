
using BikeSharingApp.Services;
namespace BikeSharingApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    public PropertyDetailPage(int propertyId)
    {
        InitializeComponent();
        GetPropertyDetail(propertyId);
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await ApiService.GetPropertyDetail(propertyId);
        LblPrice.Text = "$ " + property.Price;
        LblDescription.Text = property.Detail;
        LblAddress.Text = property.Address;

        ImgProperty.Source = property.FullImageUrl;
    }

    private async void BtnBookNow_Clicked(object sender, EventArgs e)
    {
        // Call ESP8266 to unlock the cycle
        bool result = await SendUnlockSignalToEsp8266();

        if (result)
        {
            await DisplayAlert("Success", "Cycle unlocked!", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Failed to unlock the cycle. Try again.", "OK");
        }
    }

    private readonly string esp8266IpAddress = "http://172.16.36.7";
    //private async Task<bool> SendUnlockSignalToEsp8266()
    //{
    //    try
    //    {
    //        using (HttpClient client = new HttpClient())
    //        {
    //            // You can customize the request here, sending a specific message if required by the ESP8266
    //            string unlockUrl = $"{esp8266IpAddress}/unlock";
    //            var response = await client.GetAsync(unlockUrl);

    //            // Check if the response is successful
    //            return response.IsSuccessStatusCode;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle any errors that occur during the HTTP request
    //        Console.WriteLine($"Error unlocking cycle: {ex.Message}");
    //        return false;
    //    }
    //}

    private async Task<bool> SendUnlockSignalToEsp8266()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Setting a timeout in case the ESP8266 takes time to respond
                client.Timeout = TimeSpan.FromSeconds(10);

                string unlockUrl = $"{esp8266IpAddress}/unlock";

                // Sending GET request to ESP8266
                HttpResponseMessage response = await client.GetAsync(unlockUrl);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Unlock request successful!");
                    return true;
                }
                else
                {
                    // Log the response status code for debugging
                    Console.WriteLine($"Unlock request failed: {response.StatusCode}");
                    return false;
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            // Handle network or connection errors
            Console.WriteLine($"Network error: {httpEx.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // General error logging
            Console.WriteLine($"Error unlocking cycle: {ex.Message}");
            return false;
        }
    }



}

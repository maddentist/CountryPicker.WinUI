using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CountryPicker.UWP.Class;
using CountryPicker.UWP.Class.Models;
using System;

namespace TestApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
            // Set window size
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(500, 700));
        }

        private async void ShowPickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new PickerDialog();
                
                // XamlRoot is required for ContentDialog in WinUI 3
                picker.XamlRoot = this.Content.XamlRoot;
                
                // Subscribe to selection event
                picker.SelectedCountry += (s, country) =>
                {
                    SelectedCountryText.Text = $"Selected: {country.Name} (+{country.Code})";
                };

                picker.BackButtonClicked += (s) =>
                {
                    SelectedCountryText.Text = "Cancelled";
                };

                await picker.ShowAsync();
            }
            catch (Exception ex)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }

        private void ShowInFrameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Test loading the page directly in a frame
                ContentFrame.Navigate(typeof(CountryPicker.UWP.CountryPickerPage));
                SelectedCountryText.Text = "Page loaded in frame";
            }
            catch (Exception ex)
            {
                SelectedCountryText.Text = $"Error: {ex.Message}";
            }
        }
    }
}

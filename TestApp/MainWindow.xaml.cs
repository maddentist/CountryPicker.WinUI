using Microsoft.UI.Xaml;
using CountryPicker.UWP.Class;

namespace TestApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void ShowPickerButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new PickerDialog();
            
            // Set XamlRoot for ContentDialog (required in WinUI 3)
            picker.XamlRoot = this.Content.XamlRoot;
            
            // Subscribe to selection event
            picker.SelectedCountry += (s, country) =>
            {
                SelectedCountryText.Text = $"Selected: {country.Name} ({country.Code})";
            };

            await picker.ShowAsync();
        }
    }
}

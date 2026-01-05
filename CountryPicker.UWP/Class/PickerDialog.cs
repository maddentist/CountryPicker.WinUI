using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using CountryPicker.UWP.Class.Models;

namespace CountryPicker.UWP.Class
{
    public class PickerDialog : InitializeModel
    {
        public delegate void SelectedCountryEventHandler(object sender, CountryModel selected);

        public delegate void BackButtonPressedEventHandler(object sender);

        /// <summary>
        /// Event fire when user click country
        /// </summary>
        public event SelectedCountryEventHandler SelectedCountry;

        /// <summary>
        /// Event fire when Hardware back button pressed or Header back button was clicked.
        /// </summary>
        public event BackButtonPressedEventHandler BackButtonClicked;

        private ContentDialog _dialog;
        
        /// <summary>
        /// XamlRoot required for ContentDialog in WinUI 3
        /// Must be set before calling ShowAsync()
        /// </summary>
        public XamlRoot XamlRoot { get; set; }

        #region Properties

        private Style _style;

        /// <summary>
        /// Style for Content dialog
        /// </summary>
        public Style Style
        {
            get => _style;
            set => _style = value;
        }

        #endregion

        public PickerDialog()
        {
            SetEvents();
        }

        /// <summary>
        /// Contractor with select country
        /// </summary>
        /// <param name="countryName"></param>
        public PickerDialog(string countryName)
        {
            CountryName = countryName;
            SetEvents();
        }

        #region Private methods

        /// <summary>
        /// Set county picker events for fire class events
        /// </summary>
        private void SetEvents()
        {
            CountryPickerPage.ClearSelectedEvents();
            CountryPickerPage.SelectedCountryEvent += CountryPickerPageOnSelectedCountry;

            CountryPickerPage.ClearBackEvents();
            CountryPickerPage.BackButtonClickedEvent += CountryPickerPageOnBackButtonClicked;
        }

        private void CountryPickerPageOnBackButtonClicked(object sender)
        {
            BackButtonClicked?.Invoke(this);
            Hide();
        }

        private void CountryPickerPageOnSelectedCountry(object o, CountryModel selected)
        {
            CountryName = selected.Name;

            if (SelectedCountry != null) SelectedCountry.Invoke(o, selected);

            Hide();
        }

        private void Init()
        {
            _dialog = new ContentDialog()
            {
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(20, 50, 50, 50)),
                FullSizeDesired = true,
            };

            // Set XamlRoot for WinUI 3 ContentDialog
            if (XamlRoot != null)
            {
                _dialog.XamlRoot = XamlRoot;
            }

            var frame = new Frame();

            var countryPage = new CountryPickerPage(CountryName);
            countryPage.InitializeProperties(this);

            frame.Content = countryPage;

            _dialog.Content = frame;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Show picker dialog
        /// </summary>
        public async Task ShowAsync()
        {
            Init();

            if (_dialog != null)
            {
                if (Style != null) _dialog.Style = Style;

                await _dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Hide Picker dialog
        /// </summary>
        public void Hide()
        {
            if (_dialog != null)
            {
                _dialog.Hide();
                _dialog = null;
            }
        }

        #endregion
    }
}

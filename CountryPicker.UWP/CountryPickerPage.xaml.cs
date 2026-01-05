using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Dispatching;
using CountryPicker.UWP.Class;
using CountryPicker.UWP.Class.Models;

//Hussein.Juybari@gmail.com
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CountryPicker.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CountryPickerPage : Page
    {
        public delegate void SelectedCountryEventHandler(object sender, CountryModel selected);

        public delegate void BackButtonPressedEventHandler(object sender);

        /// <summary>
        /// Event fire when user click country
        /// </summary>
        public static event SelectedCountryEventHandler SelectedCountryEvent;

        /// <summary>
        /// Event fire when Hardware back button pressed or Header back button was clicked.
        /// </summary>
        public static event BackButtonPressedEventHandler BackButtonClickedEvent; 

        #region Properties

        public FlowDirection SearchBoxFlowDirection
        {
            get { return TxtSearchBox.FlowDirection; }
            set { TxtSearchBox.FlowDirection = value; }
        }

        private string _countryName;

        /// <summary>
        /// Select country with country name
        /// </summary>
        public string CountryName
        {
            get => _countryName;
            set
            {
                _countryName = value;
            }
        }

        /// <summary>
        /// Show picker header
        /// </summary>
        public bool ShowHeader
        {
            get { return BorderHeader.Visibility == Visibility.Visible; }
            set { BorderHeader.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        /// <summary>
        /// Text of header
        /// </summary>
        public string Header
        {
            get { return LblTitle.Text; }
            set { LblTitle.Text = value; }
        }

        /// <summary>
        /// Searchbar placeholder
        /// </summary>
        public string SearchBoxPlaceHolder
        {
            get { return TxtSearchBox.PlaceholderText; }
            set { TxtSearchBox.PlaceholderText = value; }
        }

        /// <summary>
        /// Header background
        /// </summary>
        public Brush HeaderBackground
        {
            get { return BorderHeader.Background; }
            set
            {
                BorderHeader.Background = value;
                TxtSearchBox.BorderBrush = value;
            }
        }

        public bool ShowBackButton
        {
            get {return BtnBackButton.Visibility == Visibility.Visible;}
            set { BtnBackButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string BackButtonText
        {
            get { return BtnBackButton.Content?.ToString(); }
            set { BtnBackButton.Content = value; }
        }
        #endregion

        public CountryPickerPage()
        {
            this.InitializeComponent();

            Loading += OnLoading;
            Loaded += OnLoaded;
        }

        public CountryPickerPage(string countryName)
        {
            this.InitializeComponent();

            CountryName = countryName;
            
            Loading += OnLoading;
            Loaded += OnLoaded;
        }

        #region Private event methods

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
        }

        private async void OnLoading(FrameworkElement sender, object args)
        {
            await LoadData(CountryName);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is InitializeModel)
            {
                var model = e.Parameter as InitializeModel;
                InitializeProperties(model);
            }
        }

        private void BtnBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            BackButtonClickedEvent?.Invoke(this);
        }

        private void CountryListViewOnItemClick(object sender, ItemClickEventArgs itemClickEventArgs)
        {
            var model = itemClickEventArgs.ClickedItem as CountryModel;
            SelectedCountryEvent?.Invoke(CountryListView, model);
        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only filter when user types (not when suggestion is chosen)
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                CountryVM.Source = CountryModel.GetCountries(sender.Text);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Load data and show them
        /// </summary>
        /// <param name="countryName"></param>
        private async Task LoadData(string countryName = "")
        {
            DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, async () =>
            {
                CountryVM.Source = CountryModel.GetCountries();

                CountryListView.ItemClick -= CountryListViewOnItemClick;
                CountryListView.ItemClick += CountryListViewOnItemClick;
                CountryListView.IsItemClickEnabled = true;

                DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
                {
                    CountryListView.SelectedIndex = CountryModel.GetCountryModelIndex(CountryName);
                    CountryListView.ScrollIntoView(CountryListView.SelectedItem);
                });
            });
            
            await Task.CompletedTask;
        }

        /// <summary>
        /// Initialize visual parameters.
        /// </summary>
        /// <param name="initialize">Initialize model</param>
        public void InitializeProperties(InitializeModel initialize)
        {
            if (!string.IsNullOrEmpty(initialize.SearchBoxPlaceHolder) ) SearchBoxPlaceHolder = initialize.SearchBoxPlaceHolder;

            if (!string.IsNullOrEmpty(initialize.Header)) Header = initialize.Header;
            ShowHeader = initialize.ShowHeader;
            if (initialize.HeaderBackground != null) HeaderBackground = initialize.HeaderBackground;
            
            if (!string.IsNullOrEmpty(initialize.BackButtonText)) BackButtonText = initialize.BackButtonText;
            ShowBackButton = initialize.ShowBackButton;

            CountryName = initialize.CountryName;

            SearchBoxFlowDirection = initialize.SearchBoxFlowDirection;
        }

        #endregion

        #region Public methods

        public static void ClearSelectedEvents()
        {
            SelectedCountryEvent = (SelectedCountryEventHandler)Delegate.RemoveAll(SelectedCountryEvent, SelectedCountryEvent);// Then you will find SomeEvent is set to null.
        }

        public static void ClearBackEvents()
        {
            BackButtonClickedEvent = (BackButtonPressedEventHandler)Delegate.RemoveAll(BackButtonClickedEvent, BackButtonClickedEvent);// Then you will find SomeEvent is set to null.
        }

        #endregion
    }
}

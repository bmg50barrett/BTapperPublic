using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.ViewManagement;
using Windows.Foundation;

namespace BTApper
{

    public sealed partial class MainPage : Page
    {
        private Windows.UI.Xaml.Controls.NavigationViewItem _lastitem;
        public MainPage()
        {
            ApplicationView.PreferredLaunchViewSize = new Size(1400, 1000);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(
                new Size(
                    500, // Width
                    500 // Height
                    )
                );
        }

        private void ContentFrame_NavigationFailed(object sender, Windows.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {

        }

        private void NavView_ItemInvoked(Windows.UI.Xaml.Controls.NavigationView sender, Windows.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as Windows.UI.Xaml.Controls.NavigationViewItem;
            if (item == null || item == _lastitem)
                return;

            var clickedView = item.Tag?.ToString() ?? "SettingsView";
            if (!NavigateToView(clickedView)) return;

            _lastitem = item;
        }

        private bool NavigateToView(String clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"BTApper.Views.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.IsPaneOpen = false;
            foreach (Windows.UI.Xaml.Controls.NavigationViewItemBase item in NavView.MenuItems)
            {
                if(item is Windows.UI.Xaml.Controls.NavigationViewItem && item.Tag.ToString() == "GatorView")
                {
                    NavView.SelectedItem = item;
                    break;
                }
                
            }
            ContentFrame.Navigate(typeof(BTApper.Views.GatorView));
        }

        private void NavView_SelectionChanged(Windows.UI.Xaml.Controls.NavigationView sender, Windows.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void NavView_BackRequested(Windows.UI.Xaml.Controls.NavigationView sender, Windows.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }
    }
}

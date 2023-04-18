using System;
using CommunityToolkit.Maui.Views;

namespace TrueSport
{
    public static class ContentPageExtensions
    {
        public static void SetViewModel(this ContentPage self, BaseViewModel viewModel)
        {
            self.BindingContext = viewModel;
            self.NavigatedTo += Self_NavigatedTo;
        }

        public static void SetViewModel(this Popup self, BaseViewModel viewModel)
        {
            self.BindingContext = viewModel;
            self.Opened += Self_Opened;
        }

        private static async void Self_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
        {
            if (sender is Popup page &&
                page.BindingContext is BaseViewModel viewModel)
            {
                page.Opened -= Self_Opened;
                await viewModel.InitializeAsync();
            }
        }

        static async void Self_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (sender is ContentPage page &&
                page.BindingContext is BaseViewModel viewModel)
            {
                page.NavigatedTo -= Self_NavigatedTo;
                await viewModel.InitializeAsync();
            }
        }
    }
}
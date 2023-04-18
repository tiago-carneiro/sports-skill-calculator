using System;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TrueSport
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool _isLoading;

        public void SetLoading(bool value)
            => IsLoading = value;

        public virtual Task InitializeAsync()
            => Task.CompletedTask;

        protected async Task ShowMessageAsync(string message)
            => await Toast.Make(message).Show();
    }
}
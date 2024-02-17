namespace TrueSport;

internal static class TaskExtensions
{
    const string NoInternetConnection = "Something went wrong with the internet connection, please try again later.";
    const string GenericError = "Something went wrong, please try again later";

    public static async Task<(bool Success, T Data)> Handle<T>(this Task<T> self, BaseViewModel vm = null)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Toast.Make(NoInternetConnection).Show();
            return (false, default(T));
        }

        vm?.SetLoading(true);

        try
        {
            var result = await self.ConfigureAwait(false);
            return (true, result);
        }
        catch (Exception ex)
        {
            LogHelper.Log(nameof(TaskExtensions), ex);
            await Toast.Make(GenericError).Show();
        }
        finally
        {
            vm?.SetLoading(false);
        }

        return (false, default(T));
    }

    public static async Task<bool> Handle(this Task self, BaseViewModel vm = null)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Toast.Make(NoInternetConnection).Show();
            return (false);
        }

        vm?.SetLoading(true);

        try
        {
            await self.ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {
            LogHelper.Log(nameof(TaskExtensions), ex);
            await Toast.Make(GenericError).Show();
        }
        finally
        {
            vm?.SetLoading(false);
        }

        return false;
    }
}
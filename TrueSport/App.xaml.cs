namespace TrueSport;
using System.Net.Http;
using System.Text;
using Flurl.Http;
using Flurl.Http.Content;

public partial class App : Application
{
    public App(AppShell root)
    {
        InitializeComponent();
        ConfigureFlurl();
        VersionTracking.Track();
        MainPage = root;
    }

    void ConfigureFlurl()
    {

#if DEBUG
        FlurlHttp.Configure(settings =>
       {
           settings.AfterCall = async (call) =>
           {
               var strLog = new StringBuilder();
               strLog.AppendLine($"ULR: {call.Request.Url.ToString()}");
               strLog.AppendLine($"token: {call.HttpRequestMessage?.Headers?.Authorization}");
               var request = (call.HttpRequestMessage.Content as CapturedStringContent)?.Content;
               strLog.AppendLine($"request: {request}");

               if (call.HttpResponseMessage != null)
               {
                   var response = await (call.HttpResponseMessage.Content as HttpContent)?.ReadAsStringAsync();
                   strLog.AppendLine($"response: {response}");
               }

               System.Console.WriteLine(strLog.ToString());
           };
       });
#endif

        FlurlHttp.GlobalSettings.HttpClientFactory = new PollyHttpClientFactory();
    }
}
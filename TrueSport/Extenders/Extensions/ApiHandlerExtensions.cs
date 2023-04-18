using Flurl.Http.Configuration;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace TrueSport
{
    public static class Policies
    {
        private static AsyncTimeoutPolicy<HttpResponseMessage> TimeoutPolicy
            => Policy.TimeoutAsync<HttpResponseMessage>(20, (context, timeSpan, task) =>
                {
                    LogHelper.Log("App|Policy", "Timeout delegate fired after {timeSpan.Seconds} seconds");
                    return Task.CompletedTask;
                });

        private static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy
            => Policy
                    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .Or<TimeoutRejectedException>()
                    .WaitAndRetryAsync(new[]
                        {
                            TimeSpan.FromSeconds(3),
                        },
                        (delegateResult, retryCount) =>
                        {
                            LogHelper.Log("[App|Policy]", "Retry delegate fired, attempt {retryCount}");
                        });

        public static AsyncPolicyWrap<HttpResponseMessage> PolicyStrategy
                => Policy.WrapAsync(RetryPolicy, TimeoutPolicy);
    }

    public class PollyHttpClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
            => new PolicyHandler { InnerHandler = base.CreateMessageHandler() };
    }

    public class PolicyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Policies.PolicyStrategy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
    }
}
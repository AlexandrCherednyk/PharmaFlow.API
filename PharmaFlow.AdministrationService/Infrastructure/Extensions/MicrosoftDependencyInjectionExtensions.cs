using ProtoBuf.Grpc.ClientFactory;

namespace PharmaFlow.AdministrationService.Infrastructure.Extensions;

public static class MicrosoftDependencyInjectionExtensions
{
    public static IHttpClientBuilder AddCodeFirstGrpcClient<TService>(
        this IServiceCollection services)
        where TService : class
    {
        return services
            .AddCodeFirstGrpcClient<TService>(options =>
            {
                options.Address = new Uri("http://pharmaflow.service:8080");
                options.ChannelOptionsActions.Add(o =>
                {
                    o.HttpHandler = new SocketsHttpHandler()
                    {
                        PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                        KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                        KeepAlivePingTimeout = TimeSpan.FromSeconds(30),

                        EnableMultipleHttp2Connections = true,
                    };
                });
            });
    }
}

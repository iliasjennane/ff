using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        private static string _appConfigConnectionString = "Endpoint=https://iliasjappconfig.azconfig.io;Id=+tVQ-l0-s0:TOp7JDHwPTzbKqXGAlmo;Secret=9AEq1lwkgLOoq/rwF5/5w8SLPd4ziemtX4rDBROldvg=";


        public static async Task Main(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddAzureAppConfiguration(options =>
                {
                    //options.Connect(Environment.GetEnvironmentVariable("ConnectionString")).UseFeatureFlags();
                    options.Connect(_appConfigConnectionString).UseFeatureFlags();
                }).Build();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration).AddFeatureManagement().AddFeatureFilter<ringDeploymentFilter>();

            FeatureFilterEvaluationContext context = new FeatureFilterEvaluationContext();
            ringDeploymentFilter ff = new ringDeploymentFilter();
            
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IFeatureManager featureManager = serviceProvider.GetRequiredService<IFeatureManager>();

                var enabled = await featureManager.IsEnabledAsync("ff_RefreshWeatherData", ff);
                if (enabled == true)
                {
                    Console.WriteLine("Welcome to the beta!");
                }
            }

            Console.WriteLine("Hello World!");
        }
    }

   
}
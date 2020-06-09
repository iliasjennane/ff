using System.Threading.Tasks;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration;
namespace TestConsole
{
    [FilterAlias("ringDeployment")]
    public class ringDeploymentFilter : Microsoft.FeatureManagement.IFeatureFilter
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var parameters = context.Parameters;
            var bla = parameters["ring0Users"];
            return Task.FromResult(true);
        }
    }

    public class ringDeploymentFilterContext
    {
        public string FeatureName { get; set; }
        public IConfiguration Parameters { get; set; }
    }
}   
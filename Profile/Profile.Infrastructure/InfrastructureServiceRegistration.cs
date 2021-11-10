using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Profile.Application.Contracts;
using Profile.Infrastructure.Repositories;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;

namespace Profile.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig()
            {
                ServiceURL = "http://localhost:8000",
                AuthenticationRegion = "eu-west-1"
            });
            services.AddSingleton<AmazonDynamoDBClient>(dbClient);
            var dbContext = new DynamoDBContext(dbClient);
            services.AddSingleton<DynamoDBContext>(dbContext);

            services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();            
            services.AddScoped<IProfileRepository, ProfileRepository>();

            return services;
        }
    }
}

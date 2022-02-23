using Bogus;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AuthService.Application.Test.Mocks
{
    public abstract class ConfigurationMock
    {
        static Faker faker = new("en");

        public static IConfigurationRoot CreateConfiguration()
        {
            var configurationOptions = new Dictionary<string, string>
            {
                { "Cryptography:Key", faker.Lorem.Letter(32) },
            };

            return new ConfigurationBuilder()
                    .AddInMemoryCollection(configurationOptions)
                    .Build();
        }
    }
}

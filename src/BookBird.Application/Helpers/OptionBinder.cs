using Microsoft.Extensions.Configuration;

namespace BookBird.Application.Helpers
{
    public static class OptionBinder
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName) 
            where TOptions : new()
        {
            var options = new TOptions();
            configuration.GetSection(sectionName).Bind(options);

            return options;
        }
    }
}
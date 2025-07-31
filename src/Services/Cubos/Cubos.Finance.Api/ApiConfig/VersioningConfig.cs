namespace Cubos.Finance.Api
{
    public static class VersioningConfig
    {
        public static WebApplicationBuilder AddVersioningConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                o.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });


            return builder;
        }
    }
}

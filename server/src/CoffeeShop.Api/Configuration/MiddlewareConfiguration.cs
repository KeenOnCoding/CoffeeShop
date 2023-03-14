namespace CoffeeShop.Api.Configuration
{
    public static class MiddlewareConfiguration
    {
        public static void UseSwagger(this IApplicationBuilder app, string endpointName)
        {
            app.UseSwagger();
            app.UseSwaggerOn("swagger", endpointName);
            app.UseSwaggerOn("openapi", endpointName);
        }

        private static void UseSwaggerOn(this IApplicationBuilder app, string route, string endpointName)
        {
            app.UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = route;

                setup.IndexStream = () => typeof(Program)
                    .Assembly
                    .GetManifestResourceStream("CoffeeShop.Api.Resources.Swagger.index.html");

                setup.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: endpointName);
            });
        }
    }
}
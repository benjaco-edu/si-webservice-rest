using System.ServiceModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace MiniprojectSoapService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITeacherService,TeacherServiceImpl>();
            services.AddSingleton<IRelationDb,RelationsDb>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSoapEndpoint<ITeacherService>("/service.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer );
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Server runnning - bottom of middleware stack reached");
            });
        }
    }
}

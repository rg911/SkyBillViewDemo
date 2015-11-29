using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
using System;
using System.Collections;
using System.IO;

namespace SkyBillViewer.Infrustructure.NancyHosting
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //get views from embedded resource
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var assembly = GetType().Assembly;
            ResourceViewLocationProvider.RootNamespaces.Add(assembly, "SignalFuseApiService.WebUi.Views");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder); }
        }

        static void OnConfigurationBuilder(NancyInternalConfiguration config)
        {
            config.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            //get css / js from embedded resources
            var assembly = GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var assemblyName = assembly.GetName().Name;
            nancyConventions.StaticContentsConventions.Add((context, path) =>
            {
                var directoryName = Path.GetDirectoryName(context.Request.Path);
                if (directoryName == null) return null;

                var filePath = assemblyName + directoryName.Replace(Path.DirectorySeparatorChar, '.').Replace("-", "_");
                var file = Path.GetFileName(context.Request.Path);
                var name = String.Concat(filePath, ".", file);

                return ((IList)resourceNames).Contains(name) ? new EmbeddedFileResponse(assembly, filePath, file) : null;
            });
            base.ConfigureConventions(nancyConventions);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            //enable cors - get/post for all origins at the minute as this will be used internally
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });

            base.RequestStartup(container, pipelines, context);
        }

    }
}

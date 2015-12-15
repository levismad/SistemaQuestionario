using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using ServiceStack.WebHost.Endpoints;
using SQ.Core.Repositorio;
using Funq;
using SQ.Core.Database;
using SQ.Core.Services;
using ServiceStack.Common.Web;

namespace SQ.Services
{
    public class AppHost
            : AppHostBase
    {
        //public AppHost() : base("SQ Web Service", typeof(HelloService).Assembly) { }
        public AppHost(string connectionString)
            : base("SQ Web Service", typeof(HelloService).Assembly)
        {
            this._connectionString = connectionString;
        }

        private string _connectionString;
        public override void Configure(Container container)
        {
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
            container.Register<IRepositorio<Hello>>(c => new RepositorioSQL<Hello>(_connectionString)).ReusedWithin(ReuseScope.None);

            container.Register(new TodoRepository());
            //Configure User Defined REST Paths
            Routes
              .Add<Hello>("/hello")
              .Add<Hello>("/hello/{Name*}");


        }
        
        /* Uncomment to enable ServiceStack Authentication and CustomUserSession
        private void ConfigureAuth(Funq.Container container)
        {
            var appSettings = new AppSettings();

            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(),
                new IAuthProvider[] {
                    new CredentialsAuthProvider(appSettings), 
                    new FacebookAuthProvider(appSettings), 
                    new TwitterAuthProvider(appSettings), 
                    new BasicAuthProvider(appSettings), 
                })); 

            //Default route: /register
            Plugins.Add(new RegistrationFeature()); 

            //Requires ConnectionString configured in Web.Config
            var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.CreateMissingTables();
        }
        */


    }
}
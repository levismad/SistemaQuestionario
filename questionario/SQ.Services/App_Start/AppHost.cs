using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using ServiceStack.WebHost.Endpoints;
using SQ.Core.Repositorio;
using Funq;
using SQ.Core.Database;

namespace SQ.Services.App_Start
{
public class AppHost
		: AppHostBase
	{
    public AppHost(string connectionString) //Tell ServiceStack the name and where to find your web services
        : base("SQ Web Service", typeof(AppHost).Assembly) {
            this._connectionString = connectionString;
        }

        private string _connectionString;
        public override void Configure(Container container)
        {
            container.Register<IRepositorio<Hello>>(c => new RepositorioSQL<Hello>(_connectionString)).ReusedWithin(ReuseScope.None);


        }
        public void Init()
        {
            base.Init();
        }

	}
}
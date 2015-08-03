﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bottles.Services;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace FubuTodo
{
    public class AppStart: IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            var fubuapp = FubuApplication.For<MyFubuAppRegistry>().StructureMap<MyStructureMapFubuAppRegistry>();
            //var fubuapp = FubuApplication.For<MyFubuAppRegistry>().StructureMap();
            return fubuapp;
        }

        

    }
    public class MyFubuAppRegistry : FubuRegistry
    {
        public MyFubuAppRegistry()
        {
            Actions.IncludeClassesSuffixedWithEndpoint();
            AlterSettings<DiagnosticsSettings>(x => x.TraceLevel = TraceLevel.Verbose);
        }
    }

    public class MyStructureMapFubuAppRegistry : StructureMapFubuRegistry
    {
        public MyStructureMapFubuAppRegistry()
        {
            IncludeRegistry<RavenDbRegistry>();
        }
    }

    public class RavenDbRegistry : Registry
    {
        public RavenDbRegistry()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "localhost" }.Initialize();
            For<IDocumentSession>().Use(() => documentStore.OpenSession());
            For<IDocumentStore>().Singleton().Use(documentStore);
        }
    }
}
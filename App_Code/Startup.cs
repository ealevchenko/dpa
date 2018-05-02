using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Hangfire.Dashboard;
using WebBase;
using System.Web.Hosting;

[assembly: OwinStartup(typeof(MyWebApplication.Startup))]

namespace MyWebApplication
{
    
    public class Startup
    {
        classHangfireJobs chfj = new classHangfireJobs();
        classHangfireJobsDB chfdb = new classHangfireJobsDB();

        public class MyRestrictiveAuthorizationFilter : IAuthorizationFilter
        {
            public bool Authorize(IDictionary<string, object> owinEnvironment)
            {
                // In case you need an OWIN context, use the next line,
                // `OwinContext` class is the part of the `Microsoft.Owin` package.
                var context = new OwinContext(owinEnvironment);

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                return context.Authentication.User.Identity.IsAuthenticated;
            }
        }

        public void Configuration(IAppBuilder app)
        {

            GlobalConfiguration.Configuration.UseSqlServerStorage("aspserver");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            // настроить фоновые задачи
            foreach (JobEntity je in chfj.ListJob)
            {
                if ((je.Cron != null) & (je.Enable))
                {
                    RecurringJob.AddOrUpdate("task-" + je.Metod.ToString(), () => chfj.StartJob(je), je.Cron.ToString());
                    chfdb.SaveStatus(je.IDJob, "Job добавлен");
                }
            }

        }

    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(empPayrollDemo.Startup))]
namespace empPayrollDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

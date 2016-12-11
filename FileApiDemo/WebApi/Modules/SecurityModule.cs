using System;
using System.Web;

namespace WebApi.Modules
{
    public class SecurityModule : IHttpModule
    {
        private static readonly string[] SecurityHeaders = new[] { "Server", "X-Powered-By", "X-AspNet-Version", "X-AspNetMvc-Version" };

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += new EventHandler(context_PreSendRequestHeaders);
        }

        private void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;

            foreach (string securityHeader in SecurityHeaders)
            {
                context.Response.Headers.Remove(securityHeader);
            }
        }
    }
}
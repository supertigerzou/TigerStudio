using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TigerStudio.API.Providers;
using TigerStudio.Users.Data;

namespace TigerStudio.API
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthrizationServerProvider(new AuthRepository())
            };
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "671830126309-drlernfdnh6n09oavvqlkv9rpk98p45k.apps.googleusercontent.com",
                ClientSecret = "FAA0hz8ssyL4x4Hawsf-HbRH",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleAuthOptions);
        }
    }
}

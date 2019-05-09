using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Keycloak;
using SSOAuth.Config;
using SSOAuth.Exception;
using System;
using System.Text;

[assembly: OwinStartup(typeof(SSOAuth.Startup))]

namespace SSOAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            try
            {
                bool isValid = ConfigManager.IsRequiredConfigValid();

                if (!isValid)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("As configurações mínimas requeridas para iniciar o Keycloak não foram atendidas,");
                    sb.AppendLine("revise o arquivo de configuração e verifique se as seguintes entradas foram criadas no web.config: [SSO.PersistentAuthType], [SSO.Realm], [SSO.ClientID], [SSO.KeycloakUrl]");
                    throw new SSOAuthInvalidParametersException(sb.ToString());
                }

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = ConfigManager.PersistentAuthType,
                    ExpireTimeSpan = TimeSpan.FromMinutes(ConfigManager.ExpireMinutes)
                });

                // You may also use this method if you have multiple authentication methods below,
                // or if you just like it better:
                app.SetDefaultSignInAsAuthenticationType(ConfigManager.PersistentAuthType);

                app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
                {
                    Realm = ConfigManager.Realm,
                    ClientId = ConfigManager.ClientID,
                    KeycloakUrl = ConfigManager.KeycloakUrl,
                    AuthenticationType = ConfigManager.PersistentAuthType,
                    SignInAsAuthenticationType = ConfigManager.PersistentAuthType,

                    //Token validation options - these are all set to defaults
                    AllowUnsignedTokens = ConfigManager.AllowUnsignedTokens,
                    DisableIssuerSigningKeyValidation = ConfigManager.DisableIssuerSigningKeyValidation,
                    DisableIssuerValidation = ConfigManager.DisableIssuerValidation,
                    DisableAudienceValidation = ConfigManager.DisableAudienceValidation,
                    TokenClockSkew = TimeSpan.FromSeconds(ConfigManager.TokenClockSkew),
                    UseRemoteTokenValidation = ConfigManager.UseRemoteTokenValidation,
                    MetadataRefreshInterval = ConfigManager.MetadataRefreshInterval,
                    PostLogoutRedirectUrl = ConfigManager.PostLogoutRedirectUrl,
                });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

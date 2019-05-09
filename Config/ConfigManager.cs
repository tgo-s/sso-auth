using System;
using System.Configuration;

namespace SSOAuth.Config
{
    public static class ConfigManager
    {
        public static string PersistentAuthType
        {
            get
            {
                return GetConfigValueClass<string>("PersistentAuthType", "");
            }
        }
        public static double ExpireMinutes
        {
            get
            {
                return GetConfigValueStruct<double>("ExpireMinutes", 30.0);
            }
        }
        public static string Realm
        {
            get
            {
                return GetConfigValueClass("Realm", "");
            }
        }
        public static string ClientID
        {
            get
            {
                return GetConfigValueClass("ClientID", "");
            }
        }
        public static string KeycloakUrl
        {
            get
            {
                return GetConfigValueClass("KeycloakUrl", "");
            }
        }

        public static bool AllowUnsignedTokens
        {
            get
            {
                return GetConfigValueStruct<bool>("AllowUnsignedTokens", false);
            }
        }
        public static bool DisableIssuerSigningKeyValidation
        {
            get
            {
                return GetConfigValueStruct<bool>("DisableIssuerSigningKeyValidation", false);
            }
        }
        public static bool DisableIssuerValidation
        {
            get
            {
                return GetConfigValueStruct<bool>("DisableIssuerValidation", false);
            }
        }
        public static bool DisableAudienceValidation
        {
            get
            {
                return GetConfigValueStruct<bool>("DisableAudienceValidation", false);
            }
        }
        public static int TokenClockSkew
        {
            get
            {
                return GetConfigValueStruct<int>("TokenClockSkew", 2);
            }
        }
        public static bool UseRemoteTokenValidation
        {
            get
            {
                return GetConfigValueStruct<bool>("UseRemoteTokenValidation", true);
            }
        }
        public static TimeSpan MetadataRefreshInterval
        {
            get
            {
                return GetConfigValueStruct<TimeSpan>("MetadataRefreshInterval", TimeSpan.Zero);
            }
        }
        public static string PostLogoutRedirectUrl
        {
            get
            {
                return (string)GetConfig("PostLogoutRedirectUrl");
            }
        }

        public static bool IsRequiredConfigValid()
        {
            bool isValid = false;

            if (!PersistentAuthType.IsNullOrEmpty() && !ExpireMinutes.IsNull()
                && !Realm.IsNullOrEmpty() && !ClientID.IsNullOrEmpty() && !KeycloakUrl.IsNullOrEmpty())
            {
                isValid = true;
            }

            return isValid;
        }
        private static T GetConfigValueClass<T>(string propName, T defaultValue) where T : class
        {
            T value = (T)GetConfig(propName);

            return value ?? defaultValue;
        }

        private static T GetConfigValueStruct<T>(string propName, T defaultValue) where T : struct
        {
            T? value = (T?)GetConfig(propName);

            return value ?? defaultValue;
        }
        private static object GetConfig(string propName)
        {
            propName = string.Format("SSO.{0}", propName);

            if (ConfigurationManager.AppSettings[propName] != null)
                return ConfigurationManager.AppSettings[propName];
            else
                return null;
        }
    }
}

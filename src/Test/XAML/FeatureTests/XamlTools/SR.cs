// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MS.Utility
{
    internal static class SR
    {
        internal static ResourceManager ResourceManager => SRID.ResourceManager;

        // This method is used to decide if we need to append the exception message parameters to the message when calling SR.Format.
        // by default it returns false.
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static bool UsingResourceKeys()
        {
            return false;
        }

        internal static string GetResourceString(string resourceKey, string defaultString)
        {
            string resourceString = null;
            try { resourceString = ResourceManager.GetString(resourceKey); }
            catch (MissingManifestResourceException) { }

            if (defaultString != null && resourceKey.Equals(resourceString, StringComparison.Ordinal))
            {
                return defaultString;
            }

            return resourceString;
        }

        internal static string Format(string resourceFormat, params object[] args)
        {
            if (args != null)
            {
                if (UsingResourceKeys())
                {
                    return resourceFormat + string.Join(", ", args);
                }

                return string.Format(resourceFormat, args);
            }

            return resourceFormat;
        }

        internal static string Format(string resourceFormat, object p1)
        {
            if (UsingResourceKeys())
            {
                return string.Join(", ", resourceFormat, p1);
            }

            return string.Format(resourceFormat, p1);
        }

        internal static string Format(string resourceFormat, object p1, object p2)
        {
            if (UsingResourceKeys())
            {
                return string.Join(", ", resourceFormat, p1, p2);
            }

            return string.Format(resourceFormat, p1, p2);
        }

        internal static string Format(string resourceFormat, object p1, object p2, object p3)
        {
            if (UsingResourceKeys())
            {
                return string.Join(", ", resourceFormat, p1, p2, p3);
            }

            return string.Format(resourceFormat, p1, p2, p3);
        }
        
        public static string Get(string id)
        {
             return SR.Format(id);
        }
        
        public static string Get(string id, params object[] args)
        {
             return SR.Format(id, args);
        }
    }
}

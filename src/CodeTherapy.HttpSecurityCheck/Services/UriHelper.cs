using System;

namespace CodeTherapy.HttpSecurityChecks.Services
{
    public sealed class UriHelper
    {
        public bool TryCreateUri(string uriString, out Uri uri, out string message)
        {
            message = null;
            if (Uri.TryCreate(uriString, UriKind.Absolute, out uri))
            {
                return ValidateUri(uri, out message);
            }
            else
            {
                message = $"{uriString ?? "null"} is not a valid absolute uri.";
            }
            return false;
        }

        public static void ValidateUri(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (!ValidateUri(uri, out string message))
            {
                throw new ArgumentException(message);
            }
        }

        public static bool ValidateUri(Uri uri, out string message)
        {
            message = null;
            if (!uri.IsAbsoluteUri)
            {
                message = "The uri cannot be a relative uri.";
                return false;
            }
            if (uri.IsFile)
            {
                message = "The uri cannot be a file.";
                return false;
            }
            if (uri.IsUnc)
            {
                message = "The uri cannot be a UNC path.";
                return false;
            }
            return true;
        }
    }
}

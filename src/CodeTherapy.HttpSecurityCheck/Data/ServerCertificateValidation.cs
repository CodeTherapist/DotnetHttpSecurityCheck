using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CodeTherapy.HttpSecurityChecks.Data
{
    public sealed class ServerCertificateInfo
    {
        public ServerCertificateInfo(string x509CertificateName, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
        {
            X509CertificateName = x509CertificateName    ?? throw new ArgumentNullException(nameof(x509CertificateName));
            X509Chain = x509Chain ?? throw new ArgumentNullException(nameof(x509Chain));
            SslPolicyErrors = sslPolicyErrors;
        }

        public string X509CertificateName { get;  }

        public X509Chain X509Chain { get;  }

        public SslPolicyErrors SslPolicyErrors { get;  }

    }
}

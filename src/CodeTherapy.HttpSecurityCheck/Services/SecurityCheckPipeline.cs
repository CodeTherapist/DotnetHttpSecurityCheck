using CodeTherapy.HttpSecurityChecks.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CodeTherapy.HttpSecurityChecks.Services
{
    public sealed class SecurityCheckPipeline : ISecurityCheckPipeline 
    {
        private readonly Lazy<HttpClient> _httpClient;

        public SecurityCheckPipeline(IEnumerable<ISecurityCheck> securityChecks)
        {
            if (securityChecks is null)
            {
                throw new ArgumentNullException(nameof(securityChecks));
            }

            _httpClient = new Lazy<HttpClient>(CreateHttpClient);
            SecurityChecks = new HashSet<ISecurityCheck>(securityChecks);
        }
        
        private HttpClient HttpClient => _httpClient.Value;

        private HashSet<ISecurityCheck> SecurityChecks { get; }

        /// <summary>
        /// Analyzes a HTTP response.
        /// </summary>
        /// <param name="httpResponse">The target response.</param>
        /// <returns>The results from the scan.</returns>
        public SecurityCheckPiplineResult Analyze(HttpResponseMessage httpResponse)
        {
            if (httpResponse is null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var result = new SecurityCheckPiplineResult();
            foreach (var securityCheck in SecurityChecks)
            {
                try
                {
                    var info = securityCheck.Check(httpResponse);
                    result.Add(securityCheck, info);
                }
                catch (Exception ex)
                {
                    result.Add(securityCheck, ex);
                }
            }
            return result;
        }

        /// <summary>
        /// Runs a scan.
        /// </summary>
        /// <param name="uri">The target uri.</param>
        /// <param name="beforeRequest">Before the request is sent.</param>
        /// <param name="afterRequest">After the request is sent.</param>
        /// <returns>The results from the scan.</returns>
        public SecurityCheckPiplineResult Run(Uri uri, Action<HttpRequestMessage> beforeRequest = null, Action<HttpResponseMessage> afterRequest =null)
        {
            UriHelper.ValidateUri(uri);
            var startTime = DateTimeOffset.Now; 
            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                beforeRequest?.Invoke(request);
                using (var response = HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult())
                {
                    afterRequest?.Invoke(response);
                    
                    var result = Analyze(response);
                    result.DateTime = startTime;
                    result.Url = uri.ToString();
                    return result;
                }
            }
        }

        
        private HttpClient CreateHttpClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                CheckCertificateRevocationList = true,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                ServerCertificateCustomValidationCallback =
                    (HttpRequestMessage httpRequestMessage,
                    X509Certificate2 certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors) =>
                    {                        
                        httpRequestMessage.Properties[nameof(ServerCertificateInfo)] 
                            = CreateServerCertificateValidationInfo(certificate, chain, sslPolicyErrors);
                        return true;
                    }
            };
            return new HttpClient(httpClientHandler);
        }

        private static ServerCertificateInfo CreateServerCertificateValidationInfo(X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            string friendlyName = string.Empty;
            if (!(certificate is null))
                if (!string.IsNullOrWhiteSpace(certificate.FriendlyName))
                {
                    friendlyName = certificate.FriendlyName;
                }
                else
                {
                    friendlyName = certificate.Subject;
                }
            return new ServerCertificateInfo(friendlyName, chain, sslPolicyErrors);
        }
    }
}

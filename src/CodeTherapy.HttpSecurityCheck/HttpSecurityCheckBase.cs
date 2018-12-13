using System;
using System.Collections.Generic;
using System.Net.Http;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public abstract class HttpSecurityCheckBase : ISecurityCheck, IEquatable<HttpSecurityCheckBase>
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Category { get; }

        public abstract string Recommendation { get; }

        public virtual bool HttpsOnly => false;

        public SecurityCheckResult Check(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage is null)
            {
                throw new ArgumentNullException(nameof(httpResponseMessage));
            }

            if (HttpsOnly && httpResponseMessage.RequestMessage.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                return SecurityCheckResult.Create(SecurityCheckState.Skipped, $"{Name} is HTTPS only.");
            }

            return CheckCore(httpResponseMessage);
        }

        

        protected abstract SecurityCheckResult CheckCore(HttpResponseMessage httpResponseMessage);

        public bool Equals(HttpSecurityCheckBase other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as HttpSecurityCheckBase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
            }
        }
        
        public static bool operator ==(HttpSecurityCheckBase obj1, HttpSecurityCheckBase obj2)
        {
            return EqualityComparer<HttpSecurityCheckBase>.Default.Equals(obj1, obj2);
        }

        public static bool operator !=(HttpSecurityCheckBase obj1, HttpSecurityCheckBase obj2)
        {
            return !(obj1 == obj2);
        }
    }
}

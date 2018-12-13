using CodeTherapy.HttpSecurityChecks.Data;
using System.Collections.Generic;
using System.Linq;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class ContentSecurityPolicySecurityHeaderCheck : MissingHeaderValueSecurityCheckBase
    {
        public override string Name => HeaderName;

        public override string HeaderName => "Content-Security-Policy";

        public override string Recommendation => $"Whitelisting sources of approved content. Example: \"{HeaderName}: default-src 'self'\"";

        public IReadOnlyCollection<string> ValidSrcs => new[]{
            "default-src",
            "child-src",
            "connect-src",
            "font-src",
            "frame-src",
            "img-src",
            "manifest-src",
            "media-src",
            "object-src",
            "prefetch-src",
            "script-src",
            "style-src",
            "worker-src",
        };

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => ValidSrcs.Any(vs => value.Contains(vs)))
        };
    }
}

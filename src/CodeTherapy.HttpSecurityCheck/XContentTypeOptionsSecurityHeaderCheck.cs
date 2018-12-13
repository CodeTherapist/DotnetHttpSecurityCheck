using System.Collections.Generic;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class XContentTypeOptionsSecurityHeaderCheck : MissingHeaderValueSecurityCheckBase
    {
        public override string Name => HeaderName;

        public override string HeaderName => "X-Content-Type-Options";
        
        public override string Recommendation => $"Recommended is \"{HeaderName}: nosniff\".";

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("nosniff")),
        };
    }
}

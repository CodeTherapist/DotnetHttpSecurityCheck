using System.Collections.Generic;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class XFrameOptionsSecurityHeaderCheck : MissingHeaderValueSecurityCheckBase
    {
        public override string Name => HeaderName;

        public override string HeaderName => "X-Frame-Options";
        
        public override string Recommendation => $"Recommended is \"{HeaderName}: deny\".";

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("deny")),
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("sameorigin"), recommandation: $"The page can only be displayed in a frame on the same origin as the page itself. Some browsers do not ensure that all ancestors are also in the same origin. {Recommendation}"),
            HeaderValueCheck.IsGood(when: value => value.StartsWithOrdinalIgnoreCase("allow-from"), recommandation: $"All ancestors are also in the same origin. Some browsers do not ensure that all ancestors are also in the same origin. {Recommendation}"),
        };
    }

    
}

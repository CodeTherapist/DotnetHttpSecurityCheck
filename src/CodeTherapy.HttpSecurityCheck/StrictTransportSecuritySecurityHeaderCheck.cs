using System.Collections.Generic;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class StrictTransportSecuritySecurityHeaderCheck : MissingHeaderValueSecurityCheckBase
    {
        public override string Name => HeaderName;

        public override string HeaderName => "Strict-Transport-Security";
        
        public override string Recommendation => $"Recommended is \"{HeaderName}: max-age=31536000; includeSubDomains\".";
        
        public override bool HttpsOnly => true;

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => value.StartsWithOrdinalIgnoreCase("max-age=") && value.Contains("includeSubDomains")),
            HeaderValueCheck.IsGood(when: value => value.StartsWithOrdinalIgnoreCase("max-age=") && value.Contains("preload"), recommandation: $".The optional value 'preload' is not part of the HSTS specification and should not be treated as official. {Recommendation}"),
            HeaderValueCheck.IsGood(when: value => value.StartsWithOrdinalIgnoreCase("max-age="), recommandation: $"All sub domains should be included. {Recommendation}"),
        };
    }

    
}

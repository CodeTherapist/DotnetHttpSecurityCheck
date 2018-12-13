using System.Collections.Generic;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class ReferrerPolicySecurityHeaderCheck : MissingHeaderValueSecurityCheckBase
    {
        public override string Name => HeaderName;

        public override string HeaderName => "Referrer-Policy";
        
        public override string Recommendation => "Recommended values: \"strict-origin\", \"strict-origin-when-cross-origin\" or \"no-referrer\".";

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("strict-origin-when-cross-origin")),
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("strict-origin")),
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("no-referrer")),
            
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("origin-when-cross-origin"), recommandation: $"Recommended value \"strict-origin-when-cross-origin\" to prevent leaking referrer data over an insecure connection."),
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("origin"), recommandation: $"Recommended value \"strict-origin\" to prevent leaking referrer data over an insecure connection."),
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("no-referrer-when-downgrade"), recommandation: Recommendation),
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("same-origin"), recommandation: Recommendation),

            HeaderValueCheck.IsBad(when: value => value.EqualsOrdinalIgnoreCase("unsafe-url"), recommandation: $"This policy will leak origins and paths from TLS-protected resources to insecure origins. {Recommendation}."),
        };
    }    
}

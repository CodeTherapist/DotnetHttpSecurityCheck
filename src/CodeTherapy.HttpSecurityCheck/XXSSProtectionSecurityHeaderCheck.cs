using System;
using System.Collections.Generic;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class XXSSProtectionSecurityHeaderCheck : MissingHeaderValueSecurityCheckBase, IEquatable<XXSSProtectionSecurityHeaderCheck>
    {
        public override string Name => HeaderName;

        public override string HeaderName => "X-XSS-Protection";

        public override string Recommendation => "Recommended is \"X-XSS-Protection: 1;\".";

        public override IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks => new[]
        {
            HeaderValueCheck.IsBest(when: value => value.EqualsOrdinalIgnoreCase("1; mode=block")),
            HeaderValueCheck.IsGood(when: value => value.EqualsOrdinalIgnoreCase("1"), recommandation: $"By abusing false-positives, attackers can selectively disable innocent scripts on the page. {Recommendation}"),
            HeaderValueCheck.IsBad(when: value => value.StartsWithOrdinalIgnoreCase("1; report="), recommandation: $"This is only supported on Chromium. By abusing false-positives, attackers can selectively disable innocent scripts on the page. {Recommendation}"),
            HeaderValueCheck.IsBad(when: value => value.EqualsOrdinalIgnoreCase("0"), recommandation: $"Value 0 disables XSS filtering. {Recommendation}")
        };

        public bool Equals(XXSSProtectionSecurityHeaderCheck other)
        {
           return base.Equals(other);
        }
    }
}

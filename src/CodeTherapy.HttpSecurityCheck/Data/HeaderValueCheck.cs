using System;

namespace CodeTherapy.HttpSecurityChecks.Data
{
    public sealed class HeaderValueCheck
    {
        private readonly Predicate<string> _when;

        public HeaderValueCheck()
        {
        }

        public HeaderValueCheck(SecurityCheckState securityCheckState, Predicate<string> when, string recommendation = "")
        {
            _when = when;
            SecurityCheckState = securityCheckState;
            Recommendation = recommendation;
        }

        
        public bool Matches(string value)
        {
            return _when(value);
        }

        public SecurityCheckState SecurityCheckState { get; private set; }

        public string Recommendation { get; private set; }

        public static HeaderValueCheck IsBad(Predicate<string> when, string recommandation)
        {
            return new HeaderValueCheck(SecurityCheckState.Bad, when, recommandation);
        }

        public static HeaderValueCheck IsGood(Predicate<string> when, string recommandation)
        {
            return new HeaderValueCheck(SecurityCheckState.Good, when, recommandation);
        }

        public static HeaderValueCheck IsBest(Predicate<string> when)
        {
            return new HeaderValueCheck(SecurityCheckState.Best, when);
        }


    }

}

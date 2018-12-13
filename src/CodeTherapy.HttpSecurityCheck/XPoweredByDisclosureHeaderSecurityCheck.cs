namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class XPoweredByDisclosureHeaderSecurityCheck : DisclosureHeaderValueSecurityCheck
    {
        public override string Name => HeaderName;
        
        public override string HeaderName => "X-Powered-By";

        public override string Recommendation => "Technology information should be removed.";
    }


}

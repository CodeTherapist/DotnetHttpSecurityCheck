namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class ServerDisclosureHeaderValueSecurityCheck : DisclosureHeaderValueSecurityCheck
    {
        public override string Name => HeaderName;

        public override string HeaderName => "Server";
        
        public override string Recommendation => "Server information should be removed.";
    }


}

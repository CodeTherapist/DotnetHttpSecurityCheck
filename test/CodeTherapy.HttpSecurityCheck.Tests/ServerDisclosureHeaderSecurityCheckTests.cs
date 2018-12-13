namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class ServerDisclosureHeaderSecurityCheckTests : DisclosureHeaderSecurityCheckTestBase<ServerDisclosureHeaderValueSecurityCheck>
    {
        public override string HeaderName => "server";
    }
}

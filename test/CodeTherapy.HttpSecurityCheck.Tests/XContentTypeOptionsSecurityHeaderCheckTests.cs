namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class XContentTypeOptionsSecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<XContentTypeOptionsSecurityHeaderCheck>
    {
        public override string HeaderName => "x-content-type-options";
    }
}

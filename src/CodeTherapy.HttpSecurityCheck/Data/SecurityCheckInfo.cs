namespace CodeTherapy.HttpSecurityChecks.Data
{
    public sealed class SecurityCheckResult
    {
        public static SecurityCheckResult Empty = new SecurityCheckResult(SecurityCheckState.None, string.Empty);

        private SecurityCheckResult(SecurityCheckState state,  string recommandation, string value = "")
        {
            State = state;
            Recommandation = recommandation;
            Value = value;
        }
        
        public SecurityCheckState State { get; }
        
        public string Recommandation { get; }

        public bool HasRecommandation => !string.IsNullOrWhiteSpace(Recommandation);

        public string Value { get; }
        
        public static SecurityCheckResult Create(SecurityCheckState state, string recommandation = "", string value = "") => new SecurityCheckResult(state, recommandation, value);
    }
}

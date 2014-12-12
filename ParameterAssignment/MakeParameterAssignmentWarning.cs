using JetBrains.ReSharper.Feature.Services.CSharp.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ParameterAssignment
{
    [StaticSeverityHighlighting(Severity.WARNING, "Unsafe Assignment")]
    public class MakeParameterAssignmentWarning : CSharpHighlightingBase, IHighlighting
    {
        private IAssignmentExpression AssignmentExpression { get; set; }
        public MakeParameterAssignmentWarning(IAssignmentExpression memberAssignmentExpression)
        {
            AssignmentExpression = memberAssignmentExpression;
        }

        public string ToolTip
        {
            get { return "Parameter variable should not be assigned to."; }
        }

        public string ErrorStripeToolTip
        {
            get { return ToolTip; }
        }

        public override bool IsValid()
        {
            return AssignmentExpression.IsValid();
        }

        public int NavigationOffsetPatch
        {
            get { return 0; }
        }
    }
}
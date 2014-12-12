using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ParameterAssignment
{
    [QuickFix]
    public class Fix : QuickFixBase
    {
        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            return null;
        }

        public override string Text
        {
            get { return "Introduce Local Variable to replace Parameter Variable"; }
        }

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return false;
        }
    }
}

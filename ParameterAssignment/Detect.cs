using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Feature.Services.LinqTools;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace ParameterAssignment
{
    [UsedImplicitly]
    public class Detect : IDaemonStageProcess
    {
        public IDaemonProcess DaemonProcess { get; private set; }

        public Detect(IDaemonProcess process)
        {
            DaemonProcess = process;
        }
 
        public void Execute(Action<DaemonStageResult> committer)
        {
            if (DaemonProcess.InterruptFlag)
            {
                return;
            }
            var rangeMarker = DaemonProcess.VisibleRange.CreateRangeMarker(DaemonProcess.Document);
            //Retrieve the project file
            //(using the daemon we stored in the constructor)
            var file = DaemonProcess.SourceFile.GetPsiFile<CSharpLanguage>(new DocumentRange(rangeMarker)) as ICSharpFile;

            if (file != null)
            {
                var highlights = new List<HighlightingInfo>();

                var methodProcessor = new RecursiveElementProcessor<IMethodDeclaration>(method =>
                {
                    var parameterVariables = method.Params;

                    var assignmentsProcessor = new RecursiveElementProcessor<IAssignmentExpression>(assignment =>
                    {
                        var variableAssignedto = assignment.ReferenceExpressionTarget();
                        var valid = true;

                        //Check against all parameters
                        foreach (var parameterVariable in parameterVariables.ParameterDeclarationsEnumerable)
                        {
                            if (parameterVariable.DeclaredElement != null && parameterVariable.DeclaredElement.Equals(variableAssignedto))
                                valid = false;
                        }
                        if (valid) return;

                        //Assign to parameter variable detected!
                        var docRange = assignment.GetDocumentRange();

                        highlights.Add(new HighlightingInfo(docRange, new MakeParameterAssignmentWarning(assignment)));
                    });

                    method.ProcessDescendants(assignmentsProcessor);
                });

                file.ProcessDescendants(methodProcessor);

                committer(new DaemonStageResult(highlights));
            }
        }
    }
}

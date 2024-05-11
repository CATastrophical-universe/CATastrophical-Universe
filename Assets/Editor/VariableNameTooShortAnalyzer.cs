using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil.Cil;
using Unity.ProjectAuditor.Editor;
using Unity.ProjectAuditor.Editor.Core;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

class VariableNameTooShortAnalyzer : AssetsModuleAnalyzer
{
    const string k_ShortVariableNameId = "PAA5000";

    // Define the minimum allowed variable name length.
    const int k_MinVariableNameLength = 10;

    // Data for constructing a descriptor.
    const string k_Title = "(Custom) Short Variable Name";
    const Areas k_ImpactedAreas = Areas.Quality;
    const string k_Description =
        "A variable name has a length less than the recommended minimum. " +
        "Short variable names can be less descriptive and harder to understand.";
    const string k_Recommendation =
        "Consider using more descriptive names for your variables with a minimum characters.";

    // Declare a Descriptor to describe the issue we want to report.
    static readonly Descriptor k_ShortVariableNameDescriptor = new Descriptor
    (
        k_ShortVariableNameId,
        k_Title,
        k_ImpactedAreas,
        k_Description,
        k_Recommendation
    )
    {
        // Project Auditor will format this message using the arguments passed into
        // CreateIssue.
        MessageFormat = "Variable '{0}' has a name length of {1}, consider using a longer name"
    };

    public override void Initialize(Action<Descriptor> registerDescriptor)
    {
        registerDescriptor(k_ShortVariableNameDescriptor);
    }


    // Override the Analyze method to perform analysis on the provided asset.
    public override IEnumerable<ReportItem> Analyze(AssetAnalysisContext context)
    {
        if (Path.GetExtension(context.AssetPath) == ".cs" && !context.AssetPath.Contains("Packages/")) {
            string obsolutePath = Path.GetFullPath(context.AssetPath);
            string fileContents = File.ReadAllText(obsolutePath);

            string pattern = @"\b(?:public|private|protected|internal)?\s+(?:static\s+)?(?:readonly\s+)?(?:volatile\s+)?([\w<>,]+)\s+(\w+)\s*=(?!=)";

            MatchCollection matches = Regex.Matches(fileContents, pattern);

            foreach (Match match in matches) {
                string variableType = match.Groups[1].Value.Trim();
                string variableName = match.Groups[2].Value.Trim();

                int variableNameLength = variableName.Length;
                Debug.Log(string.Format("{0} {1} {2} {3} {4}", context.AssetPath, variableName, variableNameLength, k_MinVariableNameLength, variableNameLength < k_MinVariableNameLength));

                if (variableNameLength < k_MinVariableNameLength) {
                    yield return context.CreateIssue(IssueCategory.AssetIssue, k_ShortVariableNameDescriptor.Id, variableName, variableNameLength)
                        .WithLocation(context.AssetPath);
                }
            }
        }
    }
}

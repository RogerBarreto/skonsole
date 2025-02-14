﻿using System.ComponentModel;
using System.Diagnostics;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace SKonsole.Skills;

internal sealed class GitSkill
{
    [SKFunction, Description("Run 'git diff --staged' and return it's output.")]
    public static async Task<SKContext> GitDiffStaged(SKContext context,
        CancellationToken cancellationToken = default)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "diff --staged",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8
            }
        };
        process.Start();

        string output = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        context.Variables.Update(output);
        return context;
    }
}

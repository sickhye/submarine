﻿using UnityEngine;
using UnityEditor;

public static class BuildScript
{
    public static void ExecuteViaCommandLine()
    {
        var args = new CommandLineCustomArgs();
        var settings = new BuildSettings(args);
        Execute(settings);
    }

    static void Execute(BuildSettings settings)
    {
        Debug.Log("buildTarget: " + settings.BuildTarget);
        Debug.Log("bundleIdentifier" + settings.BundleIdentifier);
        Debug.Log("bundleVersion" + settings.BundleVersion);
        Debug.Log("productName" + settings.ProductName);

        PlayerSettings.bundleIdentifier = settings.BundleIdentifier;
        PlayerSettings.bundleVersion = settings.BundleVersion;
        PlayerSettings.productName = settings.ProductName;

        Debug.Log("Build Start");

        var errorMessage = BuildPipeline.BuildPlayer(
            settings.SceneNames,
            settings.OutputPath,
            settings.BuildTarget,
            BuildOptions.Development | BuildOptions.AllowDebugging
        );

        if (string.IsNullOrEmpty(errorMessage))
        {
            Debug.Log("Build Succeeded");
        }
        else
        {
            Debug.LogError(errorMessage);
        }
    }
}
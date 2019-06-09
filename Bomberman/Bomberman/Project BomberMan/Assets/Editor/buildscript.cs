using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

class BuildScript {
	static string[] SCENES = FindEnabledEditorScenes();

	static string APP_NAME = "Pikachu";
	static string TARGET_DIR = "target";

	static void PerformAllBuilds ()
	{
		PerformWindowsBuild ();
		PerformAndroidBuild();
	}

	static void PerformWindowsBuild ()
	{
		string target_dir = APP_NAME + ".exe";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows,BuildOptions.None);
	}

	static void PerformAndroidBuild ()
	{
		//Set the path to the Android SDK on the machine, since Unity cannot retain the state properly
        // TODO if android SDK is setup,
		/*AndroidSDKFolder.Path = "${ANDROID_HOME}";
		string target_dir = APP_NAME + ".apk";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.Android,BuildOptions.None);*/
	}

	private static string[] FindEnabledEditorScenes() 
	{
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) 
		{
			if (!scene.enabled) 
			{
				continue;
			}
			
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}

	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, build_target);
		BuildReport report = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		
		if (report == null) 
		{
			throw new Exception("BuildPlayer failure: " );
		}
	}
}
using UnityEngine;
using UnityEditor;

public class GitAboutPanel : GitPanel {
  private GUIContent gitShellVersion = new GUIContent("UnityGit " + GitShell.VERSION);
  private GUIContent gitShellCopyright = new GUIContent("(C)Copyright 2010 MrJoy, Inc.");
  private GUIContent gitShellLink = new GUIContent("http://github.com/MrJoy/UnityGit");
  private GUIContent gitVersion = null, gitBinary = null;
  private bool cantFindGit = false;

  protected void LoadInfo() {
    if(gitVersion == null) {
      try {
        gitVersion = new GUIContent("Git Version: " + ShellHelpers.OutputFromCommand("git", "--version").Replace("git version ", "").Replace("\n", ""));
        gitBinary = new GUIContent("Git Binary: " + ShellHelpers.OutputFromCommand("which", "git").Replace("\n", ""));
        cantFindGit = false;
      } catch {
        gitVersion = new GUIContent("Git Version:");
        gitBinary = new GUIContent("Git Binary: Can't find git binary!");
        cantFindGit = true;
      }
    }
  }

  public override void OnEnable() {
    LoadInfo();
  }

  public override void OnGUI() {
    GUILayout.Label(gitShellVersion, GitStyles.BoldLabel);
    GUILayout.Label(gitShellCopyright, GitStyles.MiniLabel);
    LinkTo(gitShellLink, gitShellLink.text);

    EditorGUILayout.Separator();

    Color c = GUI.color;
    GUI.color = (cantFindGit) ? Color.red : Color.black;
    GUILayout.Label(gitVersion, GitStyles.WhiteLabel);
    GUILayout.Label(gitBinary, GitStyles.WhiteLabel);
    GUI.color = c;
  }
}

using UnityEngine;
using UnityEditor;

public class GitAboutPanel : GitPanel {
  public override bool IsDisabledForError { get { return false; } }

  private GUIContent gitShellVersion = new GUIContent("UnityGit " + GitShell.VERSION);
  private GUIContent gitShellCopyright = new GUIContent(GitShell.COPYRIGHT);
  private GUIContent gitShellLink = new GUIContent("http://github.com/MrJoy/UnityGit");
  private GUIContent gitVersion = null, gitBinary = null;
  private bool cantFindGit = false;

  protected void LoadInfo() {
    if(gitVersion == null) {
      gitVersion = new GUIContent("Git Version: " + GitWrapper.Version);
      gitBinary = new GUIContent("Git Binary: " + GitWrapper.GitBinary);
      if(!GitWrapper.IsWorking) {
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
    GUI.color = (cantFindGit) ? GitStyles.ErrorColor : GitStyles.TextColor;
    GUILayout.Label(gitVersion, GitStyles.WhiteLabel);
    GUILayout.Label(gitBinary, GitStyles.WhiteLabel);
    GUI.color = c;
  }

  // Base constructor
  public GitAboutPanel(GitShell owner) : base(owner) {}
}

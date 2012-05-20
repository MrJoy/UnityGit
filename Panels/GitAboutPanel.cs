using UnityEditor;
using UnityEngine;

public class GitAboutPanel : GitPanel {
  public override bool IsDisabledForError { get { return false; } }

  private GUIContent gitShellVersion = new GUIContent("UnityGit " + GitShell.VERSION);
  private GUIContent gitShellCopyright = new GUIContent(GitShell.COPYRIGHT);
  private GUIContent gitShellLink = new GUIContent("http://github.com/MrJoy/UnityGit");
  private string gitVersion = null, gitBinary = null, gitProjectStatus = null, gitExtraStatus = null;

  public override void OnRefresh() {
    gitVersion = "Git Version: " + GitWrapper.Version;
    gitBinary = "Git Binary: " + GitWrapper.GitBinary;
    gitProjectStatus = "Project: Your Unity project is configured ideally.";
    gitExtraStatus = null;
    if(!GitWrapper.IsUsable) {
      if(!GitWrapper.IsGitPresent) {
        gitVersion = "Git Version: N/A";
        gitBinary = "Git Binary: Can't find git binary!";
      }
      if(!GitWrapper.IsVersioningEnabled)
        gitProjectStatus = "Project: You must set 'version control' to 'meta files' in the 'Editor' project panel!";
      else if(!GitWrapper.IsVersioningIdeal)
        gitProjectStatus = "Project: It's recommended you set 'asset serialization' to 'force text' in the 'Editor' project panel!";
      if(!GitWrapper.IsWorking)
        gitExtraStatus = "Had problems executing git commands!";
    }
  }

  public override void OnEnable() {
    OnRefresh();
  }

  public override void OnGUI() {
    GUILayout.Label(gitShellVersion, GitStyles.BoldLabel);
    GUILayout.Label(gitShellCopyright, GitStyles.MiniLabel);
    LinkTo(gitShellLink, gitShellLink.text);

    EditorGUILayout.Separator();

    EditorGUILayout.HelpBox(gitVersion, (GitWrapper.Version == null) ? MessageType.Error : MessageType.Info, true);
    EditorGUILayout.HelpBox(gitBinary, (GitWrapper.GitBinary == null) ? MessageType.Error : MessageType.Info, true);
    MessageType t;
    if(GitWrapper.IsVersioningEnabled) {
      if(GitWrapper.IsVersioningIdeal)
        t = MessageType.Info;
      else
        t = MessageType.Warning;
    } else {
      t = MessageType.Error;
    }
    EditorGUILayout.HelpBox(gitProjectStatus, t, true);
    if(gitExtraStatus != null)
      EditorGUILayout.HelpBox(gitExtraStatus, MessageType.Error, true);
  }

  // Base constructor
  public GitAboutPanel(GitShell owner) : base(owner) {}
}

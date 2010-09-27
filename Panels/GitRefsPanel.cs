using UnityEngine;
using UnityEditor;

public class GitRefsPanel : GitPanel {
  protected GitWrapper.Ref[] refs = null;

  public override void OnRefresh() {
    refs = GitWrapper.Refs;
  }

  public override void OnEnable() {
    OnRefresh();
  }

  private static GUIContent BRANCHES_LABEL = new GUIContent("Branches:");
  public override void OnGUI() {
    string currentBranch = GitWrapper.CurrentBranch;

    GUILayout.Label(BRANCHES_LABEL, GitStyles.BoldLabel);
    GUILayout.BeginVertical(GitStyles.FileListBox);
      Color c = GUI.contentColor;
      for(int i = 0; i < refs.Length; i++) {
        if(refs[i].kind == GitWrapper.RefKind.Branch) {
          GUI.contentColor = (currentBranch == refs[i].shortName) ? GitStyles.CurrentBranchColor : GitStyles.BranchColor;
          GUILayout.Label(refs[i].shortName, GitStyles.WhiteLargeLabel);
          GUI.contentColor = c;
        }
      }
    GUILayout.EndVertical();
  }

  // Base constructor
  public GitRefsPanel(GitShell owner) : base(owner) {}
}

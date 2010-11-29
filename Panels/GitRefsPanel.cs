using UnityEditor;
using UnityEngine;

public class GitRefsPanel : GitPanel {
  protected GitWrapper.Ref[] refs = null;

  public override void OnRefresh() {
    refs = GitWrapper.Refs;
  }

  public override void OnEnable() {
    OnRefresh();
  }

  private static GUIContent BRANCHES_LABEL = new GUIContent("Local Branches:"),
                            REMOTES_LABEL = new GUIContent("Remote Branches:"),
                            TAGS_LABEL = new GUIContent("Tags:");

  protected static Vector2 BeginList(GUIContent heading, Vector2 pos) {
    GUILayout.Label(heading, GitStyles.BoldLabel);
    return EditorGUILayout.BeginScrollView(pos, GitStyles.FileListBox);
  }

  protected static void EndList() {
    GUILayout.Label(GUIHelper.NoContent, GUIHelper.ExpandHeight);
    EditorGUILayout.EndScrollView();
  }

  private Vector2 branchesPos, remotesPos, tagsPos;

  public override void OnGUI() {
    string currentBranch = GitWrapper.CurrentBranch;
    Color c = GUI.contentColor;

    GUILayout.BeginVertical(GUIHelper.ExpandWidth, GUIHelper.ExpandHeight);
      int counter = 0;
      branchesPos = BeginList(BRANCHES_LABEL, branchesPos);
        counter = 0;
        for(int i = 0; i < refs.Length; i++) {
          if(refs[i].kind == GitWrapper.RefKind.Branch) {
            counter++;
            GUI.contentColor = (currentBranch == refs[i].shortName) ? GitStyles.CurrentBranchColor : GitStyles.BranchColor;
            GUILayout.Label(refs[i].shortName, GitStyles.WhiteLargeLabel);
            GUI.contentColor = c;
          }
        }
        if(counter == 0) GUILayout.Label(GUIHelper.NoContent, GUIHelper.ExpandHeight);
      EndList();

      Space();

      remotesPos = BeginList(REMOTES_LABEL, remotesPos);
        counter = 0;
        for(int i = 0; i < refs.Length; i++) {
          if(refs[i].kind == GitWrapper.RefKind.TrackingBranch) {
            counter++;
            GUI.contentColor = GitStyles.BranchColor;
            GUILayout.Label(refs[i].shortName, GitStyles.WhiteLargeLabel);
            GUI.contentColor = c;
          }
        }
        if(counter == 0) GUILayout.Label(GUIHelper.NoContent, GUIHelper.ExpandHeight);
      EndList();

      Space();

      tagsPos = BeginList(TAGS_LABEL, tagsPos);
        counter = 0;
        for(int i = 0; i < refs.Length; i++) {
          if(refs[i].kind == GitWrapper.RefKind.Tag) {
            counter++;
            GUI.contentColor = GitStyles.BranchColor;
            GUILayout.Label(refs[i].shortName, GitStyles.WhiteLargeLabel);
            GUI.contentColor = c;
          }
        }
        if(counter == 0) GUILayout.Label(GUIHelper.NoContent, GUIHelper.ExpandHeight);
      EndList();
    GUILayout.EndVertical();
  }

  // Base constructor
  public GitRefsPanel(GitShell owner) : base(owner) {}
}

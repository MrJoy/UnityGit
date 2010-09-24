using UnityEngine;
using UnityEditor;

public class GitStatusPanel : GitPanel {
  private GUIContent currentBranchLabel = null;
  private string currentBranch = null;

  private bool isDetachedHeadMode = false;

  private GitWrapper.Change[] changes = null;

  public void Refresh() {
    currentBranch = GitWrapper.CurrentBranch;
    if(currentBranch == "") {
      currentBranchLabel = new GUIContent("Detached HEAD Mode");
      isDetachedHeadMode = true;
    } else {
      currentBranchLabel = new GUIContent(currentBranch);
      isDetachedHeadMode = false;
    }

    changes = GitWrapper.Status;
  }

  public override void OnEnable() {
    Refresh();
  }

  private Vector2 workingScrollPos;
  private void ShowUnstagedChanges() {
    Color c = GUI.color;
    GUILayout.Label("Unstaged Changes:", GitStyles.BoldLabel, NoExpandWidth);
    workingScrollPos = EditorGUILayout.BeginScrollView(workingScrollPos, GitStyles.FileListBox);
      if(changes != null) {
        for(int i = 0; i < changes.Length; i++) {
          if(changes[i].workingStatus != GitWrapper.ChangeType.Unmodified) {
            switch(changes[i].workingStatus) {
              case GitWrapper.ChangeType.Modified:  GUI.color = GitStyles.ModifiedColor; break;
              case GitWrapper.ChangeType.Deleted:   GUI.color = GitStyles.DeletedColor; break;
              case GitWrapper.ChangeType.Untracked: GUI.color = GitStyles.UntrackedColor; break;
              default: 
                GUI.color = Color.red;
                Debug.Log("Should not have gotten this status for working set: " + changes[i].workingStatus);
                break;
            }
            GUILayout.Label(changes[i].path, GitStyles.WhiteLargeLabel);
            GUI.color = c;
          }
        }
      }
    EditorGUILayout.EndScrollView();
  }

  private Vector2 indexScrollPos;
  private void ShowStagedChanges() {
    Color c = GUI.color;
    GUILayout.Label("Staged Changes (Will Commit):", GitStyles.BoldLabel, NoExpandWidth);
    indexScrollPos = EditorGUILayout.BeginScrollView(indexScrollPos, GitStyles.FileListBox);
      if(changes != null) {
        for(int i = 0; i < changes.Length; i++) {
          if(changes[i].indexStatus != GitWrapper.ChangeType.Unmodified && changes[i].indexStatus != GitWrapper.ChangeType.Untracked) {
            switch(changes[i].indexStatus) {
              case GitWrapper.ChangeType.Modified:  GUI.color = GitStyles.ModifiedColor; break;
              case GitWrapper.ChangeType.Added:     GUI.color = GitStyles.AddedColor; break;
              case GitWrapper.ChangeType.Deleted:   GUI.color = GitStyles.DeletedColor; break;
              case GitWrapper.ChangeType.Renamed:   GUI.color = GitStyles.RenamedColor; break;
              case GitWrapper.ChangeType.Copied:    GUI.color = GitStyles.CopiedColor; break;
              default: 
                GUI.color = Color.red;
                Debug.Log("Should not have gotten this status for index set: " + changes[i].indexStatus);
                break;
            }
            GUILayout.Label(changes[i].path, GitStyles.WhiteLargeLabel);
            GUI.color = c;
          }
        }
      }
    EditorGUILayout.EndScrollView();
  }

  public override void OnGUI() {
    if(GUILayout.Button("Refresh"))
      Refresh();

    Color c = GUI.color;
    GUILayout.BeginHorizontal();
      GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
          GUILayout.Label("Current Branch:", GitStyles.BoldLabel, NoExpandWidth);
          GUILayout.Space(SizeOfSpace(GitStyles.BoldLabel));
          GUI.color = isDetachedHeadMode ? GitStyles.ErrorColor : GitStyles.TextColor;
          GUILayout.Label(currentBranchLabel, GitStyles.WhiteBoldLabel, NoExpandWidth);
          GUI.color = c;
        GUILayout.EndHorizontal();
        EditorGUILayout.Separator();

        ShowUnstagedChanges();
        EditorGUILayout.Separator();

        ShowStagedChanges();
      GUILayout.EndVertical();
      GUILayout.BeginVertical();
        GUILayout.Box("....................................XXXXXXXXXXXXXXXXXXXXX.............\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n.", "box", ExpandWidth, ExpandHeight);
      GUILayout.EndVertical();
    GUILayout.EndHorizontal();
  }
}

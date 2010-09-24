using UnityEngine;
using UnityEditor;

public class GitStatusPanel : GitPanel {
  private GUIContent currentBranchLabel = null;
  private string currentBranch = null;

  private bool isDetachedHeadMode = false;

  public void Refresh() {
    currentBranch = GitWrapper.CurrentBranch;
    if(currentBranch == "") {
      currentBranchLabel = new GUIContent("Detached HEAD Mode");
      isDetachedHeadMode = true;
    } else {
      currentBranchLabel = new GUIContent(currentBranch);
      isDetachedHeadMode = false;
    }
  }

  public override void OnEnable() {
    Refresh();
  }

  public override void OnGUI() {
    GUILayout.BeginHorizontal();
      GUILayout.Label("Current Branch:", GitStyles.BoldLabel, NoExpandWidth);
      GUILayout.Space(SizeOfSpace(GitStyles.BoldLabel));
      Color c = GUI.color;
      GUI.color = isDetachedHeadMode ? GitStyles.ErrorColor : GitStyles.TextColor;
      GUILayout.Label(currentBranchLabel, GitStyles.WhiteBoldLabel, NoExpandWidth);
      GUI.color = c;
    GUILayout.EndHorizontal();
  }
}

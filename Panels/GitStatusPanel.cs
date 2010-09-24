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
    if(BLANK_TEX == null) {
      BLANK_TEX = new Texture2D(16, 16);
      BLANK_TEX.hideFlags = HideFlags.HideAndDontSave;
      Color[] tmp = new Color[16 * 16];
      for(int i = 0; i < tmp.Length; i++)
        tmp[i] = Color.clear;
      BLANK_TEX.SetPixels(tmp);
      BLANK_TEX.Apply();
    }
  }

  private Vector2 workingScrollPos;
  private const int ICON_WIDTH = 16, ITEM_HEIGHT = 21;
  private static Texture2D BLANK_TEX = null;

  private Color ColorForChangeType(GitWrapper.ChangeType status) {
    Color c = Color.red;
    switch(status) {
      case GitWrapper.ChangeType.Modified:  c = GitStyles.ModifiedColor; break;
      case GitWrapper.ChangeType.Added:     c = GitStyles.AddedColor; break;
      case GitWrapper.ChangeType.Deleted:   c = GitStyles.DeletedColor; break;
      case GitWrapper.ChangeType.Renamed:   c = GitStyles.RenamedColor; break;
      case GitWrapper.ChangeType.Copied:    c = GitStyles.CopiedColor; break;
      case GitWrapper.ChangeType.Untracked: c = GitStyles.UntrackedColor; break;
      default: 
        Debug.Log("Should not have gotten this status: " + status);
        break;
    }
    return c;
  }

  private void ShowFile(string path, GitWrapper.ChangeType status) {
    GUILayout.BeginHorizontal();
      GUIContent tmp = new GUIContent() {
        image = AssetDatabase.GetCachedIcon(path) ?? BLANK_TEX,
        text = null
      };
      GUILayout.Label(tmp, GUILayout.Width(ICON_WIDTH), GUILayout.Height(ITEM_HEIGHT));
      Color c = GUI.color;
      GUI.color = ColorForChangeType(status);
      GUILayout.BeginVertical(GUILayout.MaxHeight(ITEM_HEIGHT));
        GUILayout.FlexibleSpace();
        GUILayout.Label(path, GitStyles.WhiteLargeLabel);
      GUILayout.EndVertical();
      GUI.color = c;
    GUILayout.EndHorizontal();
  }

  private void ShowUnstagedChanges() {
    GUILayout.Label("Unstaged Changes:", GitStyles.BoldLabel, NoExpandWidth);
    workingScrollPos = EditorGUILayout.BeginScrollView(workingScrollPos, GitStyles.FileListBox);
      if(changes != null) {
        for(int i = 0; i < changes.Length; i++) {
          if(changes[i].workingStatus != GitWrapper.ChangeType.Unmodified) {
            ShowFile(changes[i].path, changes[i].workingStatus);
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
            GUI.color = ColorForChangeType(changes[i].indexStatus);
            GUILayout.Label(changes[i].path, GitStyles.WhiteLargeLabel);
            GUI.color = c;
          }
        }
      }
    EditorGUILayout.EndScrollView();
  }

  public override void OnToolbarGUI() {
    if(GUILayout.Button("Refresh", EditorStyles.toolbarButton, NoExpandWidth))
      Refresh();
  }

  public override void OnGUI() {
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
        GUILayout.Box("Lorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\n", "box", ExpandWidth, ExpandHeight);
      GUILayout.EndVertical();
    GUILayout.EndHorizontal();
  }
}

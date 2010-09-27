using UnityEngine;
using UnityEditor;
using System.Collections;

public class GitStatusPanel : GitPanel {
  // Messages...
  private static GUIContent UNSTAGED_CHANGES_LABEL = new GUIContent("Unstaged Changes:"),
                            STAGED_CHANGES_LABEL = new GUIContent("Staged Changes:"),
                            TMP_DUMMY_DIFF = new GUIContent("Lorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\nLorem ipsum dolor sit amar blah blah blah blah blah, blah blah blah.\n"),
                            CURRENT_BRANCH_LABEL = new GUIContent("Current Branch:"),
                            REFRESH_BUTTON = new GUIContent("Refresh"),
                            STAGE_CHANGES_BUTTON = new GUIContent("Stage Changed"),
                            SIGN_OFF_BUTTON = new GUIContent("Sign Off"),
                            COMMIT_BUTTON = new GUIContent("Commit"),
                            PUSH_BUTTON = new GUIContent("Push"),
                            AMEND_TOGGLE = new GUIContent("Amend");


  // Static helper data.
  private static GUIContent DUMMY_CONTENT = new GUIContent("X");
  private static GUILayoutOption ICON_WIDTH = GUILayout.Width(16), 
                                 ITEM_HEIGHT = GUILayout.Height(21), 
                                 MAX_ITEM_HEIGHT = GUILayout.MaxHeight(21);
  private static Texture2D BLANK_TEX = null;


  // Overarching state for the panel.
  private GUIContent currentBranchLabel = new GUIContent("");
  private string currentBranch = null;
  private bool isDetachedHeadMode = false;
  private GitWrapper.Change[] changes = null;


  // Event handlers.
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

  protected void Init() {
    // Set up data we can't set up until OnGUI.
    if(editorLineHeight <= 0) {
      editorLineHeight = GUI.skin.GetStyle("textarea").CalcHeight(DUMMY_CONTENT, 100);
      boldLabelSpaceSize = SizeOfSpace(GitStyles.BoldLabel);
    }
  }


  // Operations.
  public void Refresh() {
    currentBranch = GitWrapper.CurrentBranch;
    // TODO: Refactor detection of detached-head state into GitWrapper.
    if(currentBranch == "") {
      currentBranchLabel.text = "Detached HEAD Mode";
      isDetachedHeadMode = true;
    } else {
      currentBranchLabel.text = currentBranch;
      isDetachedHeadMode = false;
    }

    changes = GitWrapper.Status;
  }

  public void SignOff() {
    // TODO: Refactor signoff operation into GitWrapper.
    string signOffMessage = "Signed-off-by: " + GitWrapper.ConfigGet("user.name") + " <" + GitWrapper.ConfigGet("user.email") + ">";
    if(!commitMessage.EndsWith(signOffMessage))
      commitMessage += "\n" + signOffMessage;
  }


  // Helpers.
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

  public void StagePath(string path) {
    Debug.LogWarning("TODO: Implement me.");
  }

  private Hashtable iconCache = new Hashtable();
  private Hashtable selectionCache = new Hashtable();
  private void ShowFile(string path, GitWrapper.ChangeType status) {
    bool isSelected = selectionCache.ContainsKey(path) ? (bool)selectionCache[path] : false;

    GUIStyle style = isSelected ? GitStyles.FileLabelSelected : GitStyles.FileLabel;
    GUILayout.BeginHorizontal();
      GUIContent tmp = null;
      if(!iconCache.ContainsKey(path)) {
        tmp = new GUIContent() {
          image = AssetDatabase.GetCachedIcon(path) ?? BLANK_TEX,
          text = null
        };
        iconCache[path] = tmp;
      }
      tmp = (GUIContent)iconCache[path];
      if(GUILayout.Button(tmp, style, ICON_WIDTH, ITEM_HEIGHT)) {
        // TODO: Stage this path.
        StagePath(path);
        if(selectionCache.ContainsKey(path))
          selectionCache.Remove(path);
      }
      Color c = GUI.contentColor;
      GUI.contentColor = ColorForChangeType(status);
      Rect r = EditorGUILayout.BeginVertical(style, MAX_ITEM_HEIGHT);
        GUILayout.FlexibleSpace();
        GUILayout.Label(path, style);
        GUILayout.Space(3);
      EditorGUILayout.EndVertical();
      if(r.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown) {
        isSelected = !isSelected;
        selectionCache[path] = isSelected;
        Shell.Repaint();
      }
      GUI.contentColor = c;
    GUILayout.EndHorizontal();
  }


  // Sub-views.
  private Vector2 workingScrollPos;
  private void ShowUnstagedChanges() {
    GUILayout.Label(UNSTAGED_CHANGES_LABEL, GitStyles.BoldLabel, NoExpandWidth);
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
    GUILayout.Label(STAGED_CHANGES_LABEL, GitStyles.BoldLabel, NoExpandWidth);
    indexScrollPos = EditorGUILayout.BeginScrollView(indexScrollPos, GitStyles.FileListBox);
      if(changes != null) {
        for(int i = 0; i < changes.Length; i++) {
          if(changes[i].indexStatus != GitWrapper.ChangeType.Unmodified && changes[i].indexStatus != GitWrapper.ChangeType.Untracked) {
            ShowFile(changes[i].path, changes[i].indexStatus);
          }
        }
      }
    EditorGUILayout.EndScrollView();
  }

  private float editorLineHeight = -1, boldLabelSpaceSize = -1;
  private string commitMessage = "";
  private bool amendCommit = false;
  public void ShowCommitMessageEditor() {
    // TODO: Make this scrollable, and make it obey editor commands properly.
    // TODO: Clear selection cache as appropriate.
    commitMessage = GUILayout.TextArea(commitMessage, GUILayout.Height(editorLineHeight * 9 + 2));
    GUILayout.BeginHorizontal();
      if(GUILayout.Button(REFRESH_BUTTON, GitStyles.CommandLeft)) {
        Refresh();
      }
      GUILayout.Button(STAGE_CHANGES_BUTTON, GitStyles.CommandMid);
      if(GUILayout.Button(SIGN_OFF_BUTTON, GitStyles.CommandMid))
        SignOff();
      GUILayout.Button(COMMIT_BUTTON, GitStyles.CommandMid);
      GUILayout.Button(PUSH_BUTTON, GitStyles.CommandRight);
      GUILayout.Space(10);
      amendCommit = GUILayout.Toggle(amendCommit, AMEND_TOGGLE);
      GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();
  }

  public void ShowDiffView() {
    GUILayout.Box(TMP_DUMMY_DIFF, GitStyles.FileListBox, ExpandWidth, ExpandHeight);
  }

  public override void OnGUI() {
    Init();

    Color c = GUI.color;
    GUILayout.BeginHorizontal();
      GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
          GUILayout.Label(CURRENT_BRANCH_LABEL, GitStyles.BoldLabel, NoExpandWidth);
          GUILayout.Space(boldLabelSpaceSize);
          GUI.color = isDetachedHeadMode ? GitStyles.ErrorColor : GitStyles.TextColor;
          GUILayout.Label(currentBranchLabel, GitStyles.WhiteBoldLabel, NoExpandWidth);
          GUI.color = c;
        GUILayout.EndHorizontal();
        GUILayout.Space(5);

        ShowUnstagedChanges();
        GUILayout.Space(5);

        ShowStagedChanges();
      GUILayout.EndVertical();
      GUILayout.Space(5);
      GUILayout.BeginVertical();
        ShowDiffView();
        ShowCommitMessageEditor();
      GUILayout.EndVertical();
    GUILayout.EndHorizontal();
  }

  // Base constructor
  public GitStatusPanel(GitShell owner) : base(owner) {}
}

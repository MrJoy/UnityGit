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
  private static Texture DEFAULT_FILE_ICON = EditorGUIUtility.ObjectContent(null, typeof(MonoScript)).image;
  private static GUIContent DUMMY_CONTENT = new GUIContent("X");
  private static GUILayoutOption ICON_WIDTH = GUILayout.Width(16), 
                                 ITEM_HEIGHT = GUILayout.Height(21), 
                                 MAX_ITEM_HEIGHT = GUILayout.MaxHeight(21);


  // Overarching state for the panel.
  private GUIContent currentBranchLabel = new GUIContent("");
  private string currentBranch = null;
  private bool isDetachedHeadMode = false;
  private GitWrapper.Change[] changes = null;


  // Event handlers.
  public override void OnEnable() {
    Refresh();
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

  public void StagePath(string path) {
    GitWrapper.StagePath(path);
    RefreshPath(path);
  }

  public void UnstagePath(string path) {
    GitWrapper.UnstagePath(path);
    RefreshPath(path);
  }


  // Helpers.
  public Color ColorForChangeType(GitWrapper.ChangeType status) {
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

  public void RefreshPath(string path) {
    GitWrapper.Change tmp = GitWrapper.StatusForPath(path);
    for(int i = 0; i < changes.Length; i++) {
      if(changes[i].path == tmp.path) {
        changes[i] = tmp;
        break;
      }
    }
  }

  [System.NonSerialized]
  private Hashtable iconCache = new Hashtable();
  protected bool ShowFile(int id, Hashtable selectionCache, string path, GitWrapper.ChangeType status) {
    bool isChanged = false;
    bool isSelected = selectionCache.ContainsKey(path) ? (bool)selectionCache[path] : false;
    // TODO: This is a VERY ugly hack to not "lose" focus when we click a button
    // TODO: in here due to the button stealing the focus from our list view.
    // TODO: Strictly speaking we don't know which widget is being clicked but 
    // TODO: I'm operating under the assumption that that's a moot issue since
    // TODO: the list view 
    bool hasFocus = (GUIUtility.hotControl == id) || (Event.current.button == 0);
    GUIStyle style = isSelected ? (hasFocus ? GitStyles.FileLabelSelected : GitStyles.FileLabelSelectedUnfocused) : GitStyles.FileLabel;

    GUILayout.BeginHorizontal();
      GUIContent tmp = null;
      if(!iconCache.ContainsKey(path)) {
        tmp = new GUIContent() {
          image = AssetDatabase.GetCachedIcon(path),
          text = null
        };
        if(tmp.image == null)
          tmp.image = DEFAULT_FILE_ICON;
        iconCache[path] = tmp;
      }
      tmp = (GUIContent)iconCache[path];
      if(GUILayout.Button(tmp, style, ICON_WIDTH, ITEM_HEIGHT)) {
        isChanged = true;
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
      if(GUI.Button(r, GUIContent.none, GUIStyle.none)) {
        isChanged = true;
        isSelected = !isSelected;
        selectionCache[path] = isSelected;
        GUIUtility.hotControl = id;
      }
      GUI.contentColor = c;
    GUILayout.EndHorizontal();
    return isChanged;
  }

  protected delegate bool FilterDelegate(GitWrapper.Change change);
  protected delegate GitWrapper.ChangeType ChangeTypeDelegate(GitWrapper.Change change);

  protected Vector2 FileListView(GUIContent label, Vector2 scrollPos, FilterDelegate filter, ChangeTypeDelegate changeTypeFetcher, Hashtable selectionCache) {
    GUILayout.Label(label, GitStyles.BoldLabel, NoExpandWidth);
    int id = GUIUtility.GetControlID(FocusType.Passive);
    bool hasFocus = GUIUtility.hotControl == id;
    bool isChanged = false;
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GitStyles.FileListBox);
      if(changes != null) {
        for(int i = 0; i < changes.Length; i++) {
          if(filter(changes[i])) {
            isChanged = isChanged || ShowFile(id, selectionCache, changes[i].path, changeTypeFetcher(changes[i]));
          }
        }
      }
    EditorGUILayout.EndScrollView();
    Rect r = GUILayoutUtility.GetLastRect();
    isChanged = isChanged || ((Event.current.type == EventType.MouseDown) && r.Contains(Event.current.mousePosition));
    if(isChanged && !hasFocus) {
      Debug.Log("Focusing on " + id);
      GUIUtility.hotControl = id;
    }
    return scrollPos;
  }

  protected bool WorkingSetFilter(GitWrapper.Change change) {
    return change.workingStatus != GitWrapper.ChangeType.Unmodified;
  }

  protected bool IndexSetFilter(GitWrapper.Change change) {
    return change.indexStatus != GitWrapper.ChangeType.Unmodified && change.indexStatus != GitWrapper.ChangeType.Untracked;
  }

  protected GitWrapper.ChangeType WorkingSetFetcher(GitWrapper.Change change) {
    return change.workingStatus;
  }

  protected GitWrapper.ChangeType IndexSetFetcher(GitWrapper.Change change) {
    return change.indexStatus;
  }

  // Sub-views.
  private Hashtable workingSetSelectionCache = new Hashtable();  
  private Vector2 workingScrollPos;
  protected void ShowUnstagedChanges() {
    workingScrollPos = FileListView(UNSTAGED_CHANGES_LABEL, workingScrollPos, WorkingSetFilter, WorkingSetFetcher, workingSetSelectionCache);
  }

  private Hashtable indexSetSelectionCache = new Hashtable();
  private Vector2 indexScrollPos;
  protected void ShowStagedChanges() {
    indexScrollPos = FileListView(STAGED_CHANGES_LABEL, indexScrollPos, IndexSetFilter, IndexSetFetcher, indexSetSelectionCache);
  }

  private float editorLineHeight = -1, boldLabelSpaceSize = -1;
  private string commitMessage = "";
  private bool amendCommit = false;
  protected void ShowCommitMessageEditor() {
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

  protected void ShowDiffView() {
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

using UnityEditor;
using UnityEngine;

public abstract class GitPanel {
  public GitPanel(GitShell owner) { _Shell = owner; }


  // Principal interface...
  public virtual bool IsDisabledForError { get { return !GitWrapper.IsUsable; } }
  public virtual void OnEnable() {}
  public virtual void OnDisable() {}
  public virtual void OnToolbarGUI() {}
  public virtual void OnRefresh() {}
  public virtual void OnFocus() { }
  public virtual void OnLostFocus() { }
  public abstract void OnGUI();


  // Internal tools...
  private static GUIContent DUMMY_WITH_SPACE = new GUIContent(" .");
  private static GUIContent DUMMY_WITHOUT_SPACE = new GUIContent(".");


  // Tools for panels...
  private GitShell _Shell = null;
  protected virtual GitShell Shell { get { return _Shell; } }

  protected const int SPACE_SIZE = 5;
  protected static void Space() {
    GUILayout.Space(SPACE_SIZE);
  }

  protected static void LinkTo(GUIContent label, string url) {
    // TODO: Find a way to make this underlined and have the expected cursor.
    Color c = GUI.contentColor;
    GUI.contentColor = GitStyles.LinkColor;
    if(GUILayout.Button(label, GitStyles.Link)) {
      Application.OpenURL(url);
    }
    GUI.contentColor = c;
  }

  protected static float SizeOfSpace(GUIStyle style) {
    // TODO: Make this less expensive than I believe it to be.
    float x1 = style.CalcSize(DUMMY_WITH_SPACE).x;
    float x2 = style.CalcSize(DUMMY_WITHOUT_SPACE).x;
    return x1 - x2;
  }
}

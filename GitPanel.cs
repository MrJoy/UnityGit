using UnityEngine;
using UnityEditor;

public abstract class GitPanel {
  public virtual bool IsDisabledForError { get { return !GitWrapper.IsWorking; } }

  public virtual void OnEnable() {}
  public virtual void OnDisable() {}
  public abstract void OnGUI();

  public static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                NoExpandWidth = GUILayout.ExpandWidth(false),
                                ExpandHeight = GUILayout.ExpandHeight(true),
                                NoExpandHeight = GUILayout.ExpandHeight(false);

  public static void LinkTo(GUIContent label, string url) {
    // TODO: Find a way to make this underlined and have the expected cursor.
    if(GUILayout.Button(label, GitStyles.Link)) {
      Application.OpenURL(url);
    }
  }

  private static GUIContent DUMMY_WITH_SPACE = new GUIContent(" .");
  private static GUIContent DUMMY_WITHOUT_SPACE = new GUIContent(".");

  public static float SizeOfSpace(GUIStyle style) {
    float x1 = style.CalcSize(DUMMY_WITH_SPACE).x;
    float x2 = style.CalcSize(DUMMY_WITHOUT_SPACE).x;
    return x1 - x2;
  }
}

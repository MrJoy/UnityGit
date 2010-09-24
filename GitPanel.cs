using UnityEngine;
using UnityEditor;

public abstract class GitPanel {
  public virtual void OnEnable() {}
  public virtual void OnDisable() {}
  public abstract void OnGUI();

  public static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                NoExpandWidth = GUILayout.ExpandWidth(false),
                                ExpandHeight = GUILayout.ExpandHeight(true),
                                NoExpandHeight = GUILayout.ExpandHeight(false);
}

using UnityEngine;
using UnityEditor;

public abstract class GitPanel {
  public void OnEnable() {}
  public void OnDisable() {}
  public abstract void OnGUI();

  protected static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                   NoExpandWidth = GUILayout.ExpandWidth(false),
                                   ExpandHeight = GUILayout.ExpandHeight(true),
                                   NoExpandHeight = GUILayout.ExpandHeight(false);
}

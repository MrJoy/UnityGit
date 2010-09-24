using UnityEngine;
using UnityEditor;

public static class GitStyles {
  private static GUIStyle _BoldLabel = null;
  public static GUIStyle BoldLabel {
    get {
      if(_BoldLabel == null) {
        _BoldLabel = new GUIStyle(EditorStyles.boldLabel) {
          padding = new RectOffset(0,0,0,0),
          margin = new RectOffset(0,0,0,0)
        };
      }
      return _BoldLabel;
    }
  }
  private static GUIStyle _MiniLabel = null;
  public static GUIStyle MiniLabel {
    get {
      if(_MiniLabel == null) {
        _MiniLabel = new GUIStyle(EditorStyles.miniLabel) {
          padding = new RectOffset(0,0,0,0),
          margin = new RectOffset(0,0,0,0)
        };
      }
      return _MiniLabel;
    }
  }
}

using UnityEngine;
using UnityEditor;

public static class GitStyles {
  private static RectOffset ZeroOffset = new RectOffset(0,0,0,0);

  private static GUIStyle _Indented = null;
  public static GUIStyle Indented {
    get {
      if(_Indented == null) {
        _Indented = new GUIStyle() {
          padding = new RectOffset(4,4,4,4),
          margin = ZeroOffset
        };
      }
      return _Indented;
    }
  }

  private static GUIStyle _BoldLabel = null;
  public static GUIStyle BoldLabel {
    get {
      if(_BoldLabel == null) {
        _BoldLabel = new GUIStyle(EditorStyles.boldLabel) {
          padding = ZeroOffset,
          margin = ZeroOffset
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
          padding = ZeroOffset,
          margin = ZeroOffset
        };
      }
      return _MiniLabel;
    }
  }

  private static GUIStyle _WhiteLabel = null;
  public static GUIStyle WhiteLabel {
    get {
      if(_WhiteLabel == null) {
        _WhiteLabel = new GUIStyle(EditorStyles.whiteLabel) {
          padding = ZeroOffset,
          margin = ZeroOffset
        };
      }
      return _WhiteLabel;
    }
  }
}

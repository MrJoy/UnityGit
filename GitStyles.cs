using UnityEngine;
using UnityEditor;

public static class GitStyles {
  private static RectOffset ZeroOffset = new RectOffset(0,0,0,0);

  public static Color TextColor = new Color(0f, 0f, 0f, 1f);
  public static Color ErrorColor = new Color(0.5f, 0f, 0f, 1f);
  public static Color LinkColor = new Color(0f, 0f, 1f, 1f);

  // Change status colors:
  public static Color ModifiedColor = new Color(0.35f, 0.35f, 0f, 1f);
  public static Color DeletedColor = new Color(0.5f, 0f, 0f, 1f);
  public static Color UntrackedColor = new Color(0f, 0f, 0.5f, 1f);
  public static Color AddedColor = new Color(0f, 0.5f, 0f, 1f);
  public static Color RenamedColor = new Color(0f, 0.5f, 0.5f, 1f);
  public static Color CopiedColor = new Color(0.5f, 0f, 0.5f, 1f);

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

  private static GUIStyle _FileListBox = null;
  public static GUIStyle FileListBox {
    get {
      if(_FileListBox == null) {
        _FileListBox = new GUIStyle("GroupBox") {
          padding = new RectOffset(4,4,0,4),
          margin = ZeroOffset
        };
      }
      return _FileListBox;
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

  private static GUIStyle _WhiteBoldLabel = null;
  public static GUIStyle WhiteBoldLabel {
    get {
      if(_WhiteBoldLabel == null) {
        _WhiteBoldLabel = new GUIStyle(EditorStyles.whiteBoldLabel) {
          padding = ZeroOffset,
          margin = ZeroOffset
        };
      }
      return _WhiteBoldLabel;
    }
  }

  private static GUIStyle _WhiteLargeLabel = null;
  public static GUIStyle WhiteLargeLabel {
    get {
      if(_WhiteLargeLabel == null) {
        _WhiteLargeLabel = new GUIStyle(EditorStyles.whiteLargeLabel) {
          padding = ZeroOffset,
          margin = ZeroOffset
        };
      }
      return _WhiteLargeLabel;
    }
  }
  private static GUIStyle _Link = null;
  public static GUIStyle Link {
    get {
      if(_Link == null) {
        _Link = new GUIStyle(EditorStyles.whiteLabel) {
          padding = ZeroOffset,
          margin = ZeroOffset,
          normal = new GUIStyleState() {
            textColor = GitStyles.LinkColor
          }
        };
      }
      return _Link;
    }
  }
}

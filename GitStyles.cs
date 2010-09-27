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
          padding = new RectOffset(1,1,1,1),
          margin = ZeroOffset,
          alignment = TextAnchor.UpperLeft,
          imagePosition = ImagePosition.TextOnly
        };
      }
      return _FileListBox;
    }
  }

  private static GUIStyle _FileLabel = null;
  public static GUIStyle FileLabel {
    get {
      if(_FileLabel == null) {
        _FileLabel = new GUIStyle(WhiteLargeLabel);
      }
      return _FileLabel;
    }
  }

  private static GUIStyle _FileLabelSelected = null;
  public static GUIStyle FileLabelSelected {
    get {
      if(_FileLabelSelected == null) {
        _FileLabelSelected = new GUIStyle(FileLabel) {
          normal = new GUIStyleState() {
            background = GUI.skin.GetStyle("PR Label").onHover.background,
            textColor = Color.white
          }
        };
      }
      return _FileLabelSelected;
    }
  }

  private static GUIStyle _FileLabelSelectedUnfocused = null;
  public static GUIStyle FileLabelSelectedUnfocused {
    get {
      if(_FileLabelSelectedUnfocused == null) {
        _FileLabelSelectedUnfocused = new GUIStyle(FileLabelSelected) {
          normal = new GUIStyleState() {
            background = GUI.skin.GetStyle("PR Label").onNormal.background,
            textColor = Color.white
          }
        };
      }
      return _FileLabelSelectedUnfocused;
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

  private static GUIStyle _CommandLeft = null;
  public static GUIStyle CommandLeft {
    get {
      if(_CommandLeft == null) {
        _CommandLeft = new GUIStyle(GUI.skin.GetStyle("CommandLeft")) {
          fixedWidth = 0,
          padding = new RectOffset(4,3,0,0)
        };
      }
      return _CommandLeft;
    }
  }

  private static GUIStyle _CommandMid = null;
  public static GUIStyle CommandMid {
    get {
      if(_CommandMid == null) {
        _CommandMid = new GUIStyle(GUI.skin.GetStyle("CommandMid")) {
          fixedWidth = 0
        };
      }
      return _CommandMid;
    }
  }

  private static GUIStyle _CommandRight = null;
  public static GUIStyle CommandRight {
    get {
      if(_CommandRight == null) {
        _CommandRight = new GUIStyle(GUI.skin.GetStyle("CommandRight")) {
          fixedWidth = 0,
          padding = new RectOffset(2,5,0,0)
        };
      }
      return _CommandRight;
    }
  }
}

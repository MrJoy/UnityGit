using UnityEditor;
using UnityEngine;

namespace UnityGit.DiffView {
  public static class Styles {
    private static RectOffset ZeroOffset = new RectOffset(0,0,0,0);
    // TODO: Suss out some sort of monospaced font here.  >.<
    // TODO: Show trailing whitespace a la "git gui"...

    /* Template for styles:
    private static GUIStyle _@FOO@Pro = null, _@FOO@Normal = null;
    public static GUIStyle @FOO@ {
      get {
        if(_@FOO@Normal == null) {
          _@FOO@Normal = new GUIStyle("@BAR@") {
            //normal = new GUIStyleState() {
            //  background = EditorGUIUtility.whiteTexture,
            //  textColor = Color.black
            //},
            //padding = new RectOffset(1,1,1,1),
            //alignment = TextAnchor.UpperLeft,
            //imagePosition = ImagePosition.TextOnly,
            //stretchWidth = true,
            //stretchHeight = true
          };
          _@FOO@Pro = new GUIStyle(_@FOO@Normal) {
            //normal = new GUIStyleState() {
            //  background = null,
            //  textColor = Color.white
            //}
          };
        }
        return IsProSkin ? _@FOO@Pro : _@FOO@Normal;
      }
    }
    */

    private static GUIStyle _Padding = null;
    public static GUIStyle Padding {
      get {
        if(_Padding == null) {
          _Padding = new GUIStyle() {
            margin = new RectOffset(2,2,2,2),
            padding = ZeroOffset,
            stretchWidth = true,
            stretchHeight = true
          };
        }
        return _Padding;
      }
    }

    private static GUIStyle _HeaderPro = null, _HeaderNormal = null;
    public static GUIStyle Header {
      get {
        if(_HeaderNormal == null) {
          _HeaderNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.blue
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft,
            font = EditorStyles.largeLabel.font,
            fontStyle = FontStyle.Bold
          };
          _HeaderPro = new GUIStyle(_HeaderNormal) {
            normal = new GUIStyleState() {
              textColor = Color.cyan
            }
          };
        }
        return IsProSkin ? _HeaderPro : _HeaderNormal;
      }
    }

    private static GUIStyle _MarkerPositionPro = null, _MarkerPositionNormal = null;
    public static GUIStyle MarkerPosition {
      get {
        if(_MarkerPositionNormal == null) {
          _MarkerPositionNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.magenta * 0.5f + Color.black * 0.5f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft,
            font = EditorStyles.largeLabel.font,
            fontStyle = FontStyle.Bold
          };
          _MarkerPositionPro = new GUIStyle(_MarkerPositionNormal) {
            normal = new GUIStyleState() {
              textColor = Color.magenta * 0.75f + Color.black * 0.25f
            }
          };
        }
        return IsProSkin ? _MarkerPositionPro : _MarkerPositionNormal;
      }
    }

    private static GUIStyle _MarkerContextPro = null, _MarkerContextNormal = null;
    public static GUIStyle MarkerContext {
      get {
        if(_MarkerContextNormal == null) {
          _MarkerContextNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.magenta * 0.75f + Color.black * 0.25f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft,
            font = EditorStyles.largeLabel.font,
            fontStyle = FontStyle.Bold
          };
          _MarkerContextPro = new GUIStyle(_MarkerContextNormal) {
            normal = new GUIStyleState() {
              textColor = Color.magenta
            }
          };
        }
        return IsProSkin ? _MarkerContextPro : _MarkerContextNormal;
      }
    }

    private static GUIStyle _LeftFilePro = null, _LeftFileNormal = null;
    public static GUIStyle LeftFile {
      get {
        if(_LeftFileNormal == null) {
          _LeftFileNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.red * 0.5f + Color.black * 0.5f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft,
            font = EditorStyles.largeLabel.font,
            fontStyle = FontStyle.Bold
          };
          _LeftFilePro = new GUIStyle(_LeftFileNormal) {
            normal = new GUIStyleState() {
              textColor = Color.red * 0.75f + Color.black * 0.25f
            }
          };
        }
        return IsProSkin ? _LeftFilePro : _LeftFileNormal;
      }
    }

    private static GUIStyle _RightFilePro = null, _RightFileNormal = null;
    public static GUIStyle RightFile {
      get {
        if(_RightFileNormal == null) {
          _RightFileNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.green * 0.5f + Color.black * 0.5f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft,
            font = EditorStyles.largeLabel.font,
            fontStyle = FontStyle.Bold
          };
          _RightFilePro = new GUIStyle(_RightFileNormal) {
            normal = new GUIStyleState() {
              textColor = Color.green * 0.75f + Color.black * 0.25f
            }
          };
        }
        return IsProSkin ? _RightFilePro : _RightFileNormal;
      }
    }

    private static GUIStyle _NormalPro = null, _NormalNormal = null;
    public static GUIStyle Normal {
      get {
        if(_NormalNormal == null) {
          _NormalNormal = new GUIStyle("Label") {
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft
          };
          _NormalPro = new GUIStyle(_NormalNormal) {
          };
        }
        return IsProSkin ? _NormalPro : _NormalNormal;
      }
    }

    private static GUIStyle _AdditionPro = null, _AdditionNormal = null;
    public static GUIStyle Addition {
      get {
        if(_AdditionNormal == null) {
          _AdditionNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.green * 0.5f + Color.black * 0.5f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft
          };
          _AdditionPro = new GUIStyle(_AdditionNormal) {
            normal = new GUIStyleState() {
              textColor = Color.green * 0.75f + Color.black * 0.25f
            }
          };
        }
        return IsProSkin ? _AdditionPro : _AdditionNormal;
      }
    }

    private static GUIStyle _RemovalPro = null, _RemovalNormal = null;
    public static GUIStyle Removal {
      get {
        if(_RemovalNormal == null) {
          _RemovalNormal = new GUIStyle("Label") {
            normal = new GUIStyleState() {
              textColor = Color.red * 0.5f + Color.black * 0.5f
            },
            margin = ZeroOffset,
            padding = ZeroOffset,
            stretchWidth = false,
            alignment = TextAnchor.MiddleLeft
          };
          _RemovalPro = new GUIStyle(_RemovalNormal) {
            normal = new GUIStyleState() {
              textColor = Color.red * 0.75f + Color.black * 0.25f
            }
          };
        }
        return IsProSkin ? _RemovalPro : _RemovalNormal;
      }
    }

    public static bool IsProSkin {
      get {
        return EditorStyles.whiteLargeLabel.normal.textColor == Color.black;
      }
    }

    private static string MARKER_STRING = ".";
    private static GUIContent MARKER_CONTENT = new GUIContent(MARKER_STRING);

    public static float TrueWidth(GUIStyle s, string content) {
      // NOTE: This is an ugly hack because Unity is very aggressive about 
      // NOTE: clipping trailing space from a label.  BAH!
      GUIContent tmp = new GUIContent(content + MARKER_STRING);
      Vector2 markerSize = s.CalcSize(MARKER_CONTENT);
      Vector2 markedContentSize = s.CalcSize(tmp);
      return markedContentSize.x - markerSize.x;
    }
  }
}

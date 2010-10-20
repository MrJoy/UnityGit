using UnityEngine;
using UnityEditor;

namespace UnityGit.DiffView {
  public static class Styles {
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
    public static bool IsProSkin {
      get {
        return EditorStyles.whiteLargeLabel.normal.textColor == Color.black;
      }
    }
  }
}
using UnityEditor;
using UnityEngine;
using UnityGit.DiffView.State;


namespace UnityGit.DiffView {
  public static class DiffGUI {
    private static GUILayoutOption NoExpandWidth = GUILayout.ExpandWidth(false);

    public static void WordDiff(string content, bool showFileNames) {
      int id = GUIUtility.GetControlID(FocusType.Passive);
      WordDiffState state = (WordDiffState)GUIUtility.GetStateObject(typeof(WordDiffState), id);
      state.content = content;
      state.showFileNames = showFileNames;

      Line[] lines = state.lines;

      Color oldColor = GUI.contentColor;
      GUILayout.BeginVertical();
        foreach(Line line in lines) {
          GUILayout.BeginHorizontal();
            for(int i = 0; i < line.segments.Length; i++) {
              GUILayout.Label(line.segments[i], line.styles[i], GUILayout.Width(line.widths[i]), NoExpandWidth);
            }
          GUILayout.EndHorizontal();
          GUI.contentColor = oldColor;
        }
      GUILayout.EndVertical();
    }

    public static Vector2 ScrollableWordDiff(Vector2 scrollPosition, string content, bool showFileNames) {
      scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.BeginVertical(Styles.Padding);
          WordDiff(content, showFileNames);
        GUILayout.EndVertical();
      EditorGUILayout.EndScrollView();

      return scrollPosition;
    }
  }
}
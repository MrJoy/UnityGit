using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UnityGit.DiffView.State;

public class GitDiffTestPanel : GitPanel {
  private static string TEST_DIFF = 
    "diff --git a/GitPanel.cs b/GitPanel.cs\n" +
    "index d408a19..8e9863a 100644\n" +
    "--- a/GitPanel.cs\n" +
    "+++ b/GitPanel.cs\n" +
    "@@ -38,9 +38,12 @@ public abstract class GitPanel {\n" +
    " \n" +
    "~\n" +
    "   protected static void LinkTo(GUIContent label, string url) {\n" +
    "~\n" +
    "     // TODO: Find a way to make this underlined and have the expected cursor.\n" +
    "~\n" +
    "     \n" +
    "+Color c = GUI.contentColor;\n" +
    "~\n" +
    "+    GUI.contentColor = GitStyles.LinkColor;\n" +
    "~\n" +
    "     if(GUILayout.Button(label, GitStyles.Link)) {\n" +
    "~\n" +
    "       Application.OpenURL(url);\n" +
    "~\n" +
    "     }\n" +
    "~\n" +
    "     \n" +
    "+GUI.contentColor = c;\n" +
    "~\n" +
    "   }\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   protected static float SizeOfSpace(GUIStyle style) {\n" +
    "~\n" +
    "diff --git a/GitStyles.cs b/GitStyles.cs\n" +
    "index dd267c4..9f317ac 100644\n" +
    "--- a/GitStyles.cs\n" +
    "+++ b/GitStyles.cs\n" +
    "@@ -4,21 +4,57 @@ using UnityEditor;\n" +
    " public static class GitStyles {\n" +
    "~\n" +
    "   private static RectOffset ZeroOffset = new RectOffset(0,0,0,0);\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   public static \n" +
    "+class NormalSkinColors {\n" +
    "~\n" +
    "+    public static\n" +
    "  Color TextColor = new Color(0f, 0f, 0f, 1f);\n" +
    "~\n" +
    "     public static Color ErrorColor = new Color(0.5f, 0f, 0f, 1f);\n" +
    "~\n" +
    "     public static Color LinkColor = new Color(0f, 0f, 1f, 1f);\n" +
    "~\n" +
    "~\n" +
    "     \n" +
    "+// Change status colors:\n" +
    "~\n" +
    "+    public static Color ModifiedColor = new Color(0.35f, 0.35f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color DeletedColor = new Color(0.5f, 0f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color UntrackedColor = new Color(0f, 0f, 0.5f, 1f);\n" +
    "~\n" +
    "+    public static Color AddedColor = new Color(0f, 0.5f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color RenamedColor = new Color(0f, 0.5f, 0.5f, 1f);\n" +
    "~\n" +
    "+    public static Color CopiedColor = new Color(0.5f, 0f, 0.5f, 1f);\n" +
    "~\n" +
    "~\n" +
    "+    // Ref colors:\n" +
    "~\n" +
    "+    public static Color CurrentBranchColor = new Color(0f, 0.5f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color BranchColor = new Color(0f, 0f, 0f, 1f);\n" +
    "~\n" +
    "+  }\n" +
    "~\n" +
    "~\n" +
    "+  public static class ProSkinColors {\n" +
    "~\n" +
    "+    public static Color TextColor = new Color(0.75f, 0.75f, 0.75f, 1f);\n" +
    "~\n" +
    "+    public static Color ErrorColor = new Color(1f, 0f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color LinkColor = new Color(0.2f, 0.2f, 1f, 1f);\n" +
    "~\n" +
    "~\n" +
    "+    // Change status colors:\n" +
    "~\n" +
    "+    public static Color ModifiedColor = new Color(0.75f, 0.75f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color DeletedColor = new Color(0.75f, 0f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color UntrackedColor = new Color(0.25f, 0.25f, 1f, 1f);\n" +
    "~\n" +
    "+    public static Color AddedColor = new Color(0f, 0.75f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color RenamedColor = new Color(0f, 0.75f, 0.75f, 1f);\n" +
    "~\n" +
    "+    public static Color CopiedColor = new Color(0.75f, 0f, 0.75f, 1f);\n" +
    "~\n" +
    "~\n" +
    "+    // Ref colors:\n" +
    "~\n" +
    "+    public static Color CurrentBranchColor = new Color(0f, 0.75f, 0f, 1f);\n" +
    "~\n" +
    "+    public static Color BranchColor = new Color(0.75f, 0.75f, 0.75f, 1f);\n" +
    "~\n" +
    "+  }\n" +
    "~\n" +
    "~\n" +
    "+  public static Color TextColor { get { return IsProSkin ? ProSkinColors.TextColor : NormalSkinColors.TextColor; } }\n" +
    "~\n" +
    "+  public static Color ErrorColor { get { return IsProSkin ? ProSkinColors.ErrorColor : NormalSkinColors.ErrorColor; } }\n" +
    "~\n" +
    "+  public static Color LinkColor { get { return IsProSkin ? ProSkinColors.LinkColor : NormalSkinColors.LinkColor; } }\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   // Change status colors:\n" +
    "~\n" +
    "   public static Color ModifiedColor \n" +
    "-= new Color(0.35f, 0.35f, 0f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.ModifiedColor : NormalSkinColors.ModifiedColor; } }\n" +
    "~\n" +
    "   public static Color DeletedColor \n" +
    "-= new Color(0.5f, 0f, 0f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.DeletedColor : NormalSkinColors.DeletedColor; } }\n" +
    "~\n" +
    "   public static Color UntrackedColor \n" +
    "-= new Color(0f, 0f, 0.5f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.UntrackedColor : NormalSkinColors.UntrackedColor; } }\n" +
    "~\n" +
    "   public static Color AddedColor \n" +
    "-= new Color(0f, 0.5f, 0f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.AddedColor : NormalSkinColors.AddedColor; } }\n" +
    "~\n" +
    "   public static Color RenamedColor \n" +
    "-= new Color(0f, 0.5f, 0.5f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.RenamedColor : NormalSkinColors.RenamedColor; } }\n" +
    "~\n" +
    "   public static Color CopiedColor \n" +
    "-= new Color(0.5f, 0f, 0.5f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.CopiedColor : NormalSkinColors.CopiedColor; } }\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   // Ref colors:\n" +
    "~\n" +
    "   public static Color CurrentBranchColor \n" +
    "-= new Color(0f, 0.5f, 0f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.CurrentBranchColor : NormalSkinColors.CurrentBranchColor; } }\n" +
    "~\n" +
    "   public static Color BranchColor \n" +
    "-= new Color(0f, 0f, 0f, 1f);\n" +
    "+{ get { return IsProSkin ? ProSkinColors.BranchColor : NormalSkinColors.BranchColor; } }\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   private static GUIStyle _Indented = null;\n" +
    "~\n" +
    "   public static GUIStyle Indented {\n" +
    "~\n" +
    "@@ -50,11 +86,22 @@ public static class GitStyles {\n" +
    "     }\n" +
    "~\n" +
    "   }\n" +
    "~\n" +
    " \n" +
    "~\n" +
    "   \n" +
    "+public static bool IsProSkin {\n" +
    "~\n" +
    "+    get {\n" +
    "~\n" +
    "+      return EditorStyles.whiteLargeLabel.normal.textColor == Color.black;\n" +
    "~\n" +
    "+    }\n" +
    "~\n" +
    "+  }\n" +
    "~\n" +
    "~\n" +
    "   private static GUIStyle _FileLabel = null;\n" +
    "~\n" +
    "   public static GUIStyle FileLabel {\n" +
    "~\n" +
    "     get {\n" +
    "~\n" +
    "       if(_FileLabel == null) {\n" +
    "~\n" +
    "         _FileLabel = new \n" +
    "-GUIStyle(WhiteLargeLabel);\n" +
    "+GUIStyle(WhiteLargeLabel) {\n" +
    "~\n" +
    "+          normal = new GUIStyleState() {\n" +
    "~\n" +
    "+            background = null,\n" +
    "~\n" +
    "+            textColor = Color.white\n" +
    "~\n" +
    "+          }\n" +
    "~\n" +
    "+        };\n" +
    "~\n" +
    "       }\n" +
    "~\n" +
    "       return _FileLabel;\n" +
    "~\n" +
    "     }\n" +
    "~\n" +
    "@@ -120,6 +167,10 @@ public static class GitStyles {\n" +
    "     get {\n" +
    "~\n" +
    "       if(_WhiteLabel == null) {\n" +
    "~\n" +
    "         _WhiteLabel = new GUIStyle(EditorStyles.whiteLabel) {\n" +
    "~\n" +
    "           \n" +
    "+normal = new GUIStyleState() {\n" +
    "~\n" +
    "+            background = null,\n" +
    "~\n" +
    "+            textColor = Color.white\n" +
    "~\n" +
    "+          },\n" +
    "~\n" +
    "           padding = ZeroOffset,\n" +
    "~\n" +
    "           margin = ZeroOffset\n" +
    "~\n" +
    "         };\n" +
    "~\n" +
    "@@ -133,6 +184,10 @@ public static class GitStyles {\n" +
    "     get {\n" +
    "~\n" +
    "       if(_WhiteBoldLabel == null) {\n" +
    "~\n" +
    "         _WhiteBoldLabel = new GUIStyle(EditorStyles.whiteBoldLabel) {\n" +
    "~\n" +
    "           \n" +
    "+normal = new GUIStyleState() {\n" +
    "~\n" +
    "+            background = null,\n" +
    "~\n" +
    "+            textColor = Color.white\n" +
    "~\n" +
    "+          },\n" +
    "~\n" +
    "           padding = ZeroOffset,\n" +
    "~\n" +
    "           margin = ZeroOffset\n" +
    "~\n" +
    "         };\n" +
    "~\n" +
    "@@ -146,6 +201,10 @@ public static class GitStyles {\n" +
    "     get {\n" +
    "~\n" +
    "       if(_WhiteLargeLabel == null) {\n" +
    "~\n" +
    "         _WhiteLargeLabel = new GUIStyle(EditorStyles.whiteLargeLabel) {\n" +
    "~\n" +
    "           \n" +
    "+normal = new GUIStyleState() {\n" +
    "~\n" +
    "+            background = null,\n" +
    "~\n" +
    "+            textColor = Color.white\n" +
    "~\n" +
    "+          },\n" +
    "~\n" +
    "           padding = ZeroOffset,\n" +
    "~\n" +
    "           margin = ZeroOffset\n" +
    "~\n" +
    "         };\n" +
    "~\n" +
    "@@ -162,7 +221,7 @@ public static class GitStyles {\n" +
    "           padding = ZeroOffset,\n" +
    "~\n" +
    "           margin = ZeroOffset,\n" +
    "~\n" +
    "           normal = new GUIStyleState() {\n" +
    "~\n" +
    "             textColor = \n" +
    "-GitStyles.LinkColor\n" +
    "+Color.white\n" +
    "~\n" +
    "           }\n" +
    "~\n" +
    "         };\n" +
    "~\n" +
    "       }\n" +
    "~\n";

  protected static void WordDiffDisplay(string content) {
    int id = GUIUtility.GetControlID(FocusType.Passive);
    WordDiffState state = (WordDiffState)GUIUtility.GetStateObject(typeof(WordDiffState), id);
    state.content = content;

    Line[] lines = state.lines;

    Color oldColor = GUI.contentColor;
    GUILayout.BeginVertical();
      foreach(Line line in lines) {
        GUILayout.BeginHorizontal();
          for(int i = 0; i < line.segments.Length; i++) {
            GUI.contentColor = line.colors[i];
            GUILayout.Label(line.segments[i], GitStyles.WhiteLabel, NoExpandWidth);
          }
        GUILayout.EndHorizontal();
        GUI.contentColor = oldColor;
      }
    GUILayout.EndVertical();
//    GUILayout.Box(NoContent, GitStyles.FileListBox, ExpandWidth, ExpandHeight);
  }

  private Vector2 scrollPosition;
  public override void OnGUI() {
    scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GitStyles.FileListBox);
      WordDiffDisplay(TEST_DIFF);
    EditorGUILayout.EndScrollView();
  }

  // Base constructor
  public GitDiffTestPanel(GitShell owner) : base(owner) {}
}

using UnityEngine;
using UnityEditor;

public abstract class GitPanel {
  public virtual bool IsDisabledForError { get { return !GitWrapper.IsWorking; } }

  public GitPanel(GitShell owner) { _Shell = owner; }

  private GitShell _Shell = null;
  protected virtual GitShell Shell { get { return _Shell; } }

  public virtual void OnEnable() {}
  public virtual void OnDisable() {}
  public virtual void OnToolbarGUI() {}
  public abstract void OnGUI();

  protected static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                   NoExpandWidth = GUILayout.ExpandWidth(false),
                                   ExpandHeight = GUILayout.ExpandHeight(true),
                                   NoExpandHeight = GUILayout.ExpandHeight(false);

  protected static GUIContent NoContent = GUIContent.none;
  protected static GUIStyle NoStyle = GUIStyle.none;

  protected static void Space() {
    GUILayout.Space(5);
  }

  protected static void LinkTo(GUIContent label, string url) {
    // TODO: Find a way to make this underlined and have the expected cursor.
    if(GUILayout.Button(label, GitStyles.Link)) {
      Application.OpenURL(url);
    }
  }

  private static GUIContent DUMMY_WITH_SPACE = new GUIContent(" .");
  private static GUIContent DUMMY_WITHOUT_SPACE = new GUIContent(".");

  public static float SizeOfSpace(GUIStyle style) {
    // TODO: Make this less expensive than I believe it to be.
    float x1 = style.CalcSize(DUMMY_WITH_SPACE).x;
    float x2 = style.CalcSize(DUMMY_WITHOUT_SPACE).x;
    return x1 - x2;
  }


  public class PaneState {
    public int id = 0;
    public bool isDraggingSplitter = false,
                isPaneHeightChanged = false;
    public float topPaneHeight = -1, lastAvailableHeight = -1,
                 availableHeight = 0,
                 minPaneHeight = 75;

    private float _splitterHeight = 10;
    public float splitterHeight {
      get { return _splitterHeight; }
      set {
        if(value != _splitterHeight) {
          _splitterHeight = value;
          _SplitterHeight = null;
        }
      }
    }

    private GUILayoutOption _SplitterHeight = null;
    public GUILayoutOption SplitterHeight {
      get { 
        if(_SplitterHeight == null)
          _SplitterHeight = GUILayout.Height(_splitterHeight);
        return _SplitterHeight;
      }
    }
  }

  // TODO: This makes it impossible to nest pane sets!
  private static PaneState state;
  public static void BeginVerticalPanes() {
    int id = GUIUtility.GetControlID(FocusType.Passive);
    state = (PaneState)GUIUtility.GetStateObject(typeof(PaneState), id);

    Rect totalArea = EditorGUILayout.BeginVertical();
      state.availableHeight = totalArea.height - state.splitterHeight;
      state.isPaneHeightChanged = false;
      if(totalArea.height > 0) {
        if(state.topPaneHeight < 0) {
          state.topPaneHeight = state.availableHeight * 0.5f;
          state.isPaneHeightChanged = true;
        }
        if(state.lastAvailableHeight < 0)
          state.lastAvailableHeight = state.availableHeight;
        if(state.lastAvailableHeight != state.availableHeight) {
          state.topPaneHeight = state.availableHeight * (state.topPaneHeight / state.lastAvailableHeight);
          state.isPaneHeightChanged = true;
        }
        state.lastAvailableHeight = state.availableHeight;
      }

      GUILayout.BeginVertical(GUILayout.Height(state.topPaneHeight));
  }

  public static bool VerticalSplitter() {
    GUILayout.EndVertical();

    float availableHeightForOnePanel = state.availableHeight - (state.splitterHeight + state.minPaneHeight);
    Rect splitterArea = GUILayoutUtility.GetRect(NoContent, GUI.skin.box, state.SplitterHeight, ExpandWidth);
    if(splitterArea.Contains(Event.current.mousePosition) || state.isDraggingSplitter) {
      switch(Event.current.type) {
        case EventType.MouseDown:
          state.isDraggingSplitter = true;
          break;
        case EventType.MouseDrag:
          state.topPaneHeight += Event.current.delta.y;
          state.isPaneHeightChanged = true;
          break;
        case EventType.MouseUp:
          state.isDraggingSplitter = false;
          break;
      }
    }
    if(state.isPaneHeightChanged) {
      if(state.topPaneHeight < state.minPaneHeight) state.topPaneHeight = state.minPaneHeight;
      if(state.topPaneHeight >= availableHeightForOnePanel) state.topPaneHeight = availableHeightForOnePanel;
      return true;
    } else {
      return false;
    }
//        GUI.Label(splitterArea, NoContent, GUI.skin.box);
//        EditorGUIUtility.AddCursorRect(splitterArea, MouseCursor.ResizeVertical);
  }

  public static void EndVerticalPanes() {
    EditorGUILayout.EndVertical();
  }

}

using UnityEngine;
using UnityEditor;

public abstract class GitPanel {
  public GitPanel(GitShell owner) { _Shell = owner; }


  // Principal interface...
  public virtual bool IsDisabledForError { get { return !GitWrapper.IsWorking; } }
  public virtual void OnEnable() {}
  public virtual void OnDisable() {}
  public virtual void OnToolbarGUI() {}
  public virtual void OnRefresh() {}
  public abstract void OnGUI();


  // Internal tools...
  private static GUIContent DUMMY_WITH_SPACE = new GUIContent(" .");
  private static GUIContent DUMMY_WITHOUT_SPACE = new GUIContent(".");


  // Tools for panels...
  private GitShell _Shell = null;
  protected virtual GitShell Shell { get { return _Shell; } }

  protected static GUILayoutOption ExpandWidth = GUILayout.ExpandWidth(true),
                                   NoExpandWidth = GUILayout.ExpandWidth(false),
                                   ExpandHeight = GUILayout.ExpandHeight(true),
                                   NoExpandHeight = GUILayout.ExpandHeight(false);

  protected static GUIContent NoContent = GUIContent.none;
  protected static GUIStyle NoStyle = GUIStyle.none;

  protected const int SPACE_SIZE = 5;
  protected static void Space() {
    GUILayout.Space(SPACE_SIZE);
  }

  protected static void LinkTo(GUIContent label, string url) {
    // TODO: Find a way to make this underlined and have the expected cursor.
    Color c = GUI.contentColor;
    GUI.contentColor = GitStyles.LinkColor;
    if(GUILayout.Button(label, GitStyles.Link)) {
      Application.OpenURL(url);
    }
    GUI.contentColor = c;
  }

  protected static float SizeOfSpace(GUIStyle style) {
    // TODO: Make this less expensive than I believe it to be.
    float x1 = style.CalcSize(DUMMY_WITH_SPACE).x;
    float x2 = style.CalcSize(DUMMY_WITHOUT_SPACE).x;
    return x1 - x2;
  }

  protected class VerticalPaneState {
    public int id = 0;
    public bool isDraggingSplitter = false,
                isPaneHeightChanged = false;
    public float topPaneHeight = -1, initialTopPaneHeight = -1,
                 lastAvailableHeight = -1, availableHeight = 0,
                 minPaneHeightTop = 75, minPaneHeightBottom = 75;

    private float _splitterHeight = 5;
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
  private static VerticalPaneState vState;
  protected static void BeginVerticalPanes() {
    BeginVerticalPanes(null);
  }

  protected static void BeginVerticalPanes(VerticalPaneState prototype) {
    int id = GUIUtility.GetControlID(FocusType.Passive);
    vState = (VerticalPaneState)GUIUtility.GetStateObject(typeof(VerticalPaneState), id);
    if(vState.id != id) {
      vState.id = id;
      vState.isDraggingSplitter = false;
      vState.isPaneHeightChanged = false;
      vState.topPaneHeight = -1;
      vState.initialTopPaneHeight = -1;
      vState.lastAvailableHeight = -1;
      vState.availableHeight = 0;
      vState.minPaneHeightTop = 75;
      vState.minPaneHeightBottom = 75;
    } else if(prototype != null) {
      vState.id = id;
      vState.initialTopPaneHeight = prototype.initialTopPaneHeight;
      vState.minPaneHeightTop = prototype.minPaneHeightTop;
      vState.minPaneHeightBottom = prototype.minPaneHeightBottom;
    }

    Rect totalArea = EditorGUILayout.BeginVertical();
      vState.availableHeight = totalArea.height - vState.splitterHeight;
      vState.isPaneHeightChanged = false;
      if(totalArea.height > 0) {
        if(vState.topPaneHeight < 0) {
          if(vState.initialTopPaneHeight < 0)
            vState.topPaneHeight = vState.availableHeight * 0.5f;
          else
            vState.topPaneHeight = vState.initialTopPaneHeight;
          vState.isPaneHeightChanged = true;
        }
        if(vState.lastAvailableHeight < 0)
          vState.lastAvailableHeight = vState.availableHeight;
        if(vState.lastAvailableHeight != vState.availableHeight) {
          vState.topPaneHeight = vState.availableHeight * (vState.topPaneHeight / vState.lastAvailableHeight);
          vState.isPaneHeightChanged = true;
        }
        vState.lastAvailableHeight = vState.availableHeight;
      }

      GUILayout.BeginVertical(GUILayout.Height(vState.topPaneHeight));
  }

  protected static void VerticalSplitter() {
    GUILayout.EndVertical();

    float availableHeightForOnePanel = vState.availableHeight - (vState.splitterHeight + vState.minPaneHeightBottom);
    Rect splitterArea = GUILayoutUtility.GetRect(NoContent, GUI.skin.box, vState.SplitterHeight, ExpandWidth);
    if(splitterArea.Contains(Event.current.mousePosition) || vState.isDraggingSplitter) {
      switch(Event.current.type) {
        case EventType.MouseDown:
          vState.isDraggingSplitter = true;
          break;
        case EventType.MouseDrag:
          if(vState.isDraggingSplitter) {
            vState.topPaneHeight += Event.current.delta.y;
            vState.isPaneHeightChanged = true;
          }
          break;
        case EventType.MouseUp:
          vState.isDraggingSplitter = false;
          break;
      }
    }
    if(vState.isPaneHeightChanged) {
      if(vState.topPaneHeight < vState.minPaneHeightTop) vState.topPaneHeight = vState.minPaneHeightTop;
      if(vState.topPaneHeight >= availableHeightForOnePanel) vState.topPaneHeight = availableHeightForOnePanel;
      if(EditorWindow.focusedWindow != null) EditorWindow.focusedWindow.Repaint();
    }
    //GUI.Label(splitterArea, NoContent, GUI.skin.box);
    //EditorGUIUtility.AddCursorRect(splitterArea, MouseCursor.ResizeVertical);
  }

  protected static void EndVerticalPanes() {
    EditorGUILayout.EndVertical();
  }

  protected class HorizontalPaneState {
    public int id = 0;
    public bool isDraggingSplitter = false,
                isPaneWidthChanged = false;
    public float leftPaneWidth = -1, initialLeftPaneWidth = -1,
                 lastAvailableWidth = -1, availableWidth = 0,
                 minPaneWidthLeft = 75, minPaneWidthRight = 75;

    private float _splitterWidth = 5;
    public float splitterWidth {
      get { return _splitterWidth; }
      set {
        if(value != _splitterWidth) {
          _splitterWidth = value;
          _SplitterWidth = null;
        }
      }
    }

    private GUILayoutOption _SplitterWidth = null;
    public GUILayoutOption SplitterWidth {
      get { 
        if(_SplitterWidth == null)
          _SplitterWidth = GUILayout.Width(_splitterWidth);
        return _SplitterWidth;
      }
    }
  }

  // TODO: This makes it impossible to nest pane sets!
  private static HorizontalPaneState hState;
  protected static void BeginHorizontalPanes() {
    BeginHorizontalPanes(null);
  }

  protected static void BeginHorizontalPanes(HorizontalPaneState prototype) {
    int id = GUIUtility.GetControlID(FocusType.Passive);
    hState = (HorizontalPaneState)GUIUtility.GetStateObject(typeof(HorizontalPaneState), id);
    if(hState.id != id) {
      hState.id = id;
      hState.isDraggingSplitter = false;
      hState.isPaneWidthChanged = false;
      hState.leftPaneWidth = -1;
      hState.initialLeftPaneWidth = -1;
      hState.lastAvailableWidth = -1;
      hState.availableWidth = 0;
      hState.minPaneWidthLeft = 75;
      hState.minPaneWidthRight = 75;
    } else if(prototype != null) {
      hState.id = id;
      hState.initialLeftPaneWidth = prototype.initialLeftPaneWidth;
      hState.minPaneWidthLeft = prototype.minPaneWidthLeft;
      hState.minPaneWidthRight = prototype.minPaneWidthRight;
    }

    Rect totalArea = EditorGUILayout.BeginHorizontal();
      hState.availableWidth = totalArea.width - hState.splitterWidth;
      hState.isPaneWidthChanged = false;
      if(totalArea.width > 0) {
        if(hState.leftPaneWidth < 0) {
          if(hState.initialLeftPaneWidth < 0)
            hState.leftPaneWidth = hState.availableWidth * 0.5f;
          else
            hState.leftPaneWidth = hState.initialLeftPaneWidth;
          hState.isPaneWidthChanged = true;
        }
        if(hState.lastAvailableWidth < 0)
          hState.lastAvailableWidth = hState.availableWidth;
        if(hState.lastAvailableWidth != hState.availableWidth) {
          hState.leftPaneWidth = hState.availableWidth * (hState.leftPaneWidth / hState.lastAvailableWidth);
          hState.isPaneWidthChanged = true;
        }
        hState.lastAvailableWidth = hState.availableWidth;
      }

      GUILayout.BeginHorizontal(GUILayout.Width(hState.leftPaneWidth));
  }

  protected static void HorizontalSplitter() {
    GUILayout.EndHorizontal();

    float availableWidthForOnePanel = hState.availableWidth - (hState.splitterWidth + hState.minPaneWidthRight);
    Rect splitterArea = GUILayoutUtility.GetRect(NoContent, GUI.skin.box, hState.SplitterWidth, ExpandHeight);
    if(splitterArea.Contains(Event.current.mousePosition) || hState.isDraggingSplitter) {
      switch(Event.current.type) {
        case EventType.MouseDown:
          hState.isDraggingSplitter = true;
          break;
        case EventType.MouseDrag:
          if(hState.isDraggingSplitter) {
            hState.leftPaneWidth += Event.current.delta.x;
            hState.isPaneWidthChanged = true;
          }
          break;
        case EventType.MouseUp:
          hState.isDraggingSplitter = false;
          break;
      }
    }
    if(hState.isPaneWidthChanged) {
      if(hState.leftPaneWidth < hState.minPaneWidthLeft) hState.leftPaneWidth = hState.minPaneWidthLeft;
      if(hState.leftPaneWidth >= availableWidthForOnePanel) hState.leftPaneWidth = availableWidthForOnePanel;
      if(EditorWindow.focusedWindow != null) EditorWindow.focusedWindow.Repaint();
    }
    //GUI.Label(splitterArea, NoContent, GUI.skin.box);
    //EditorGUIUtility.AddCursorRect(splitterArea, MouseCursor.ResizeHorizontal);
  }

  protected static void EndHorizontalPanes() {
    EditorGUILayout.EndHorizontal();
  }
}

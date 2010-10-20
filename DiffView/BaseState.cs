using UnityEngine;

namespace UnityGit.DiffView.State {
  public abstract class BaseState {
    protected bool isDirty = false;

    private GUIStyle _header = null;
    public GUIStyle header {
      set { if(_header != value) { _header = value; isDirty = true; } }
      get { return _header ?? Styles.Header; }
    }

    private GUIStyle _markerPosition = null;
    public GUIStyle markerPosition {
      set { if(_markerPosition != value) { _markerPosition = value; isDirty = true; } }
      get { return _markerPosition ?? Styles.MarkerPosition; }
    }

    private GUIStyle _markerContext = null;
    public GUIStyle markerContext {
      set { if(_markerContext != value) { _markerContext = value; isDirty = true; } }
      get { return _markerContext ?? Styles.MarkerContext; }
    }

    private GUIStyle _normal = null;
    public GUIStyle normal {
      set { if(_normal != value) { _normal = value; isDirty = true; } }
      get { return _normal ?? Styles.Normal; }
    }

    private GUIStyle _addition = null;
    public GUIStyle addition {
      set { if(_addition != value) { _addition = value; isDirty = true; } }
      get { return _addition ?? Styles.Addition; }
    }

    private GUIStyle _removal = null;
    public GUIStyle removal {
      set { if(_removal != value) { _removal = value; isDirty = true; } }
      get { return _removal ?? Styles.Removal; }
    }

    private string _content;
    public string content {
      set { if(_content != value) { _content = value; isDirty = true; } }
      get { return _content; }
    }

    protected Line[] _lines;
    public Line[] lines {
      get {
        if(_lines == null || isDirty)
          OnRefresh();
        return _lines;
      }
    }

    public abstract void OnRefresh();
  }
}
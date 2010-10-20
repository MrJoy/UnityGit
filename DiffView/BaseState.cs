using UnityEngine;

namespace UnityGit.DiffView.State {
  public abstract class BaseState {
    protected bool isDirty = false;

    private Color _header = Color.white;
    public Color header {
      set { if(_header != value) { _header = value; isDirty = true; } }
      get { return _header; }
    }

    private Color _marker = Color.cyan;
    public Color marker {
      set { if(_marker != value) { _marker = value; isDirty = true; } }
      get { return _marker; }
    }

    private Color _unchanged = Color.black;
    public Color unchanged {
      set { if(_unchanged != value) { _unchanged = value; isDirty = true; } }
      get { return _unchanged; }
    }

    private Color _add = Color.green;
    public Color add {
      set { if(_add != value) { _add = value; isDirty = true; } }
      get { return _add; }
    }

    private Color _remove = Color.red;
    public Color remove {
      set { if(_remove != value) { _remove = value; isDirty = true; } }
      get { return _remove; }
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
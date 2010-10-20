using System.Collections.Generic;
using UnityEngine;

namespace UnityGit.DiffView.State {
  public class Line {
    public string[] segments;
    public GUIStyle[] styles;
    public float[] widths;
  }

  public class LinesBuilder {
    private List<Line> linesTmp = new List<Line>();
    private List<float> widthsTmp = new List<float>();
    private List<string> segmentsTmp = new List<string>();
    private List<GUIStyle> stylesTmp = new List<GUIStyle>();

    public void AddSegment(GUIStyle c, string s) {
      segmentsTmp.Add(s);
      stylesTmp.Add(c);
      widthsTmp.Add(Styles.TrueWidth(c, s));
    }

    public void CommitLine() {
      if(segmentsTmp.Count > 0) {
        linesTmp.Add(new Line() {
          segments = segmentsTmp.ToArray(),
          styles = stylesTmp.ToArray(),
          widths = widthsTmp.ToArray()
        });
        stylesTmp.Clear();
        segmentsTmp.Clear();
        widthsTmp.Clear();
      }
    }

    public Line[] ToArray() {
      return linesTmp.ToArray();
    }
  }
}
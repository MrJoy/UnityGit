using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UnityGit.DiffView.State {
  public class Line {
    public string[] segments;
    public GUIStyle[] styles;
  }

  public class LinesBuilder {
    private List<Line> linesTmp = new List<Line>();
    private List<string> segmentsTmp = new List<string>();
    private List<GUIStyle> stylesTmp = new List<GUIStyle>();

    public void AddSegment(GUIStyle c, string s) {
      segmentsTmp.Add(s);
      stylesTmp.Add(c);
    }

    public void CommitLine() {
      if(segmentsTmp.Count > 0) {
        linesTmp.Add(new Line() {
          segments = segmentsTmp.ToArray(),
          styles = stylesTmp.ToArray()
        });
        stylesTmp.Clear();
        segmentsTmp.Clear();
      }
    }

    public Line[] ToArray() {
      return linesTmp.ToArray();
    }
  }
}
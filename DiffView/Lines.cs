using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UnityGit.DiffView.State {
  public class Line {
    public string[] segments;
    public Color[] colors;
  }

  public class LinesBuilder {
    private List<Line> linesTmp = new List<Line>();
    private List<string> segmentsTmp = new List<string>();
    private List<Color> colorsTmp = new List<Color>();

    public void AddSegment(Color c, string s) {
      segmentsTmp.Add(s);
      colorsTmp.Add(c);
    }

    public void CommitLine() {
      linesTmp.Add(new Line() {
        segments = segmentsTmp.ToArray(),
        colors = colorsTmp.ToArray()
      });
    }

    public Line[] ToArray() {
      return linesTmp.ToArray();
    }
  }
}
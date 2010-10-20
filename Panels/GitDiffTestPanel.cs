using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GitDiffTestPanel : GitPanel {
  protected void ShowDiffView() {
//    GUILayout.Box(tmp, GitStyles.FileListBox, ExpandWidth, ExpandHeight);
  }

  public override void OnGUI() {
    ShowDiffView();
  }

  // Base constructor
  public GitDiffTestPanel(GitShell owner) : base(owner) {}
}

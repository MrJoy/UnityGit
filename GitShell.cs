//-----------------------------------------------------------------
//  GitShell v0.1
//  Copyright 2010 MrJoy, Inc.
//  All rights reserved
//
//-----------------------------------------------------------------
// Unity editor extension for Git integration.
//-----------------------------------------------------------------
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GitShell : EditorWindow {
  public const string VERSION = "0.0.1";

  public void OnEnable() {
    foreach(GitPanel panel in panels)
      if(panel != null)
        panel.OnEnable();
  }

  public void OnDisable() {
    foreach(GitPanel panel in panels)
      if(panel != null)
        panel.OnDisable();
  }

  public void OnLostFocus() { }

  public void OnDestroy() { }

  [System.NonSerialized]
  protected int panelIndex = 0;
  [System.NonSerialized]
  protected GUIContent[] panelLabels = new GUIContent[] {
    new GUIContent("Status"),
    new GUIContent("Commit"),
    new GUIContent("Remote"),
    new GUIContent("History"),
    null,
    new GUIContent("About")
  };

  [System.NonSerialized]
  protected GitPanel[] panels = new GitPanel[] {
    null,
    null,
    null,
    null,
    null,
    new GitAboutPanel()
  };

  public void OnGUI() {
    GUILayout.BeginHorizontal(EditorStyles.toolbar, GitPanel.ExpandWidth);
      for(int i = 0; i < panelLabels.Length; i++) {
        if(panelLabels[i] == null)
          GUILayout.Label(GUIContent.none, GUIStyle.none, GitPanel.ExpandWidth);
        else {
          if(GUILayout.Toggle((panelIndex == i), panelLabels[i], EditorStyles.toolbarButton, GitPanel.NoExpandWidth))
            panelIndex = i;
        }
      }
    GUILayout.EndHorizontal();
    GUILayout.BeginVertical(GitStyles.Indented);
      GitPanel panel = panels[panelIndex];
      if(panel != null)
        panel.OnGUI();
      else
        GUILayout.Label("Not implemented yet.");
    GUILayout.EndVertical();
  }

  [MenuItem("Window/Git Shell #%g")]
  public static void Init() {
    GitShell window = (GitShell)EditorWindow.GetWindow(typeof(GitShell));
    window.title = "Git";
    window.Show();
  }
}

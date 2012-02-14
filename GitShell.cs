//-----------------------------------------------------------------
//  GitShell v0.1
//  Copyright 2012 MrJoy, Inc.
//  All rights reserved
//
//-----------------------------------------------------------------
// Unity editor extension for Git integration.
//-----------------------------------------------------------------
using System;
using UnityEditor;
using UnityEngine;

public class GitShell : EditorWindow {
  public const string VERSION = "0.0.2";
  public const string COPYRIGHT = "(C)Copyright 2012 MrJoy, Inc.";

  protected GUIContent refreshButton = new GUIContent();

  public void OnEnable() {
    refreshButton.image = AssetDatabase.LoadAssetAtPath("Assets/Editor/UnityGit/Resources/Refresh.png", typeof(Texture2D)) as Texture2D;
    foreach(GitPanel panel in panels)
      if(panel != null)
        panel.OnEnable();
  }

  public void OnDisable() {
    foreach(GitPanel panel in panels)
      if(panel != null)
        panel.OnDisable();
  }

  private bool forceRefresh = false;

  public void OnFocus() {
    forceRefresh = true;
  }

  public void OnLostFocus() { }

  public void OnDestroy() { }

  [System.NonSerialized]
  protected int panelIndex = 0;
  [System.NonSerialized]
  protected GUIContent[] panelLabels = new GUIContent[] {
    new GUIContent("Commit"),
    new GUIContent("Branch/Merge"),
    new GUIContent("Remotes"),
    new GUIContent("History"),
    new GUIContent("Refs"),
    null,
    new GUIContent("TEST Diff"),
    new GUIContent("About")
  };

  [System.NonSerialized]
  protected GitPanel[] panels = null;
  public GitShell() {
    // Need to do this here to enable "this" reference.
    panels = new GitPanel[] {
      new GitStatusPanel(this),
      null,
      null,
      null,
      new GitRefsPanel(this),
      null,
      new GitDiffTestPanel(this),
      new GitAboutPanel(this)
    };
  }

  public void OnGUI() {
    GitPanel panel = panels[panelIndex];

    EditorGUILayoutToolbar.Begin();
      if(GUILayout.Button(refreshButton, EditorStyles.toolbarButton, GUIHelper.NoExpandWidth))
        forceRefresh = true;

      if(forceRefresh && panel != null) {
        forceRefresh = false;
        panel.OnRefresh();
      }

      EditorGUILayoutToolbar.Space();
      bool hasShownToolbar = false;
      for(int i = 0; i < panelLabels.Length; i++) {
        if(panelLabels[i] == null) {
          if(!hasShownToolbar) {
            hasShownToolbar = true;
            EditorGUILayoutToolbar.Space();
            if(panel != null && !panel.IsDisabledForError) {
              panel.OnToolbarGUI();
            }
            EditorGUILayoutToolbar.FlexibleSpace();
          }
        } else {
          if(GUILayout.Toggle((panelIndex == i), panelLabels[i], EditorStyles.toolbarButton, GUIHelper.NoExpandWidth))
            panelIndex = i;
        }
      }
    EditorGUILayoutToolbar.End();
    GUILayout.BeginVertical(GitStyles.Indented);
      panel = panels[panelIndex];
      if(panel != null) {
        if(!panel.IsDisabledForError)
          panel.OnGUI();
        else {
          Color c = GUI.color;
          GUI.color = GitStyles.ErrorColor;
          GUILayout.Label("Whoops!  Encountered an error in git.  Please see the About tab to ensure git is set up properly!", GitStyles.WhiteBoldLabel);
          GUI.color = c;
        }
      } else {
        GUILayout.Label("Not implemented yet.");
      }
    GUILayout.EndVertical();
  }

  [MenuItem("Window/Git Shell #%g")]
  public static void Init() {
    GitShell window = (GitShell)EditorWindow.GetWindow(typeof(GitShell));
    window.title = "Git";
    window.Show();
  }
}

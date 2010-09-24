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
//using System.Reflection;
//using System.Text;
//using System.IO;
//using Mono.CSharp;
/*
internal class ReflectionProxy {
  internal const BindingFlags PUBLIC_STATIC = BindingFlags.Public | BindingFlags.Static;
  internal const BindingFlags NONPUBLIC_STATIC = BindingFlags.NonPublic | BindingFlags.Static;

  protected static Type[] Signature(params Type[] sig) { return sig; }
}

// WARNING: Absolutely NOT thread-safe!
internal class EvaluatorProxy : ReflectionProxy {
  private static readonly Type _Evaluator = typeof(Evaluator);
  private static readonly FieldInfo _fields = _Evaluator.GetField("fields", NONPUBLIC_STATIC);

  internal static Hashtable fields { get { return (Hashtable)_fields.GetValue(null); } }
}

// WARNING: Absolutely NOT thread-safe!
internal class TypeManagerProxy : ReflectionProxy {
  private static readonly Type _TypeManager = typeof(Evaluator).Assembly.GetType("Mono.CSharp.TypeManager");
  private static readonly MethodInfo _CSharpName = _TypeManager.GetMethod("CSharpName", PUBLIC_STATIC, null, Signature(typeof(Type)), null);

  // Save an allocation per access here...
  private static readonly object[] _CSharpNameParams = new object[] { null };
  internal static string CSharpName(Type t) {
    // TODO: What am I doing wrong here that this throws on generics??
    string name = "";
    try {
      _CSharpNameParams[0] = t;
      name = (string)_CSharpName.Invoke(null, _CSharpNameParams);
    } catch(Exception) {
      name = "<error>";
    }
    return name;
  }
}
*/

public class GitShell : EditorWindow {
  public const string VERSION = "0.0.1";

//  public void Update() { }
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

//  public Vector2 scrollPosition = Vector2.zero;

  protected int panelIndex = 0;
  protected GUIContent[] panelLabels = new GUIContent[] {
    new GUIContent("Status"),
    new GUIContent("Commit"),
    new GUIContent("Remote"),
    null,
    new GUIContent("About")
  };
  protected GitPanel[] panels = new GitPanel[] {
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
          //GUILayout.FlexibleSpace(ExpandWidth);
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
    GUILayout.EndVertical();
  }

  [MenuItem("Window/Git Shell #%g")]
  public static void Init() {
    GitShell window = (GitShell)EditorWindow.GetWindow(typeof(GitShell));
    window.title = "Git";
    window.Show();
  }
}

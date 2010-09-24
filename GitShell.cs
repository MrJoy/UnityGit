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

  protected GitAboutPanel aboutPanel = new GitAboutPanel();

//  public void Update() { }
  public void OnEnable() {
    aboutPanel.OnEnable();
  }
  public void OnDisable() {
    aboutPanel.OnDisable();
  }
  public void OnLostFocus() { }
  public void OnDestroy() { }

//  public Vector2 scrollPosition = Vector2.zero;

  public void OnGUI() {
    aboutPanel.OnGUI();
  }

  [MenuItem("Window/Git Shell #%g")]
  public static void Init() {
    GitShell window = (GitShell)EditorWindow.GetWindow(typeof(GitShell));
    window.title = "Git";
    window.Show();
  }
}

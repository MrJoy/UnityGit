using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NGit;
using NGit.Api;
using NGit.Dircache;
using NGit.Storage.File;

public static class NGitWrapper {
  private static Git git = null;
  /*
  public static InitCommand Init ();
  public virtual CommitCommand Commit ();
  public virtual LogCommand Log ();
  public virtual MergeCommand Merge ();
  public virtual PullCommand Pull ();
  public virtual CreateBranchCommand BranchCreate ();
  public virtual DeleteBranchCommand BranchDelete ();
  public virtual RenameBranchCommand BranchRename ();
  public virtual AddCommand Add ();
  public virtual TagCommand Tag ();
  public virtual FetchCommand Fetch ();
  public virtual PushCommand Push ();
  public virtual CherryPickCommand CherryPick ();
  public virtual RevertCommand Revert ();
  public virtual RebaseCommand Rebase ();
  public virtual RmCommand Rm ();
  public virtual CheckoutCommand Checkout ();
  */
  public static Git GitRef {
    get {
      if(git == null) {
        string path = Path.GetFullPath(Path.Combine(Path.Combine(Application.dataPath, ".."), ".git"));
        Debug.Log("Looking for repo at: " + path);
        git = new Git(new FileRepository(path));
      }
      return git;
    }
  }

  private static bool _isWorking = true;
  public static bool IsWorking { get { return _isWorking && (EditorSettings.externalVersionControl == ExternalVersionControl.Generic); } }

  public static IList<Ref> Branches {
    get {
      return GitRef.BranchList().Call();
    }
  }

//  public virtual AddCommand Add ();
//    public virtual AddCommand AddFilepattern (string filepattern);
//    public virtual AddCommand SetWorkingTreeIterator (WorkingTreeIterator f);
//    public virtual DirCache Call ();
//    public virtual AddCommand SetUpdate (bool update);
//    public virtual bool IsUpdate ();
  public static AddCommand StagePath(string path) {
    return GitRef.Add().AddFilepattern(path);
  }

//  public virtual RmCommand Rm ();
  public static void UnstagePath(string path) {
    throw new Exception("Unimplemented");
  }
}
public static class GitWrapper {
  private static bool _isWorking = true;
  public static bool IsWorking { get { return _isWorking; } }

  public static string CurrentBranch {
    get {
      try {
        return ShellHelpers.OutputFromCommand("git", "symbolic-ref --quiet HEAD")
          .Replace("refs/heads/", "").Replace("\n", "");
      } catch {
        _isWorking = false;
        return null;
      }
    }
  }

  public static string Version {
    get {
      try {
        return ShellHelpers.OutputFromCommand("git", "--version").Replace("git version ", "");
      } catch {
        _isWorking = false;
        return null;
      }
    }
  }

  public struct Change {
    string path;
  }

  public static Change[] Status {
    get {
      try {
        string tmp = ShellHelpers.OutputFromCommand("git", "status --porcelain");
      } catch {
        
      }
      return null;
    }
  }

  public static string GitBinary {
    get {
      // TODO: This is OSX-specific.  We should fix that.
      try {
        return ShellHelpers.OutputFromCommand("which", "git");
      } catch {
        _isWorking = false;
        return null;
      }
    }
  }
}
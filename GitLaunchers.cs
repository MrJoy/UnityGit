using UnityEditor;

public class GitLaunchers {
  [MenuItem("Tools/Git/Commit Editor")]
  public static void LaunchGitGui() {
    ShellHelpers.StartProcess("git", "gui");
  }

  [MenuItem("Tools/Git/Visualize All Branches")]
  public static void LaunchGitk() {
    ShellHelpers.StartProcess("gitk", "--all");
  }
}

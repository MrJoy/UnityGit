//define DEBUG_COMMANDS
using System.Diagnostics;

public class ShellHelpers {
  public static Process StartProcess(string filename, string arguments) {
#if DEBUG_COMMANDS
    UnityEngine.Debug.Log("Running: " + filename + " " + arguments);
#endif
    Process p = new Process();
    p.StartInfo.Arguments = arguments;
    p.StartInfo.CreateNoWindow = true;
    p.StartInfo.UseShellExecute = false;
    p.StartInfo.RedirectStandardOutput = true;
    p.StartInfo.RedirectStandardInput = true;
    p.StartInfo.RedirectStandardError = true;
    p.StartInfo.FileName = filename;
    p.Start();
    return p;
  }

  public static string OutputFromCommand(string filename, string arguments) {
    var p = StartProcess(filename, arguments);
    var output = p.StandardOutput.ReadToEnd();
    p.WaitForExit();
    if(output.EndsWith("\n"))
      output = output.Substring(0, output.Length - 1);
    return output;
  }
}

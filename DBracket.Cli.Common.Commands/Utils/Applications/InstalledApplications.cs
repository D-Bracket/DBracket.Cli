using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DBracket.Cli.Common.Commands.Utils.Applications;

public class InstalledApplications : ObservableCollection<ApplicationInformation>
{
    #region "----------------------------- Private Fields ------------------------------"

    #endregion



    #region "------------------------------ Constructor --------------------------------"

    #endregion



    #region "--------------------------------- Methods ---------------------------------"
    #region "----------------------------- Public Methods ------------------------------"
    public static InstalledApplications GetInstalledApps()
    {
        var apps = new InstalledApplications();
        apps.Clear();

        string[] keys = [
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall",
            @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
            @"HKCU\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"];


        for (int i = 0; i < 4; i++)
        {
            var uninstallKey = keys[i];
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                if (rk is null)
                    continue;

                foreach (string subKeyName in rk.GetSubKeyNames())
                {
                    using (RegistryKey subKey = rk.OpenSubKey(subKeyName))
                    {
                        string displayName = subKey?.GetValue("DisplayName") as string;
                        string displayIcon = subKey?.GetValue("DisplayIcon") as string ?? "";
                        string installLocation = subKey?.GetValue("InstallLocation") as string ?? "";

                        if (!string.IsNullOrEmpty(displayName))
                        {
                            var app = new ApplicationInformation(displayName, displayIcon, installLocation, string.Empty);
                            apps.Add(app);
                            Debug.WriteLine(displayName);
                        }
                    }
                }
            }
        }

        var psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = "Get-AppxPackage | Select Name, PackageFullName",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string output = string.Empty;
        string errors = string.Empty;
        using (var process = Process.Start(psi))
        {
            output = process.StandardOutput.ReadToEnd();
            errors = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }
        Debug.WriteLine("Output:");
        Debug.WriteLine(output);
        var porgs = output.Split("\r\n");
        for (int i = 3; i < porgs.Length; i++)
        {
            var prog = porgs[i];
            if (string.IsNullOrEmpty(prog))
                continue;

            var t = prog.Split(" ");
        }
        if (!string.IsNullOrWhiteSpace(errors))
        {
            Debug.WriteLine("Errors:");
            Debug.WriteLine(errors);
        }

        return apps;
    }

    //public static void ListUwpApps()
    //{
    //    //System.Diagnostics.Process.Start("explorer.exe", "shell:appsFolder\\Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");


    //    var packageManager = new PackageManager();
    //    var packages = packageManager.FindPackagesForUser(string.Empty); // empty string = current user

    //    foreach (var package in packages)
    //    {
    //        Console.WriteLine($"Name: {package.Id.Name}");
    //        Console.WriteLine($"Publisher: {package.Id.Publisher}");
    //        Console.WriteLine($"Installed Location: {package.InstalledLocation.Path}");
    //        Console.WriteLine($"Full Name: {package.Id.FullName}");
    //        Console.WriteLine(new string('-', 40));
    //    }
    //}
    #endregion

    #region "----------------------------- Private Methods -----------------------------"

    #endregion

    #region "------------------------------ Event Handling -----------------------------"

    #endregion
    #endregion



    #region "--------------------------- Public Propterties ----------------------------"
    #region "------------------------------- Properties --------------------------------"

    #endregion

    #region "--------------------------------- Events ----------------------------------"

    #endregion
    #endregion
}
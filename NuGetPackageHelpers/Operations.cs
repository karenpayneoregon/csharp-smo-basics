using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using NuGetPackageHelpers.Classes;
using NuGetPackageHelpers.Extensions;

namespace NuGetPackageHelpers
{
    /// <summary>
    /// WIP
    /// </summary>
    public class Operations 
    {
        /// <summary>
        /// Traverse text from clipboard taken from Package manager command Get-Package | ft -AutoSize
        /// and create Git table.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string Read(string[] lines)
        {

            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                sb.AppendLine($"|{line.TrimEnd().RemoveDoubleSpacings("|")}|");
            }

            return sb.ToString();
        }
        /// <summary>
        /// Partly done method to create a git table for readme markdown file
        /// </summary>
        public static void BuilderPackageTable()
        {
            string[] exclude = new[] {".git",".vs", "packages"};
            var solutionFolder = GetFoldersToParent.GetSolutionFolderPath();
            var folders = Directory.GetDirectories(solutionFolder).Where(path => !exclude.Contains(path.Split('\\').Last()));

            foreach (var folder in folders)
            {
                var fileName = (Path.Combine(folder, "packages.config"));

                if (File.Exists(fileName))
                {
                    var projectNameWithoutExtension = Path.GetFileNameWithoutExtension(Directory.GetFiles(folder, "*.csproj")[0]);

                    Console.WriteLine(projectNameWithoutExtension);

                    var document = XDocument.Load(fileName);

                    foreach (var packageNode in document.XPathSelectElements("/packages/package"))
                    {
                        string identifier = packageNode.Attribute("id").Value;
                        string version = packageNode.Attribute("version").Value;

                        Console.WriteLine($"\t{identifier}, {version}");

                    }

                    Console.WriteLine();
                }
             
            }
        }
    }
}

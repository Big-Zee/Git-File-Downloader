using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubFileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new Repository("https://github.com/zamipl/Git-File-Downloader/blob/master/README.md");
            foreach (var item in repo.RetrieveStatus())
            {
                if (item.State == FileStatus.ModifiedInIndex)
                {
                    var blob = repo.Head.Tip[item.FilePath].Target as Blob;
                    string commitContent;
                    using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                    {
                        commitContent = content.ReadToEnd();
                    }
                    string workingContent;
                    using (var content = new StreamReader(repo.Info.WorkingDirectory + Path.DirectorySeparatorChar + item.FilePath, Encoding.UTF8))
                    {
                        workingContent = content.ReadToEnd();
                    }
                    Console.WriteLine("\n\n~~~~ Original file ~~~~");
                    Console.WriteLine(commitContent);
                    Console.WriteLine("\n\n~~~~ Current file ~~~~");
                    Console.WriteLine(workingContent);
                }
            }
        }
    }
}

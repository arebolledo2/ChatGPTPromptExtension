using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChatGPTPrompt.Parsing
{
    internal class SelectedFileParser
    {
        public string ParseFiles(DTE2 dte, IEnumerable<string> filePaths)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var fileContents = new List<string>();

            foreach (var filePath in filePaths)
            {
                var document = dte.Documents.Cast<Document>().FirstOrDefault(doc => doc.FullName == filePath);

                if (document != null)
                {
                    // The document is open in Visual Studio. We need to get the TextDocument object
                    // which allows us to access the content of the document.
                    var textDocument = (TextDocument)document.Object("TextDocument");
                    string content = textDocument.StartPoint.CreateEditPoint().GetText(textDocument.EndPoint);
                    fileContents.Add(content);
                }
                else
                {
                    // The document is not open, so we read the file from the disk.
                    string content = File.ReadAllText(filePath);
                    fileContents.Add(content);
                }
            }

            return string.Join("\n\n", fileContents);
        }
    }
}

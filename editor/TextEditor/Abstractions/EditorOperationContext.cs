using System.Text;

namespace EditorProject.TextEditor.Abstractions
{
    public class EditorOperationContext
    {
        public string Message { get; set; }
        public string FileName { get; set; }
        public StringBuilder StringBuffer { get; set; }
    }
}
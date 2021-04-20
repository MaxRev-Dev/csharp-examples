using System.IO;
using System.Text;
using System.Threading.Tasks;
using EditorProject.TextEditor.Abstractions;

namespace EditorProject.TextEditor.Persistence
{
    public class WriteFile : ITextEditorOperation
    {
        public int WriteBufferSize { get; set; } = 4096 << 2;
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public async Task ExecuteAsync(EditorOperationContext operationContext)
        {
            var file = operationContext.FileName;

            await using var stream = File.Exists(file)
                ? File.Open(file, FileMode.Truncate, FileAccess.Write)
                : File.Create(file, WriteBufferSize);

            await using var writer = new StreamWriter(stream, Encoding, WriteBufferSize);
            await writer.WriteAsync(operationContext.StringBuffer);
            operationContext.Message = "File saved successfully";
        }
    }
}
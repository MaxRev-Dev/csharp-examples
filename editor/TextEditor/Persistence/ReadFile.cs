using System.IO;
using System.Text;
using System.Threading.Tasks;
using EditorProject.TextEditor.Abstractions;

namespace EditorProject.TextEditor.Persistence
{
    public class ReadFile : ITextEditorOperation
    {
        public async Task ExecuteAsync(EditorOperationContext operationContext)
        {
            var file = operationContext.FileName;

            if (File.Exists(file))
            {
                await using var stream = File.OpenRead(file);
                using var reader = new StreamReader(stream);
                operationContext.StringBuffer = new StringBuilder(await reader.ReadToEndAsync());
            }
            else
            {
                operationContext.Message = "File not exists. Create new?";
            }
        }
    }
}
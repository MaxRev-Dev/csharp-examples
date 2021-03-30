using System.Threading.Tasks;

namespace EditorProject.TextEditor.Abstractions
{
    public interface ITextEditorOperation
    {
        Task ExecuteAsync(EditorOperationContext operationContext);
    }
}
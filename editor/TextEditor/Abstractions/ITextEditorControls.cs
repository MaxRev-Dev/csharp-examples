using System.Threading.Tasks;

namespace EditorProject.TextEditor.Abstractions
{
    public interface ITextEditorControls
    {
        Task Open();
        Task SaveAs();
        Task Save();
        Task Create();
    }
}
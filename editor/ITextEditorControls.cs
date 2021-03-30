using System.Threading.Tasks;

namespace EditorProject
{
    public interface ITextEditorControls
    {
        Task Open();
        Task SaveAs();
        Task Save();
        Task Create();
    }
}
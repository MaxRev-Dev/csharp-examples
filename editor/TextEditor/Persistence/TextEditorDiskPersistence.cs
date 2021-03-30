using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EditorProject.TextEditor.Abstractions;

namespace EditorProject.TextEditor.Persistence
{
    public class TextEditorControlsDiskPersistence : ITextEditorControls
    {
        private readonly RichTextBox _control;
        private readonly Action<string> _onInfoMessage;
        private readonly Action<string> _onErrorMessage;
        private readonly ITextEditorOperation _readOperation;
        private readonly ITextEditorOperation _writeOperation;
        private string _fileFilterDefault = @"Text file|*.txt|RichTextFile|*.rtf";
        private EditorOperationContext _context;

        public TextEditorControlsDiskPersistence(RichTextBox editorTextBox,
            Action<string> onInfoMessage, Action<string> onErrorMessage)
        {
            _control = editorTextBox;
            _onInfoMessage = onInfoMessage;
            _onErrorMessage = onErrorMessage;
            _readOperation = new ReadFile();
            _writeOperation = new WriteFile();
        }

        public async Task Open()
        {
            var fileDialog = new OpenFileDialog { Filter = _fileFilterDefault };
            var dialogResult = fileDialog.ShowDialog(_control.Parent);

            if (dialogResult != DialogResult.OK) return;
            try
            {
                _context = new EditorOperationContext
                {
                    FileName = fileDialog.FileName
                };
                await _readOperation.ExecuteAsync(_context);

                // perform UI update in synchronized context
                _control.Invoke(new Action(() =>
                {
                    _control.Text = _context.StringBuffer.ToString();
                    PostProcessContext();
                }));

            }
            catch (Exception ex)
            {
                _onErrorMessage.Invoke(ex.Message);
            }
        }

        public async Task Save()
        {
            if (_context?.FileName != null)
            {
                _context.StringBuffer = new StringBuilder(_control.Text);
                await _writeOperation.ExecuteAsync(_context);
                _control.Invoke(new Action(PostProcessContext));
            }
            else
                await SaveAs();
        }
        public Task SaveAs() => SaveAsCore(false);

        public async Task SaveAsCore(bool cleanUpAfter)
        {
            var fileDialog = new SaveFileDialog { Filter = _fileFilterDefault };
            var dialogResult = fileDialog.ShowDialog(_control.Parent);

            if (dialogResult != DialogResult.OK) return;
            try
            {
                _context.FileName = fileDialog.FileName;
                _context.StringBuffer = new StringBuilder(_control.Text);
                await _writeOperation.ExecuteAsync(_context);
                _control.Invoke(new Action(() =>
                {
                    PostProcessContext();
                    if (cleanUpAfter) _control.Clear();
                }));
            }
            catch (Exception ex)
            {
                _onErrorMessage.Invoke(ex.Message);
            }
        }

        private void PostProcessContext()
        {
            if (!string.IsNullOrEmpty(_context.Message))
                _onInfoMessage.Invoke(_context.Message);
        }

        public async Task Create()
        {
            if (_control.TextLength > 0)
            {
                var result = MessageBox.Show(@"Do you wan't to save file?", @"Save or clear",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.No) _control.Clear();
                if (result == DialogResult.OK)
                {
                    await SaveAsCore(true);
                }
            }

            var fileDialog = new OpenFileDialog
            {
                Filter = _fileFilterDefault,
                CheckFileExists = false
            };
            var dialogResult = fileDialog.ShowDialog(_control.Parent);

            if (dialogResult != DialogResult.OK) return;

            _context = new EditorOperationContext
            {
                FileName = fileDialog.FileName,
            };
            
            _control.Invoke(new Action(PostProcessContext));
        }
    }
}
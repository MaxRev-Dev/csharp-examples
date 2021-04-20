using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EditorProject.TextEditor.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Just create hierarchical controls via maps
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="editorActions"></param>
        public static void EnableControls(this ToolStrip ts,
            IDictionary<string, Dictionary<string, (Action onInvoke, Func<bool> onValidation)>> editorActions)
        {
            foreach (var dropAction in editorActions)
            {
                var fileItem = new ToolStripDropDownButton
                {
                    Text = dropAction.Key
                };

                foreach (var action in dropAction.Value)
                {
                    var actionItem = new ToolStripMenuItem
                    {
                        Text = action.Key
                    };
                    // invoke action click delegate
                    actionItem.Click += (sender, e) => action.Value.onInvoke?.Invoke();
                    // validate to enable
                    fileItem.DropDown.Opening += (s, e) =>
                        actionItem.Enabled = action.Value.onValidation?.Invoke() ?? false;
                    fileItem.DropDownItems.Add(actionItem);
                }

                ts.Items.Add(fileItem);
            }
        }

        /// <summary>
        ///     Adapter from here
        ///     <see
        ///         href="https://ourcodeworld.com/articles/read/1094/how-to-implement-a-copy-cut-and-paste-context-menu-over-a-rich-text-box-in-winforms-c-sharp" />
        /// </summary>
        public static void EnableContextMenu(this RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip != null) return;

            rtb.ContextMenuStrip = GetContextMenuStrip(rtb);
        }

        public static ContextMenuStrip GetContextMenuStrip(RichTextBox rtb)
        {
            var cms = new ContextMenuStrip {ShowImageMargin = false};

            var tsmiUndo = new ToolStripMenuItem("Undo");
            tsmiUndo.Click += (sender, e) => rtb.Undo();
            cms.Items.Add(tsmiUndo);

            var tsmiRedo = new ToolStripMenuItem("Redo");
            tsmiRedo.Click += (sender, e) => rtb.Redo();
            cms.Items.Add(tsmiRedo);

            cms.Items.Add(new ToolStripSeparator());

            var tsmiCut = new ToolStripMenuItem("Cut");
            tsmiCut.Click += (sender, e) => rtb.Cut();
            cms.Items.Add(tsmiCut);

            var tsmiCopy = new ToolStripMenuItem("Copy");
            tsmiCopy.Click += (sender, e) => rtb.Copy();
            cms.Items.Add(tsmiCopy);

            var tsmiPaste = new ToolStripMenuItem("Paste");
            tsmiPaste.Click += (sender, e) => rtb.Paste();
            cms.Items.Add(tsmiPaste);

            var tsmiDelete = new ToolStripMenuItem("Delete");
            tsmiDelete.Click += (sender, e) => rtb.SelectedText = "";
            cms.Items.Add(tsmiDelete);

            cms.Items.Add(new ToolStripSeparator());

            var tsmiSelectAll = new ToolStripMenuItem("Select All");
            tsmiSelectAll.Click += (sender, e) => rtb.SelectAll();
            cms.Items.Add(tsmiSelectAll);

            cms.Items.Add(new ToolStripSeparator());

            foreach (var fontStyle in Enum.GetValues<FontStyle>())
            {
                var tsmiStyle = new ToolStripMenuItem(fontStyle.ToString());
                tsmiStyle.Click += (sender, e) =>
                    rtb.SelectionFont = new Font(rtb.SelectionFont, rtb.SelectionFont.Style ^ fontStyle);
                cms.Items.Add(tsmiStyle);

                cms.Opening += (sender, e) =>
                    tsmiStyle.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
            }

            cms.Items.Add(new ToolStripSeparator());

            var tsmiStyleClear = new ToolStripMenuItem("Clear style");
            tsmiStyleClear.Click += (sender, e) =>
                rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Regular);
            cms.Items.Add(tsmiStyleClear);


            cms.Opening += (sender, e) =>
            {
                tsmiUndo.Enabled = !rtb.ReadOnly && rtb.CanUndo;
                tsmiRedo.Enabled = !rtb.ReadOnly && rtb.CanRedo;
                tsmiCut.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                tsmiCopy.Enabled = rtb.SelectionLength > 0;
                tsmiPaste.Enabled = !rtb.ReadOnly && Clipboard.ContainsText();
                tsmiDelete.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                tsmiSelectAll.Enabled = rtb.TextLength > 0 && rtb.SelectionLength < rtb.TextLength;
            };

            return cms;
        }
    }
}
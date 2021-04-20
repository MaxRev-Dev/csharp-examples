namespace EditorProject
{
    partial class Playground
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Playground));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCalc = new System.Windows.Forms.TabPage();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textEditorStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.editorTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.drawingToolstrip = new System.Windows.Forms.ToolStrip();
            this.drawCurveBtn = new System.Windows.Forms.ToolStripButton();
            this.drawCircleBtn = new System.Windows.Forms.ToolStripButton();
            this.drawRectangleBtn = new System.Windows.Forms.ToolStripButton();
            this.drawTriangleBtn = new System.Windows.Forms.ToolStripButton();
            this.drawingCanvas = new System.Windows.Forms.PictureBox();
            this.saveImageAsBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadImageBtn = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPageCalc.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            this.drawingToolstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCalc);
            this.tabControl1.Controls.Add(this.tabPageText);
            this.tabControl1.Controls.Add(this.tabPageImage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageCalc
            // 
            this.tabPageCalc.Controls.Add(this.resultBox);
            this.tabPageCalc.Controls.Add(this.flowLayoutPanel1);
            this.tabPageCalc.Location = new System.Drawing.Point(4, 29);
            this.tabPageCalc.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageCalc.Name = "tabPageCalc";
            this.tabPageCalc.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageCalc.Size = new System.Drawing.Size(792, 417);
            this.tabPageCalc.TabIndex = 0;
            this.tabPageCalc.Text = "Calculator";
            this.tabPageCalc.UseVisualStyleBackColor = true;
            // 
            // resultBox
            // 
            this.resultBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultBox.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultBox.Location = new System.Drawing.Point(11, 27);
            this.resultBox.Margin = new System.Windows.Forms.Padding(4);
            this.resultBox.Name = "resultBox";
            this.resultBox.ReadOnly = true;
            this.resultBox.Size = new System.Drawing.Size(772, 71);
            this.resultBox.TabIndex = 1;
            this.resultBox.TabStop = false;
            this.resultBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 129);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(772, 279);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.statusStrip1);
            this.tabPageText.Controls.Add(this.toolStrip1);
            this.tabPageText.Controls.Add(this.editorTextBox);
            this.tabPageText.Location = new System.Drawing.Point(4, 29);
            this.tabPageText.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Size = new System.Drawing.Size(792, 417);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "Text Editor";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textEditorStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 395);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textEditorStatus
            // 
            this.textEditorStatus.Name = "textEditorStatus";
            this.textEditorStatus.Size = new System.Drawing.Size(118, 17);
            this.textEditorStatus.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 0);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // editorTextBox
            // 
            this.editorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorTextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.editorTextBox.Location = new System.Drawing.Point(9, 31);
            this.editorTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.editorTextBox.Name = "editorTextBox";
            this.editorTextBox.Size = new System.Drawing.Size(774, 344);
            this.editorTextBox.TabIndex = 0;
            this.editorTextBox.Text = "";
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.drawingToolstrip);
            this.tabPageImage.Controls.Add(this.drawingCanvas);
            this.tabPageImage.Location = new System.Drawing.Point(4, 29);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Size = new System.Drawing.Size(792, 417);
            this.tabPageImage.TabIndex = 2;
            this.tabPageImage.Text = "Image Editor";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // drawingToolstrip
            // 
            this.drawingToolstrip.AllowMerge = false;
            this.drawingToolstrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawingToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.drawingToolstrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.drawingToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawCurveBtn,
            this.drawCircleBtn,
            this.drawRectangleBtn,
            this.drawTriangleBtn,
            this.toolStripSeparator1,
            this.loadImageBtn,
            this.saveImageAsBtn});
            this.drawingToolstrip.Location = new System.Drawing.Point(0, 0);
            this.drawingToolstrip.Name = "drawingToolstrip";
            this.drawingToolstrip.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.drawingToolstrip.Size = new System.Drawing.Size(792, 37);
            this.drawingToolstrip.TabIndex = 1;
            this.drawingToolstrip.Text = "toolStrip2";
            // 
            // drawCurveBtn
            // 
            this.drawCurveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawCurveBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawCurveBtn.Image = ((System.Drawing.Image)(resources.GetObject("drawCurveBtn.Image")));
            this.drawCurveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawCurveBtn.Name = "drawCurveBtn";
            this.drawCurveBtn.Size = new System.Drawing.Size(24, 24);
            this.drawCurveBtn.Text = "Curve";
            // 
            // drawCircleBtn
            // 
            this.drawCircleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawCircleBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawCircleBtn.Image = ((System.Drawing.Image)(resources.GetObject("drawCircleBtn.Image")));
            this.drawCircleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawCircleBtn.Name = "drawCircleBtn";
            this.drawCircleBtn.Size = new System.Drawing.Size(24, 24);
            this.drawCircleBtn.Text = "Circle";
            // 
            // drawRectangleBtn
            // 
            this.drawRectangleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawRectangleBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawRectangleBtn.Image = ((System.Drawing.Image)(resources.GetObject("drawRectangleBtn.Image")));
            this.drawRectangleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawRectangleBtn.Name = "drawRectangleBtn";
            this.drawRectangleBtn.Size = new System.Drawing.Size(24, 24);
            this.drawRectangleBtn.Text = "Rectangle";
            // 
            // drawTriangleBtn
            // 
            this.drawTriangleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawTriangleBtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.drawTriangleBtn.Image = ((System.Drawing.Image)(resources.GetObject("drawTriangleBtn.Image")));
            this.drawTriangleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawTriangleBtn.Name = "drawTriangleBtn";
            this.drawTriangleBtn.Size = new System.Drawing.Size(24, 24);
            this.drawTriangleBtn.Text = "Triangle";
            // 
            // drawingCanvas
            // 
            this.drawingCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawingCanvas.Location = new System.Drawing.Point(8, 38);
            this.drawingCanvas.Name = "drawingCanvas";
            this.drawingCanvas.Size = new System.Drawing.Size(776, 371);
            this.drawingCanvas.TabIndex = 0;
            this.drawingCanvas.TabStop = false;
            // 
            // saveImageAsBtn
            // 
            this.saveImageAsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveImageAsBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveImageAsBtn.Image")));
            this.saveImageAsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveImageAsBtn.Name = "saveImageAsBtn";
            this.saveImageAsBtn.Size = new System.Drawing.Size(24, 24);
            this.saveImageAsBtn.Text = "Save Image";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // loadImageBtn
            // 
            this.loadImageBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadImageBtn.Image = ((System.Drawing.Image)(resources.GetObject("loadImageBtn.Image")));
            this.loadImageBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadImageBtn.Name = "loadImageBtn";
            this.loadImageBtn.Size = new System.Drawing.Size(24, 24);
            this.loadImageBtn.Text = "Load Image";
            // 
            // Playground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.Name = "Playground";
            this.Text = "Playground";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Playground_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCalc.ResumeLayout(false);
            this.tabPageCalc.PerformLayout();
            this.tabPageText.ResumeLayout(false);
            this.tabPageText.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPageImage.ResumeLayout(false);
            this.tabPageImage.PerformLayout();
            this.drawingToolstrip.ResumeLayout(false);
            this.drawingToolstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCalc;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox resultBox;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.RichTextBox editorTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel textEditorStatus;
        private System.Windows.Forms.PictureBox drawingCanvas;
        private System.Windows.Forms.ToolStrip drawingToolstrip;
        private System.Windows.Forms.ToolStripButton drawCurveBtn;
        private System.Windows.Forms.ToolStripButton drawCircleBtn;
        private System.Windows.Forms.ToolStripButton drawRectangleBtn;
        private System.Windows.Forms.ToolStripButton drawTriangleBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton loadImageBtn;
        private System.Windows.Forms.ToolStripButton saveImageAsBtn;
    }
}


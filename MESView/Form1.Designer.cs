namespace MES.View
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose (bool disposing)
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent ()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.evaluateBtn = new System.Windows.Forms.Button();
			this.methodBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.inputPlot = new OxyPlot.WindowsForms.PlotView();
			this.outputPlot = new OxyPlot.WindowsForms.PlotView();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(205, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(592, 445);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.inputPlot);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(584, 416);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Input";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.outputPlot);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(584, 416);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Output";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// evaluateBtn
			// 
			this.evaluateBtn.Location = new System.Drawing.Point(26, 405);
			this.evaluateBtn.Name = "evaluateBtn";
			this.evaluateBtn.Size = new System.Drawing.Size(75, 23);
			this.evaluateBtn.TabIndex = 1;
			this.evaluateBtn.Text = "Calculate";
			this.evaluateBtn.UseVisualStyleBackColor = true;
			// 
			// methodBox
			// 
			this.methodBox.FormattingEnabled = true;
			this.methodBox.Location = new System.Drawing.Point(26, 375);
			this.methodBox.Name = "methodBox";
			this.methodBox.Size = new System.Drawing.Size(121, 24);
			this.methodBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 355);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Method";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(47, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 36);
			this.button1.TabIndex = 4;
			this.button1.Text = "Load data";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// inputPlot
			// 
			this.inputPlot.Location = new System.Drawing.Point(17, 31);
			this.inputPlot.Name = "inputPlot";
			this.inputPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.inputPlot.Size = new System.Drawing.Size(550, 369);
			this.inputPlot.TabIndex = 0;
			this.inputPlot.Text = "plotView1";
			this.inputPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.inputPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.inputPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// outputPlot
			// 
			this.outputPlot.Location = new System.Drawing.Point(17, 24);
			this.outputPlot.Name = "outputPlot";
			this.outputPlot.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.outputPlot.Size = new System.Drawing.Size(550, 369);
			this.outputPlot.TabIndex = 1;
			this.outputPlot.Text = "plotView1";
			this.outputPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.outputPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.outputPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.methodBox);
			this.Controls.Add(this.evaluateBtn);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button evaluateBtn;
		private System.Windows.Forms.ComboBox methodBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button button1;
		private OxyPlot.WindowsForms.PlotView inputPlot;
		private OxyPlot.WindowsForms.PlotView outputPlot;
	}
}


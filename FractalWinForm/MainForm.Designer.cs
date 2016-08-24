namespace FractalWinForm
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.viewport = new System.Windows.Forms.PictureBox();
			this.timerMain = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.viewport)).BeginInit();
			this.SuspendLayout();
			// 
			// viewport
			// 
			this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewport.Location = new System.Drawing.Point(0, 0);
			this.viewport.Name = "viewport";
			this.viewport.Size = new System.Drawing.Size(657, 615);
			this.viewport.TabIndex = 0;
			this.viewport.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(165, 110);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 100);
			this.panel1.TabIndex = 1;
			this.panel1.Visible = false;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 615);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.viewport);
			this.Name = "FormMain";
			this.Text = "Fractal";
			((System.ComponentModel.ISupportInitialize)(this.viewport)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox viewport;
		private System.Windows.Forms.Timer timerMain;
		private System.Windows.Forms.Panel panel1;
	}
}


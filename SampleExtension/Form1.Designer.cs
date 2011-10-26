namespace SampleExtension
{
	partial class Form1
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.button1 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.buttonRaiseCallback = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(546, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 28);
			this.button1.TabIndex = 1;
			this.button1.Text = "Close";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(8, 8);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(532, 244);
			this.dataGridView1.TabIndex = 2;
			// 
			// buttonRaiseCallback
			// 
			this.buttonRaiseCallback.Location = new System.Drawing.Point(546, 46);
			this.buttonRaiseCallback.Name = "buttonRaiseCallback";
			this.buttonRaiseCallback.Size = new System.Drawing.Size(96, 28);
			this.buttonRaiseCallback.TabIndex = 3;
			this.buttonRaiseCallback.Text = "Raise Callback";
			this.buttonRaiseCallback.UseVisualStyleBackColor = true;
			this.buttonRaiseCallback.Click += new System.EventHandler(this.buttonRaiseCallback_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(647, 264);
			this.Controls.Add(this.buttonRaiseCallback);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button buttonRaiseCallback;
	}
}

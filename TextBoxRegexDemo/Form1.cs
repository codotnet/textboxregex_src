#region Copyright (c) 2004 Marek Grzenkowicz
/*
 * Copyright (c) 2004 Marek Grzenkowicz
 * 
 * This software is provided 'as-is', without any warranty.
 * 
 * Permission is granted to anyone to use this software for any purpose.
 * 
 * This notice may not be removed from any source distibution; if you are
 * using this software in a product, this notice should be included in
 * materials distributed with your product.
 */
#endregion

using System;
using System.Windows.Forms;

namespace TestBoxRegexDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button TextButton;
        private System.Windows.Forms.Button TextValidatedButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Chopeen.TextBoxRegex textBoxRegex1;
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            this.propertyGrid.SelectedObject = this.textBoxRegex1;
            this.propertyGrid.CollapseAllGridItems();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.TextButton = new System.Windows.Forms.Button();
			this.TextValidatedButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxRegex1 = new Chopeen.TextBoxRegex();
			this.SuspendLayout();
			// 
			// propertyGrid
			// 
			this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Location = new System.Drawing.Point(8, 104);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(388, 328);
			this.propertyGrid.TabIndex = 1;
			this.propertyGrid.Text = "propertyGrid";
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid_SelectedGridItemChanged);
			// 
			// TextButton
			// 
			this.TextButton.Location = new System.Drawing.Point(8, 40);
			this.TextButton.Name = "TextButton";
			this.TextButton.Size = new System.Drawing.Size(88, 24);
			this.TextButton.TabIndex = 3;
			this.TextButton.Text = "Text";
			this.TextButton.Click += new System.EventHandler(this.TextButton_Click);
			// 
			// TextValidatedButton
			// 
			this.TextValidatedButton.Location = new System.Drawing.Point(8, 72);
			this.TextValidatedButton.Name = "TextValidatedButton";
			this.TextValidatedButton.Size = new System.Drawing.Size(88, 24);
			this.TextValidatedButton.TabIndex = 4;
			this.TextValidatedButton.Text = "TextValidated";
			this.TextValidatedButton.Click += new System.EventHandler(this.TextValidatedButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(104, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(288, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "This button uses TextBoxRegex.Text property.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(104, 77);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "This button uses TextBoxRegex.TextValidated property.";
			// 
			// textBoxRegex1
			// 
			this.textBoxRegex1.BackColor = System.Drawing.Color.LightPink;
			this.textBoxRegex1.InvalidTextBackColor = System.Drawing.Color.LightPink;
			this.textBoxRegex1.Location = new System.Drawing.Point(8, 8);
			this.textBoxRegex1.Name = "textBoxRegex1";
			this.textBoxRegex1.Pattern = Chopeen.TextBoxRegex.Patterns.None;
			this.textBoxRegex1.PatternString = "";
			this.textBoxRegex1.Size = new System.Drawing.Size(136, 20);
			this.textBoxRegex1.TabIndex = 7;
			this.textBoxRegex1.Text = "";
			this.textBoxRegex1.UseColors = true;
			this.textBoxRegex1.UseInvalidTextException = true;
			this.textBoxRegex1.ValidTextBackColor = System.Drawing.Color.LightGreen;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 437);
			this.Controls.Add(this.textBoxRegex1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TextValidatedButton);
			this.Controls.Add(this.TextButton);
			this.Controls.Add(this.propertyGrid);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void propertyGrid_SelectedGridItemChanged(object sender, System.Windows.Forms.SelectedGridItemChangedEventArgs e)
        {
            textBoxRegex1.Refresh();
        }

        private void TextButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("This button uses Text property.\n\n" +
                "Its current value is: \"" + textBoxRegex1.Text + "\".",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void TextValidatedButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                MessageBox.Show("This button uses TextValidated property.\n\n" +
                    "Its current value is: \"" + textBoxRegex1.TextValidated + "\"" + (textBoxRegex1.UseInvalidTextException ? " and it matches chosen pattern." : "."),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Chopeen.InvalidTextException ex)
            {
                MessageBox.Show("This button uses TextValidated property.\n\n" + 
                    "An exception has been thrown. The exception message says:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
	}
}

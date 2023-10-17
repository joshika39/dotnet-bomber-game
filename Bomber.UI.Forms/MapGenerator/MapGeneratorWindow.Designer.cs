using System.ComponentModel;

namespace Bomber.UI.Forms.MapGenerator
{
    partial class MapGeneratorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.layoutPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.draftName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.draftComboBox = new System.Windows.Forms.ComboBox();
            this.heightValue = new System.Windows.Forms.NumericUpDown();
            this.widthValue = new System.Windows.Forms.NumericUpDown();
            this.draftButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.descBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).BeginInit();
            this.SuspendLayout();
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.Location = new System.Drawing.Point(258, 552);
            this.generateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(86, 31);
            this.generateButton.TabIndex = 0;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.OnGenerateClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Map Generator";
            // 
            // layoutPanel
            // 
            this.layoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutPanel.AutoSize = true;
            this.layoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutPanel.Location = new System.Drawing.Point(14, 76);
            this.layoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.Size = new System.Drawing.Size(0, 0);
            this.layoutPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.draftName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.draftComboBox);
            this.panel2.Controls.Add(this.heightValue);
            this.panel2.Controls.Add(this.widthValue);
            this.panel2.Controls.Add(this.draftButton);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.generateButton);
            this.panel2.Controls.Add(this.descBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(558, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 599);
            this.panel2.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(7, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Name";
            // 
            // draftName
            // 
            this.draftName.Location = new System.Drawing.Point(62, 143);
            this.draftName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.draftName.Name = "draftName";
            this.draftName.Size = new System.Drawing.Size(273, 27);
            this.draftName.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(45, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Pending drafts";
            // 
            // draftComboBox
            // 
            this.draftComboBox.FormattingEnabled = true;
            this.draftComboBox.Location = new System.Drawing.Point(167, 21);
            this.draftComboBox.Name = "draftComboBox";
            this.draftComboBox.Size = new System.Drawing.Size(151, 28);
            this.draftComboBox.TabIndex = 11;
            this.draftComboBox.SelectedValueChanged += new System.EventHandler(this.OnSelectionChanged);
            // 
            // heightValue
            // 
            this.heightValue.Location = new System.Drawing.Point(62, 219);
            this.heightValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.heightValue.Name = "heightValue";
            this.heightValue.Size = new System.Drawing.Size(273, 27);
            this.heightValue.TabIndex = 10;
            this.heightValue.ValueChanged += new System.EventHandler(this.OnHeightChanged);
            // 
            // widthValue
            // 
            this.widthValue.Location = new System.Drawing.Point(62, 181);
            this.widthValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.widthValue.Name = "widthValue";
            this.widthValue.Size = new System.Drawing.Size(273, 27);
            this.widthValue.TabIndex = 9;
            this.widthValue.ValueChanged += new System.EventHandler(this.OnWidthChanged);
            // 
            // draftButton
            // 
            this.draftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.draftButton.Location = new System.Drawing.Point(9, 552);
            this.draftButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.draftButton.Name = "draftButton";
            this.draftButton.Size = new System.Drawing.Size(109, 31);
            this.draftButton.TabIndex = 8;
            this.draftButton.Text = "Save as Draft";
            this.draftButton.UseVisualStyleBackColor = true;
            this.draftButton.Click += new System.EventHandler(this.OnSaveAsDraftClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButton.Location = new System.Drawing.Point(167, 552);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 31);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(10, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Map description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(5, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(8, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width";
            // 
            // descBox
            // 
            this.descBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.descBox.Location = new System.Drawing.Point(9, 309);
            this.descBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(326, 211);
            this.descBox.TabIndex = 3;
            this.descBox.Text = "";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(8, 75);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.label2.Size = new System.Drawing.Size(336, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "Properties";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 15;
            this.button1.Text = "New";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnNewClicked);
            // 
            // MapGeneratorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.layoutPanel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MapGeneratorWindow";
            this.Text = "MapGeneratorWindow";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button generateButton;
        private Label label1;
        private Panel layoutPanel;
        private Panel panel2;
        private Label label2;
        private BackgroundWorker backgroundWorker1;
        private Label label5;
        private Label label4;
        private Label label3;
        private RichTextBox descBox;
        private Button cancelButton;
        private Button draftButton;
        private NumericUpDown heightValue;
        private NumericUpDown widthValue;
        private Label label6;
        private ComboBox draftComboBox;
        private Label label7;
        private TextBox draftName;
        private Button button1;
    }
}


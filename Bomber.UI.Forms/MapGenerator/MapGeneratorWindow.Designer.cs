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
            generateButton = new Button();
            label1 = new Label();
            layoutPanel = new Panel();
            panel2 = new Panel();
            editEntityButton = new Button();
            label12 = new Label();
            label11 = new Label();
            entityY = new NumericUpDown();
            entityX = new NumericUpDown();
            button2 = new Button();
            entityList = new ListBox();
            playerY = new NumericUpDown();
            playerX = new NumericUpDown();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            button1 = new Button();
            label7 = new Label();
            draftName = new TextBox();
            label6 = new Label();
            draftComboBox = new ComboBox();
            heightValue = new NumericUpDown();
            widthValue = new NumericUpDown();
            draftButton = new Button();
            cancelButton = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            descBox = new RichTextBox();
            label2 = new Label();
            backgroundWorker1 = new BackgroundWorker();
            panel2.SuspendLayout();
            ((ISupportInitialize)entityY).BeginInit();
            ((ISupportInitialize)entityX).BeginInit();
            ((ISupportInitialize)playerY).BeginInit();
            ((ISupportInitialize)playerX).BeginInit();
            ((ISupportInitialize)heightValue).BeginInit();
            ((ISupportInitialize)widthValue).BeginInit();
            SuspendLayout();
            // 
            // generateButton
            // 
            generateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            generateButton.Location = new Point(226, 1025);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 0;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += OnGenerateClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(235, 45);
            label1.TabIndex = 1;
            label1.Text = "Map Generator";
            // 
            // layoutPanel
            // 
            layoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            layoutPanel.AutoSize = true;
            layoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutPanel.Location = new Point(12, 57);
            layoutPanel.Name = "layoutPanel";
            layoutPanel.Size = new Size(0, 0);
            layoutPanel.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(editEntityButton);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(entityY);
            panel2.Controls.Add(entityX);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(entityList);
            panel2.Controls.Add(playerY);
            panel2.Controls.Add(playerX);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(draftName);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(draftComboBox);
            panel2.Controls.Add(heightValue);
            panel2.Controls.Add(widthValue);
            panel2.Controls.Add(draftButton);
            panel2.Controls.Add(cancelButton);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(generateButton);
            panel2.Controls.Add(descBox);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(672, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(312, 1061);
            panel2.TabIndex = 3;
            // 
            // editEntityButton
            // 
            editEntityButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editEntityButton.Enabled = false;
            editEntityButton.Location = new Point(230, 741);
            editEntityButton.Name = "editEntityButton";
            editEntityButton.Size = new Size(60, 23);
            editEntityButton.TabIndex = 28;
            editEntityButton.Text = "Edit";
            editEntityButton.UseVisualStyleBackColor = true;
            editEntityButton.Click += OnEditEntityClick;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(84, 745);
            label12.Name = "label12";
            label12.Size = new Size(14, 15);
            label12.TabIndex = 27;
            label12.Text = "Y";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(11, 745);
            label11.Name = "label11";
            label11.Size = new Size(15, 15);
            label11.TabIndex = 26;
            label11.Text = "X";
            // 
            // entityY
            // 
            entityY.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entityY.Location = new Point(102, 741);
            entityY.Name = "entityY";
            entityY.Size = new Size(45, 23);
            entityY.TabIndex = 25;
            // 
            // entityX
            // 
            entityX.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entityX.Location = new Point(29, 741);
            entityX.Name = "entityX";
            entityX.Size = new Size(44, 23);
            entityX.TabIndex = 24;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(155, 741);
            button2.Name = "button2";
            button2.Size = new Size(65, 23);
            button2.TabIndex = 23;
            button2.Text = "Add";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OnAddEnemyClick;
            // 
            // entityList
            // 
            entityList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entityList.FormattingEnabled = true;
            entityList.ItemHeight = 15;
            entityList.Location = new Point(9, 487);
            entityList.Name = "entityList";
            entityList.Size = new Size(284, 244);
            entityList.TabIndex = 22;
            entityList.SelectedValueChanged += OnSelectedEntityChanged;
            // 
            // playerY
            // 
            playerY.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playerY.Location = new Point(162, 225);
            playerY.Name = "playerY";
            playerY.Size = new Size(131, 23);
            playerY.TabIndex = 21;
            // 
            // playerX
            // 
            playerX.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playerX.Location = new Point(163, 195);
            playerX.Name = "playerX";
            playerX.Size = new Size(130, 23);
            playerX.TabIndex = 20;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(9, 229);
            label10.Name = "label10";
            label10.Size = new Size(147, 15);
            label10.TabIndex = 19;
            label10.Text = "Player initial Y coordinate";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(9, 200);
            label9.Name = "label9";
            label9.Size = new Size(148, 15);
            label9.TabIndex = 18;
            label9.Text = "Player initial X coordinate";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(9, 466);
            label8.Name = "label8";
            label8.Size = new Size(48, 15);
            label8.TabIndex = 17;
            label8.Text = "Entities";
            // 
            // button1
            // 
            button1.Location = new Point(21, 63);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(82, 22);
            button1.TabIndex = 15;
            button1.Text = "New";
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += OnNewClicked;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(6, 112);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 14;
            label7.Text = "Name";
            // 
            // draftName
            // 
            draftName.Location = new Point(54, 107);
            draftName.Name = "draftName";
            draftName.Size = new Size(239, 23);
            draftName.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(39, 20);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 12;
            label6.Text = "Pending drafts";
            // 
            // draftComboBox
            // 
            draftComboBox.FormattingEnabled = true;
            draftComboBox.Location = new Point(146, 16);
            draftComboBox.Margin = new Padding(3, 2, 3, 2);
            draftComboBox.Name = "draftComboBox";
            draftComboBox.Size = new Size(133, 23);
            draftComboBox.TabIndex = 11;
            draftComboBox.SelectedValueChanged += OnSelectionChanged;
            // 
            // heightValue
            // 
            heightValue.Location = new Point(54, 164);
            heightValue.Name = "heightValue";
            heightValue.Size = new Size(239, 23);
            heightValue.TabIndex = 10;
            heightValue.ValueChanged += OnHeightChanged;
            // 
            // widthValue
            // 
            widthValue.Location = new Point(54, 136);
            widthValue.Name = "widthValue";
            widthValue.Size = new Size(239, 23);
            widthValue.TabIndex = 9;
            widthValue.ValueChanged += OnWidthChanged;
            // 
            // draftButton
            // 
            draftButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            draftButton.Location = new Point(8, 1025);
            draftButton.Name = "draftButton";
            draftButton.Size = new Size(95, 23);
            draftButton.TabIndex = 8;
            draftButton.Text = "Save as Draft";
            draftButton.UseVisualStyleBackColor = true;
            draftButton.Click += OnSaveAsDraftClicked;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom;
            cancelButton.Location = new Point(146, 1025);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(9, 258);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 6;
            label5.Text = "Map description";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(4, 167);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 5;
            label4.Text = "Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(7, 140);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Width";
            // 
            // descBox
            // 
            descBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            descBox.Location = new Point(8, 281);
            descBox.Name = "descBox";
            descBox.Size = new Size(286, 159);
            descBox.TabIndex = 3;
            descBox.Text = "";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            label2.Location = new Point(7, 56);
            label2.Name = "label2";
            label2.Padding = new Padding(5);
            label2.Size = new Size(294, 35);
            label2.TabIndex = 0;
            label2.Text = "Properties";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MapGeneratorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 1061);
            Controls.Add(panel2);
            Controls.Add(layoutPanel);
            Controls.Add(label1);
            Name = "MapGeneratorWindow";
            Text = "MapGeneratorWindow";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)entityY).EndInit();
            ((ISupportInitialize)entityX).EndInit();
            ((ISupportInitialize)playerY).EndInit();
            ((ISupportInitialize)playerX).EndInit();
            ((ISupportInitialize)heightValue).EndInit();
            ((ISupportInitialize)widthValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private NumericUpDown playerY;
        private NumericUpDown playerX;
        private Label label10;
        private Label label9;
        private Label label8;
        private Button button2;
        private ListBox entityList;
        private NumericUpDown entityX;
        private Label label12;
        private Label label11;
        private NumericUpDown entityY;
        private Button editEntityButton;
    }
}


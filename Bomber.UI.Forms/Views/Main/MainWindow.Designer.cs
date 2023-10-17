using System.ComponentModel;

namespace Bomber.UI.Forms.Views.Main
{
    partial class MainWindow
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
            label1 = new Label();
            bomberMap = new Panel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            recentMapsToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            openMapGeneratorToolStripMenuItem = new ToolStripMenuItem();
            infoToolStripMenuItem = new ToolStripMenuItem();
            websiteToolStripMenuItem = new ToolStripMenuItem();
            mapName = new Label();
            description = new Label();
            elementList = new Panel();
            button1 = new Button();
            button2 = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(591, 37);
            label1.Name = "label1";
            label1.Size = new Size(197, 41);
            label1.TabIndex = 0;
            label1.Text = "Bomber.UI v1";
            // 
            // bomberMap
            // 
            bomberMap.AutoSize = true;
            bomberMap.Location = new Point(11, 89);
            bomberMap.Name = "bomberMap";
            bomberMap.Size = new Size(350, 350);
            bomberMap.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ScrollBar;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, infoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, quitToolStripMenuItem, recentMapsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(142, 22);
            openToolStripMenuItem.Text = "Open...";
            openToolStripMenuItem.Click += OnOpenMap;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(142, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(142, 22);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // recentMapsToolStripMenuItem
            // 
            recentMapsToolStripMenuItem.Name = "recentMapsToolStripMenuItem";
            recentMapsToolStripMenuItem.Size = new Size(142, 22);
            recentMapsToolStripMenuItem.Text = "Recent Maps";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openMapGeneratorToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // openMapGeneratorToolStripMenuItem
            // 
            openMapGeneratorToolStripMenuItem.Name = "openMapGeneratorToolStripMenuItem";
            openMapGeneratorToolStripMenuItem.Size = new Size(182, 22);
            openMapGeneratorToolStripMenuItem.Text = "Open MapGenerator";
            openMapGeneratorToolStripMenuItem.Click += openMapGeneratorToolStripMenuItem_Click;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { websiteToolStripMenuItem });
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(40, 20);
            infoToolStripMenuItem.Text = "Info";
            // 
            // websiteToolStripMenuItem
            // 
            websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            websiteToolStripMenuItem.Size = new Size(116, 22);
            websiteToolStripMenuItem.Text = "Website";
            // 
            // mapName
            // 
            mapName.AutoSize = true;
            mapName.Location = new Point(12, 37);
            mapName.Name = "mapName";
            mapName.Size = new Size(74, 15);
            mapName.TabIndex = 3;
            mapName.Text = "Select a Map";
            // 
            // description
            // 
            description.AutoSize = true;
            description.Location = new Point(93, 37);
            description.Name = "description";
            description.Size = new Size(0, 15);
            description.TabIndex = 4;
            // 
            // elementList
            // 
            elementList.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            elementList.Location = new Point(643, 192);
            elementList.Name = "elementList";
            elementList.Size = new Size(145, 247);
            elementList.TabIndex = 5;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.Location = new Point(713, 89);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OnTestClick;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Right;
            button2.Location = new Point(713, 118);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "Stop Test";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OnStopTestClick;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(elementList);
            Controls.Add(description);
            Controls.Add(mapName);
            Controls.Add(bomberMap);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "MainWindow";
            KeyDown += OnKeyPressed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel bomberMap;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem openMapGeneratorToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem websiteToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem recentMapsToolStripMenuItem;
        private Label mapName;
        private Label description;
        private Panel elementList;
        private Button button1;
        private Button button2;
    }
}


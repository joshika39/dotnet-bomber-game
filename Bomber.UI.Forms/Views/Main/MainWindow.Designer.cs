using System.ComponentModel;

namespace Bomber.UI.Forms.Views.Main
{
    sealed partial class MainWindow
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
            currentTime = new Label();
            mapDescription = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // bomberMap
            // 
            bomberMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            bomberMap.Location = new Point(12, 37);
            bomberMap.Name = "bomberMap";
            bomberMap.Size = new Size(581, 433);
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
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open...";
            openToolStripMenuItem.Click += OnOpenMap;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += OnSaveClicked;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(180, 22);
            quitToolStripMenuItem.Text = "Quit";
            // 
            // recentMapsToolStripMenuItem
            // 
            recentMapsToolStripMenuItem.Name = "recentMapsToolStripMenuItem";
            recentMapsToolStripMenuItem.Size = new Size(180, 22);
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
            mapName.Location = new Point(599, 37);
            mapName.Name = "mapName";
            mapName.Size = new Size(0, 15);
            mapName.TabIndex = 3;
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
            elementList.Location = new Point(643, 239);
            elementList.Name = "elementList";
            elementList.Size = new Size(145, 247);
            elementList.TabIndex = 5;
            // 
            // currentTime
            // 
            currentTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            currentTime.AutoSize = true;
            currentTime.Location = new Point(12, 473);
            currentTime.Name = "currentTime";
            currentTime.Size = new Size(63, 15);
            currentTime.TabIndex = 8;
            currentTime.Text = "Stopwatch";
            // 
            // mapDescription
            // 
            mapDescription.AutoSize = true;
            mapDescription.Location = new Point(599, 64);
            mapDescription.Name = "mapDescription";
            mapDescription.Size = new Size(0, 15);
            mapDescription.TabIndex = 9;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(800, 497);
            Controls.Add(mapDescription);
            Controls.Add(currentTime);
            Controls.Add(elementList);
            Controls.Add(description);
            Controls.Add(mapName);
            Controls.Add(bomberMap);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "MainWindow";
            KeyPress += OnKeyPressed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private Label currentTime;
        private Label mapDescription;
    }
}


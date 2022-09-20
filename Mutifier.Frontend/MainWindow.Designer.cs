namespace Mutifier.Frontend
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.muteMic = new System.Windows.Forms.Button();
            this.enableMic = new System.Windows.Forms.Button();
            this.mutedBeep = new System.Windows.Forms.Timer(this.components);
            this.keybindLabel = new System.Windows.Forms.Label();
            this.changeKeybind = new System.Windows.Forms.Button();
            this.authorLabel = new System.Windows.Forms.LinkLabel();
            this.beepCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // muteMic
            // 
            this.muteMic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.muteMic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.muteMic.ForeColor = System.Drawing.Color.White;
            this.muteMic.Location = new System.Drawing.Point(119, 25);
            this.muteMic.Name = "muteMic";
            this.muteMic.Size = new System.Drawing.Size(75, 23);
            this.muteMic.TabIndex = 0;
            this.muteMic.Text = "Mute";
            this.muteMic.UseVisualStyleBackColor = false;
            //this.muteMic.Click += new System.EventHandler(this.toggleMicButtons);
            this.muteMic.Click += (_, _) => this.ToggleMicrophone();
            // 
            // enableMic
            // 
            this.enableMic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.enableMic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enableMic.ForeColor = System.Drawing.Color.White;
            this.enableMic.Location = new System.Drawing.Point(214, 25);
            this.enableMic.Name = "enableMic";
            this.enableMic.Size = new System.Drawing.Size(75, 23);
            this.enableMic.TabIndex = 1;
            this.enableMic.Text = "Unmute";
            this.enableMic.UseVisualStyleBackColor = false;
            this.enableMic.Click += (_, _) => this.ToggleMicrophone();
            //this.enableMic.Click += new System.EventHandler(this.toggleMicButtons);
            // 
            // mutedBeep
            // 
            this.mutedBeep.Interval = 4000;
            this.mutedBeep.Tick += new System.EventHandler(this.mutedBeep_Tick);
            // 
            // keybindLabel
            // 
            this.keybindLabel.AutoSize = true;
            this.keybindLabel.BackColor = System.Drawing.Color.Transparent;
            this.keybindLabel.ForeColor = System.Drawing.Color.White;
            this.keybindLabel.Location = new System.Drawing.Point(198, 66);
            this.keybindLabel.Name = "keybindLabel";
            this.keybindLabel.Size = new System.Drawing.Size(91, 15);
            this.keybindLabel.TabIndex = 2;
            this.keybindLabel.Text = "Keybind: HOME";
            // 
            // changeKeybind
            // 
            this.changeKeybind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.changeKeybind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeKeybind.ForeColor = System.Drawing.Color.White;
            this.changeKeybind.Location = new System.Drawing.Point(119, 62);
            this.changeKeybind.Name = "changeKeybind";
            this.changeKeybind.Size = new System.Drawing.Size(75, 23);
            this.changeKeybind.TabIndex = 3;
            this.changeKeybind.Text = "Change";
            this.changeKeybind.UseVisualStyleBackColor = false;
            this.changeKeybind.Click += new System.EventHandler(this.changeKeybind_Click);
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.DisabledLinkColor = System.Drawing.Color.White;
            this.authorLabel.ForeColor = System.Drawing.Color.White;
            this.authorLabel.LinkArea = new System.Windows.Forms.LinkArea(15, 6);
            this.authorLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.authorLabel.LinkColor = System.Drawing.Color.DodgerBlue;
            this.authorLabel.Location = new System.Drawing.Point(135, 140);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(136, 21);
            this.authorLabel.TabIndex = 4;
            this.authorLabel.TabStop = true;
            this.authorLabel.Text = "Made with ❤ by TorniX.";
            this.authorLabel.UseCompatibleTextRendering = true;
            this.authorLabel.VisitedLinkColor = System.Drawing.Color.DodgerBlue;
            this.authorLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.authorLabel_LinkClicked);
            // 
            // beepCheckBox
            // 
            this.beepCheckBox.AutoSize = true;
            this.beepCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.beepCheckBox.Checked = true;
            this.beepCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.beepCheckBox.ForeColor = System.Drawing.Color.White;
            this.beepCheckBox.Location = new System.Drawing.Point(126, 100);
            this.beepCheckBox.Name = "beepCheckBox";
            this.beepCheckBox.Size = new System.Drawing.Size(157, 19);
            this.beepCheckBox.TabIndex = 5;
            this.beepCheckBox.Text = "Toggle beeping indicator";
            this.beepCheckBox.UseVisualStyleBackColor = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(409, 178);
            this.Controls.Add(this.beepCheckBox);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.changeKeybind);
            this.Controls.Add(this.keybindLabel);
            this.Controls.Add(this.enableMic);
            this.Controls.Add(this.muteMic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Mutifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button muteMic;
        private Button enableMic;
        private System.Windows.Forms.Timer mutedBeep;
        private Label keybindLabel;
        private Button changeKeybind;
        private LinkLabel authorLabel;
        private CheckBox beepCheckBox;
    }
}
namespace TF2_ServerManager
{
    partial class NewServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewServer));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_FilePath = new System.Windows.Forms.TextBox();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_Launch = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.textBox_IPAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox_useLocalP = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Server File Path :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Launch Options :";
            // 
            // textBox_FilePath
            // 
            this.textBox_FilePath.Location = new System.Drawing.Point(114, 37);
            this.textBox_FilePath.Name = "textBox_FilePath";
            this.textBox_FilePath.Size = new System.Drawing.Size(325, 21);
            this.textBox_FilePath.TabIndex = 4;
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(445, 37);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(75, 23);
            this.button_Browse.TabIndex = 5;
            this.button_Browse.Text = "Browse";
            this.button_Browse.UseVisualStyleBackColor = true;
            this.button_Browse.Click += new System.EventHandler(this.button_Browse_Click);
            // 
            // textBox_Launch
            // 
            this.textBox_Launch.Location = new System.Drawing.Point(114, 64);
            this.textBox_Launch.Multiline = true;
            this.textBox_Launch.Name = "textBox_Launch";
            this.textBox_Launch.Size = new System.Drawing.Size(406, 84);
            this.textBox_Launch.TabIndex = 6;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(114, 154);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(121, 38);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(318, 154);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(121, 38);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port :";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(318, 10);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(47, 21);
            this.textBox_Port.TabIndex = 10;
            this.textBox_Port.Text = "27015";
            // 
            // textBox_IPAddress
            // 
            this.textBox_IPAddress.Enabled = false;
            this.textBox_IPAddress.Location = new System.Drawing.Point(114, 10);
            this.textBox_IPAddress.Name = "textBox_IPAddress";
            this.textBox_IPAddress.Size = new System.Drawing.Size(157, 21);
            this.textBox_IPAddress.TabIndex = 11;
            this.textBox_IPAddress.Text = "192.168.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "IP Address :";
            // 
            // checkBox_useLocalP
            // 
            this.checkBox_useLocalP.AutoSize = true;
            this.checkBox_useLocalP.Checked = true;
            this.checkBox_useLocalP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_useLocalP.Location = new System.Drawing.Point(399, 12);
            this.checkBox_useLocalP.Name = "checkBox_useLocalP";
            this.checkBox_useLocalP.Size = new System.Drawing.Size(121, 16);
            this.checkBox_useLocalP.TabIndex = 13;
            this.checkBox_useLocalP.Text = "Manage Process";
            this.checkBox_useLocalP.UseVisualStyleBackColor = true;
            this.checkBox_useLocalP.CheckedChanged += new System.EventHandler(this.CheckBox_useLocalP_CheckedChanged);
            // 
            // NewServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 199);
            this.Controls.Add(this.checkBox_useLocalP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_IPAddress);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_Launch);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.textBox_FilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewServer";
            this.Text = "New Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox_FilePath;
        public System.Windows.Forms.TextBox textBox_Launch;
        public System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox_IPAddress;
        public System.Windows.Forms.CheckBox checkBox_useLocalP;
    }
}
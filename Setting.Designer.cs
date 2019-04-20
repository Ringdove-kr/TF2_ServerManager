namespace TF2_ServerManager
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_Interval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Process = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Network = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Process)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Network)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check Interval (sec) :";
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(37, 99);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(133, 99);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Process check times :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Network check times :";
            // 
            // numericUpDown_Interval
            // 
            this.numericUpDown_Interval.Location = new System.Drawing.Point(170, 14);
            this.numericUpDown_Interval.Name = "numericUpDown_Interval";
            this.numericUpDown_Interval.Size = new System.Drawing.Size(38, 21);
            this.numericUpDown_Interval.TabIndex = 6;
            this.numericUpDown_Interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Interval.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numericUpDown_Process
            // 
            this.numericUpDown_Process.Location = new System.Drawing.Point(170, 41);
            this.numericUpDown_Process.Name = "numericUpDown_Process";
            this.numericUpDown_Process.Size = new System.Drawing.Size(38, 21);
            this.numericUpDown_Process.TabIndex = 7;
            this.numericUpDown_Process.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Process.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numericUpDown_Network
            // 
            this.numericUpDown_Network.Location = new System.Drawing.Point(170, 66);
            this.numericUpDown_Network.Name = "numericUpDown_Network";
            this.numericUpDown_Network.Size = new System.Drawing.Size(38, 21);
            this.numericUpDown_Network.TabIndex = 8;
            this.numericUpDown_Network.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_Network.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 132);
            this.Controls.Add(this.numericUpDown_Network);
            this.Controls.Add(this.numericUpDown_Process);
            this.Controls.Add(this.numericUpDown_Interval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setting";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Process)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Network)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDown_Interval;
        public System.Windows.Forms.NumericUpDown numericUpDown_Process;
        public System.Windows.Forms.NumericUpDown numericUpDown_Network;
    }
}
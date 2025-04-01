namespace mouse_cursor
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AOI1 = new System.Windows.Forms.Panel();
            this.AOI4 = new System.Windows.Forms.Panel();
            this.AOI3 = new System.Windows.Forms.Panel();
            this.AOI2 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnheat = new System.Windows.Forms.Button();
            this.lsa_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(36, 39);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(119, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(194, 39);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(112, 45);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(345, 39);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(111, 45);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1080, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // AOI1
            // 
            this.AOI1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AOI1.Location = new System.Drawing.Point(36, 111);
            this.AOI1.Name = "AOI1";
            this.AOI1.Size = new System.Drawing.Size(789, 183);
            this.AOI1.TabIndex = 5;
            // 
            // AOI4
            // 
            this.AOI4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AOI4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AOI4.Location = new System.Drawing.Point(865, 111);
            this.AOI4.Name = "AOI4";
            this.AOI4.Size = new System.Drawing.Size(268, 554);
            this.AOI4.TabIndex = 6;
            // 
            // AOI3
            // 
            this.AOI3.AllowDrop = true;
            this.AOI3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AOI3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AOI3.Location = new System.Drawing.Point(36, 475);
            this.AOI3.Name = "AOI3";
            this.AOI3.Size = new System.Drawing.Size(789, 190);
            this.AOI3.TabIndex = 7;
            // 
            // AOI2
            // 
            this.AOI2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AOI2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AOI2.Location = new System.Drawing.Point(36, 316);
            this.AOI2.Name = "AOI2";
            this.AOI2.Size = new System.Drawing.Size(789, 140);
            this.AOI2.TabIndex = 7;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(497, 39);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(151, 45);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export to csv";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnheat
            // 
            this.btnheat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnheat.Location = new System.Drawing.Point(686, 39);
            this.btnheat.Name = "btnheat";
            this.btnheat.Size = new System.Drawing.Size(177, 45);
            this.btnheat.TabIndex = 8;
            this.btnheat.Text = "Create heat map";
            this.btnheat.UseVisualStyleBackColor = true;
            this.btnheat.Click += new System.EventHandler(this.btnheat_Click);
            // 
            // lsa_btn
            // 
            this.lsa_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsa_btn.Location = new System.Drawing.Point(897, 39);
            this.lsa_btn.Name = "lsa_btn";
            this.lsa_btn.Size = new System.Drawing.Size(110, 45);
            this.lsa_btn.TabIndex = 9;
            this.lsa_btn.Text = "LSA";
            this.lsa_btn.UseVisualStyleBackColor = true;
            this.lsa_btn.Click += new System.EventHandler(this.lsa_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 689);
            this.Controls.Add(this.lsa_btn);
            this.Controls.Add(this.btnheat);
            this.Controls.Add(this.AOI4);
            this.Controls.Add(this.AOI2);
            this.Controls.Add(this.AOI3);
            this.Controls.Add(this.AOI1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel AOI1;
        private System.Windows.Forms.Panel AOI4;
        private System.Windows.Forms.Panel AOI3;
        private System.Windows.Forms.Panel AOI2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnheat;
        private System.Windows.Forms.Button lsa_btn;
    }
}


//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;

//namespace WindowsFormsApplication1
//{
//    partial class ProgressForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            ComponentResourceManager resources = new ComponentResourceManager(typeof(ProgressForm));
//            this.backgroundWorker1 = new BackgroundWorker();
//            this.splitContainer1 = new SplitContainer();
//            this.splitContainer2 = new SplitContainer();
//            this.cbTitles = new ComboBox();
//            this.lblMediaTitle = new Label();
//            this.pictureBox1 = new PictureBox();
//            this.label2 = new Label();
//            this.progressBar1 = new ProgressBar();
//            this.backgroundWorker2 = new BackgroundWorker();
//            ((ISupportInitialize)(this.splitContainer1)).BeginInit();
//            this.splitContainer1.Panel1.SuspendLayout();
//            this.splitContainer1.Panel2.SuspendLayout();
//            this.splitContainer1.SuspendLayout();
//            ((ISupportInitialize)(this.splitContainer2)).BeginInit();
//            this.splitContainer2.Panel1.SuspendLayout();
//            this.splitContainer2.Panel2.SuspendLayout();
//            this.splitContainer2.SuspendLayout();
//            ((ISupportInitialize)(this.pictureBox1)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // backgroundWorker1
//            // 
//            this.backgroundWorker1.WorkerReportsProgress = true;
//            // 
//            // splitContainer1
//            // 
//            this.splitContainer1.Dock = DockStyle.Fill;
//            this.splitContainer1.FixedPanel = FixedPanel.Panel2;
//            this.splitContainer1.Location = new Point(0, 0);
//            this.splitContainer1.Name = "splitContainer1";
//            this.splitContainer1.Orientation = Orientation.Horizontal;
//            // 
//            // splitContainer1.Panel1
//            // 
//            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
//            // 
//            // splitContainer1.Panel2
//            // 
//            this.splitContainer1.Panel2.BackColor = Color.Black;
//            this.splitContainer1.Panel2.Controls.Add(this.label2);
//            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
//            this.splitContainer1.Size = new Size(563, 348);
//            this.splitContainer1.SplitterDistance = 299;
//            this.splitContainer1.TabIndex = 3;
//            // 
//            // splitContainer2
//            // 
//            this.splitContainer2.Dock = DockStyle.Fill;
//            this.splitContainer2.FixedPanel = FixedPanel.Panel1;
//            this.splitContainer2.Location = new Point(0, 0);
//            this.splitContainer2.Name = "splitContainer2";
//            this.splitContainer2.Orientation = Orientation.Horizontal;
//            // 
//            // splitContainer2.Panel1
//            // 
//            this.splitContainer2.Panel1.BackColor = Color.Black;
//            this.splitContainer2.Panel1.Controls.Add(this.cbTitles);
//            this.splitContainer2.Panel1.Controls.Add(this.lblMediaTitle);
//            // 
//            // splitContainer2.Panel2
//            // 
//            this.splitContainer2.Panel2.BackColor = Color.Black;
//            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
//            this.splitContainer2.Size = new Size(563, 299);
//            this.splitContainer2.SplitterDistance = 29;
//            this.splitContainer2.TabIndex = 2;
//            this.splitContainer2.SplitterMoved += new SplitterEventHandler(this.splitContainer2_SplitterMoved);
//            // 
//            // cbTitles
//            // 
//            this.cbTitles.BackColor = Color.Black;
//            this.cbTitles.DropDownStyle = ComboBoxStyle.DropDownList;
//            this.cbTitles.FlatStyle = FlatStyle.Flat;
//            this.cbTitles.Font = new Font("Microsoft Sans Serif", 9.25F, FontStyle.Bold);
//            this.cbTitles.ForeColor = Color.White;
//            this.cbTitles.FormattingEnabled = true;
//            this.cbTitles.Location = new Point(12, 2);
//            this.cbTitles.Name = "cbTitles";
//            this.cbTitles.Size = new Size(307, 23);
//            this.cbTitles.TabIndex = 3;
//            // 
//            // lblMediaTitle
//            // 
//            this.lblMediaTitle.AutoSize = true;
//            this.lblMediaTitle.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
//            this.lblMediaTitle.ForeColor = Color.White;
//            this.lblMediaTitle.Location = new Point(6, 5);
//            this.lblMediaTitle.Name = "lblMediaTitle";
//            this.lblMediaTitle.Size = new Size(217, 16);
//            this.lblMediaTitle.TabIndex = 1;
//            this.lblMediaTitle.Text = "Fifth Element, The (Unverified)";
//            // 
//            // pictureBox1
//            // 
//            this.pictureBox1.Image = ((Image)(resources.GetObject("pictureBox1.Image")));
//            this.pictureBox1.Location = new Point(12, 15);
//            this.pictureBox1.Name = "pictureBox1";
//            this.pictureBox1.Size = new Size(211, 239);
//            this.pictureBox1.TabIndex = 0;
//            this.pictureBox1.TabStop = false;
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.BackColor = Color.Transparent;
//            this.label2.ForeColor = Color.White;
//            this.label2.Location = new Point(412, 15);
//            this.label2.Name = "label2";
//            this.label2.Size = new Size(85, 13);
//            this.label2.TabIndex = 4;
//            this.label2.Text = "Scanning Disc...";
//            // 
//            // progressBar1
//            // 
//            this.progressBar1.Location = new Point(12, 11);
//            this.progressBar1.Name = "progressBar1";
//            this.progressBar1.Size = new Size(394, 23);
//            this.progressBar1.TabIndex = 3;
//            // 
//            // backgroundWorker2
//            // 
//            this.backgroundWorker2.WorkerReportsProgress = true;
//            // 
//            // ProgressForm
//            // 
//            this.AutoScaleDimensions = new SizeF(6F, 13F);
//            //this.AutoScaleMode = AutoScaleMode.Font;
//            this.BackColor = Color.Black;
//            this.ClientSize = new Size(563, 348);
//            this.Controls.Add(this.splitContainer1);
//            this.FormBorderStyle = FormBorderStyle.None;
//            this.Name = "ProgressForm";
//            this.Opacity = 0.7D;
//            this.ShowIcon = false;
//            this.ShowInTaskbar = false;
//            this.SizeGripStyle = SizeGripStyle.Hide;
//            this.StartPosition = FormStartPosition.Manual;
//            this.Text = "Form1";
//            this.TopMost = true;
//            this.Load += new EventHandler(this.Form1_Load);
//            this.splitContainer1.Panel1.ResumeLayout(false);
//            this.splitContainer1.Panel2.ResumeLayout(false);
//            this.splitContainer1.Panel2.PerformLayout();
//            ((ISupportInitialize)(this.splitContainer1)).EndInit();
//            this.splitContainer1.ResumeLayout(false);
//            this.splitContainer2.Panel1.ResumeLayout(false);
//            this.splitContainer2.Panel1.PerformLayout();
//            this.splitContainer2.Panel2.ResumeLayout(false);
//            ((ISupportInitialize)(this.splitContainer2)).EndInit();
//            this.splitContainer2.ResumeLayout(false);
//            ((ISupportInitialize)(this.pictureBox1)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private BackgroundWorker backgroundWorker1;
//        private SplitContainer splitContainer1;
//        private Label lblMediaTitle;
//        private Label label2;
//        private ProgressBar progressBar1;
//        private SplitContainer splitContainer2;
//        private PictureBox pictureBox1;
//        private ComboBox cbTitles;
//        private BackgroundWorker backgroundWorker2;
//    }
//}


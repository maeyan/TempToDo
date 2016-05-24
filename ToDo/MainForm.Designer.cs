namespace ToDo
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LLblCompleteTaskHistory = new System.Windows.Forms.LinkLabel();
            this.RbtnComapleteTaskStrikeout = new System.Windows.Forms.RadioButton();
            this.RbtnCompleteTaskHidden = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TbxContents = new System.Windows.Forms.TextBox();
            this.DtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CbxToBeDetermined = new System.Windows.Forms.CheckBox();
            this.FlpViewArea = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LLblCompleteTaskHistory);
            this.groupBox1.Controls.Add(this.RbtnComapleteTaskStrikeout);
            this.groupBox1.Controls.Add(this.RbtnCompleteTaskHidden);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.TbxContents);
            this.groupBox1.Controls.Add(this.DtpDeadline);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CbxToBeDetermined);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(726, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // LLblCompleteTaskHistory
            // 
            this.LLblCompleteTaskHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LLblCompleteTaskHistory.AutoSize = true;
            this.LLblCompleteTaskHistory.Location = new System.Drawing.Point(604, 96);
            this.LLblCompleteTaskHistory.Name = "LLblCompleteTaskHistory";
            this.LLblCompleteTaskHistory.Size = new System.Drawing.Size(116, 18);
            this.LLblCompleteTaskHistory.TabIndex = 15;
            this.LLblCompleteTaskHistory.TabStop = true;
            this.LLblCompleteTaskHistory.Text = "完了済みタスク履歴";
            this.LLblCompleteTaskHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LLblCompleteTaskHistory_LinkClicked);
            // 
            // RbtnComapleteTaskStrikeout
            // 
            this.RbtnComapleteTaskStrikeout.AutoSize = true;
            this.RbtnComapleteTaskStrikeout.Location = new System.Drawing.Point(220, 94);
            this.RbtnComapleteTaskStrikeout.Name = "RbtnComapleteTaskStrikeout";
            this.RbtnComapleteTaskStrikeout.Size = new System.Drawing.Size(86, 22);
            this.RbtnComapleteTaskStrikeout.TabIndex = 14;
            this.RbtnComapleteTaskStrikeout.Text = "打ち消し線";
            this.RbtnComapleteTaskStrikeout.UseVisualStyleBackColor = true;
            this.RbtnComapleteTaskStrikeout.CheckedChanged += new System.EventHandler(this.RbtnCompleteTaskStrikeout_CheckedChanged);
            // 
            // RbtnCompleteTaskHidden
            // 
            this.RbtnCompleteTaskHidden.AutoSize = true;
            this.RbtnCompleteTaskHidden.Checked = true;
            this.RbtnCompleteTaskHidden.Location = new System.Drawing.Point(152, 94);
            this.RbtnCompleteTaskHidden.Name = "RbtnCompleteTaskHidden";
            this.RbtnCompleteTaskHidden.Size = new System.Drawing.Size(62, 22);
            this.RbtnCompleteTaskHidden.TabIndex = 13;
            this.RbtnCompleteTaskHidden.TabStop = true;
            this.RbtnCompleteTaskHidden.Text = "非表示";
            this.RbtnCompleteTaskHidden.UseVisualStyleBackColor = true;
            this.RbtnCompleteTaskHidden.CheckedChanged += new System.EventHandler(this.RbtnCompleteTaskHidden_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "完了タスクの表示方法：";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(0, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(723, 1);
            this.label3.TabIndex = 11;
            this.label3.Text = "内容";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(635, 19);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(85, 30);
            this.BtnCancel.TabIndex = 10;
            this.BtnCancel.Text = "×修正取消";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Visible = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // TbxContents
            // 
            this.TbxContents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxContents.Location = new System.Drawing.Point(44, 55);
            this.TbxContents.Name = "TbxContents";
            this.TbxContents.Size = new System.Drawing.Size(676, 25);
            this.TbxContents.TabIndex = 8;
            this.TbxContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxContents_KeyDown);
            // 
            // DtpDeadline
            // 
            this.DtpDeadline.Location = new System.Drawing.Point(44, 24);
            this.DtpDeadline.Name = "DtpDeadline";
            this.DtpDeadline.Size = new System.Drawing.Size(123, 25);
            this.DtpDeadline.TabIndex = 9;
            this.DtpDeadline.ValueChanged += new System.EventHandler(this.DtpDeadline_ValueChanged);
            this.DtpDeadline.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDeadline_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "内容";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "期限";
            // 
            // CbxToBeDetermined
            // 
            this.CbxToBeDetermined.AutoSize = true;
            this.CbxToBeDetermined.Location = new System.Drawing.Point(173, 27);
            this.CbxToBeDetermined.Name = "CbxToBeDetermined";
            this.CbxToBeDetermined.Size = new System.Drawing.Size(51, 22);
            this.CbxToBeDetermined.TabIndex = 6;
            this.CbxToBeDetermined.Text = "未定";
            this.CbxToBeDetermined.UseVisualStyleBackColor = true;
            // 
            // FlpViewArea
            // 
            this.FlpViewArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlpViewArea.AutoScroll = true;
            this.FlpViewArea.BackColor = System.Drawing.Color.White;
            this.FlpViewArea.Location = new System.Drawing.Point(12, 137);
            this.FlpViewArea.Name = "FlpViewArea";
            this.FlpViewArea.Size = new System.Drawing.Size(723, 363);
            this.FlpViewArea.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(752, 512);
            this.Controls.Add(this.FlpViewArea);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "ToDo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel LLblCompleteTaskHistory;
        private System.Windows.Forms.RadioButton RbtnComapleteTaskStrikeout;
        private System.Windows.Forms.RadioButton RbtnCompleteTaskHidden;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TextBox TbxContents;
        private System.Windows.Forms.DateTimePicker DtpDeadline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CbxToBeDetermined;
        private System.Windows.Forms.FlowLayoutPanel FlpViewArea;


    }
}


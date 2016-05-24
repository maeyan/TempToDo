namespace ToDo
{
    partial class PersonalToDoHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.RbtnCompleteToday = new System.Windows.Forms.RadioButton();
            this.RbtnComplete1Week = new System.Windows.Forms.RadioButton();
            this.RbtnCompleteAll = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.DtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.PnlCompleteAll = new System.Windows.Forms.Panel();
            this.BtnSeartch = new System.Windows.Forms.Button();
            this.DgvCompleteTaskList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.PnlCompleteAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCompleteTaskList)).BeginInit();
            this.SuspendLayout();
            // 
            // RbtnCompleteToday
            // 
            this.RbtnCompleteToday.AutoSize = true;
            this.RbtnCompleteToday.Checked = true;
            this.RbtnCompleteToday.Location = new System.Drawing.Point(3, 3);
            this.RbtnCompleteToday.Name = "RbtnCompleteToday";
            this.RbtnCompleteToday.Size = new System.Drawing.Size(74, 22);
            this.RbtnCompleteToday.TabIndex = 0;
            this.RbtnCompleteToday.TabStop = true;
            this.RbtnCompleteToday.Text = "本日完了";
            this.RbtnCompleteToday.UseVisualStyleBackColor = true;
            this.RbtnCompleteToday.CheckedChanged += new System.EventHandler(this.RbtnCompleteToday_CheckedChanged);
            // 
            // RbtnComplete1Week
            // 
            this.RbtnComplete1Week.AutoSize = true;
            this.RbtnComplete1Week.Location = new System.Drawing.Point(74, 3);
            this.RbtnComplete1Week.Name = "RbtnComplete1Week";
            this.RbtnComplete1Week.Size = new System.Drawing.Size(146, 22);
            this.RbtnComplete1Week.TabIndex = 1;
            this.RbtnComplete1Week.Text = "過去１週間以内に完了";
            this.RbtnComplete1Week.UseVisualStyleBackColor = true;
            this.RbtnComplete1Week.CheckedChanged += new System.EventHandler(this.RbtnComplete1Week_CheckedChanged);
            // 
            // RbtnCompleteAll
            // 
            this.RbtnCompleteAll.AutoSize = true;
            this.RbtnCompleteAll.Location = new System.Drawing.Point(217, 3);
            this.RbtnCompleteAll.Name = "RbtnCompleteAll";
            this.RbtnCompleteAll.Size = new System.Drawing.Size(122, 22);
            this.RbtnCompleteAll.TabIndex = 2;
            this.RbtnCompleteAll.Text = "任意の期間を指定";
            this.RbtnCompleteAll.UseVisualStyleBackColor = true;
            this.RbtnCompleteAll.CheckedChanged += new System.EventHandler(this.RbtnCompleteAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbtnCompleteAll);
            this.panel1.Controls.Add(this.RbtnComplete1Week);
            this.panel1.Controls.Add(this.RbtnCompleteToday);
            this.panel1.Location = new System.Drawing.Point(26, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 29);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "■表示したいタスクの完了した期間を選択してください";
            // 
            // DtpStartTime
            // 
            this.DtpStartTime.Location = new System.Drawing.Point(3, 3);
            this.DtpStartTime.Margin = new System.Windows.Forms.Padding(0);
            this.DtpStartTime.Name = "DtpStartTime";
            this.DtpStartTime.Size = new System.Drawing.Size(111, 25);
            this.DtpStartTime.TabIndex = 5;
            // 
            // DtpEndTime
            // 
            this.DtpEndTime.Location = new System.Drawing.Point(134, 3);
            this.DtpEndTime.Margin = new System.Windows.Forms.Padding(0);
            this.DtpEndTime.Name = "DtpEndTime";
            this.DtpEndTime.Size = new System.Drawing.Size(111, 25);
            this.DtpEndTime.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "～";
            // 
            // PnlCompleteAll
            // 
            this.PnlCompleteAll.Controls.Add(this.BtnSeartch);
            this.PnlCompleteAll.Controls.Add(this.DtpStartTime);
            this.PnlCompleteAll.Controls.Add(this.DtpEndTime);
            this.PnlCompleteAll.Controls.Add(this.label2);
            this.PnlCompleteAll.Location = new System.Drawing.Point(362, 30);
            this.PnlCompleteAll.Name = "PnlCompleteAll";
            this.PnlCompleteAll.Size = new System.Drawing.Size(247, 58);
            this.PnlCompleteAll.TabIndex = 8;
            this.PnlCompleteAll.Visible = false;
            // 
            // BtnSeartch
            // 
            this.BtnSeartch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(122)))), ((int)(((byte)(196)))));
            this.BtnSeartch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSeartch.ForeColor = System.Drawing.Color.White;
            this.BtnSeartch.Location = new System.Drawing.Point(185, 31);
            this.BtnSeartch.Name = "BtnSeartch";
            this.BtnSeartch.Size = new System.Drawing.Size(59, 27);
            this.BtnSeartch.TabIndex = 8;
            this.BtnSeartch.Text = "検索";
            this.BtnSeartch.UseVisualStyleBackColor = false;
            this.BtnSeartch.Click += new System.EventHandler(this.BtnSeartch_Click);
            // 
            // DgvCompleteTaskList
            // 
            this.DgvCompleteTaskList.AllowUserToAddRows = false;
            this.DgvCompleteTaskList.AllowUserToDeleteRows = false;
            this.DgvCompleteTaskList.AllowUserToResizeColumns = false;
            this.DgvCompleteTaskList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.DgvCompleteTaskList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvCompleteTaskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvCompleteTaskList.BackgroundColor = System.Drawing.Color.White;
            this.DgvCompleteTaskList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvCompleteTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCompleteTaskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCompleteTaskList.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvCompleteTaskList.Location = new System.Drawing.Point(26, 91);
            this.DgvCompleteTaskList.Margin = new System.Windows.Forms.Padding(0);
            this.DgvCompleteTaskList.Name = "DgvCompleteTaskList";
            this.DgvCompleteTaskList.ReadOnly = true;
            this.DgvCompleteTaskList.RowHeadersVisible = false;
            this.DgvCompleteTaskList.RowTemplate.Height = 21;
            this.DgvCompleteTaskList.Size = new System.Drawing.Size(687, 309);
            this.DgvCompleteTaskList.TabIndex = 9;
            this.DgvCompleteTaskList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCompleteTaskList_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(332, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "完了済みから戻すタスクは「復帰」ボタンを押してください";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "id";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column1.HeaderText = "復帰";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Text = "復帰";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 50;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "完了日";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "期日";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "内容";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // PersonalToDoHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(744, 412);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DgvCompleteTaskList);
            this.Controls.Add(this.PnlCompleteAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PersonalToDoHistory";
            this.Text = "完了済み ToDoリスト履歴";
            this.Load += new System.EventHandler(this.PersonalToDoHistory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PnlCompleteAll.ResumeLayout(false);
            this.PnlCompleteAll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCompleteTaskList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RbtnCompleteToday;
        private System.Windows.Forms.RadioButton RbtnComplete1Week;
        private System.Windows.Forms.RadioButton RbtnCompleteAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DtpStartTime;
        private System.Windows.Forms.DateTimePicker DtpEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PnlCompleteAll;
        private System.Windows.Forms.DataGridView DgvCompleteTaskList;
        private System.Windows.Forms.Button BtnSeartch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
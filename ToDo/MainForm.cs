using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDo
{
    /// <summary>
    /// メインのフォームクラス
    /// </summary>
    public partial class MainForm : Form
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }
        #endregion
        #region Getter/Setter
        /// <summary>
        /// 修正ボタン押した時の挙動
        /// </summary>
        private Action<string, string, string> Modify { get; set; }

        /// <summary>
        /// 前回変更イベントが発生した際のパネルサイズを格納(横方向が変わったときのみ処理を行いたい)
        /// </summary>
        private int PreFlpViewAreaWidth { get; set; }
        #endregion

        #region private Method Event
        /// 全体に関係すること
        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 個人ToDoで使う処理
            this.Modify = (id, deadline, contents) =>
            {
                if (deadline == string.Empty)
                {
                    this.CbxToBeDetermined.Checked = true;
                }
                else
                {
                    this.CbxToBeDetermined.Checked = false;
                    this.DtpDeadline.Value = Convert.ToDateTime(deadline);
                }

                this.TbxContents.Text = contents;

                this.TbxContents.Tag = id;

                this.BtnCancel.Visible = true;
            };

            // 前回終了時の完了タスクの表示設定を再現
            if (Properties.Settings.Default.TodoCompleteTaskDisplay == "hidden")
            {
                this.RbtnCompleteTaskHidden.Checked = true;
            }
            else
            {
                this.RbtnComapleteTaskStrikeout.Checked = true;
            }

            // 個人のアプリを表示 
            TodoManager todo = TodoManager.GetInstanse();

            // 完了タスクの表示について(非表示 or 表示+打ち消し)
            todo.CompleteTaskStyle = this.RbtnCompleteTaskHidden.Checked ? TodoManager.CompleteTaskShowStyle.Hidden : TodoManager.CompleteTaskShowStyle.Strikeout;
            todo.Show(this.FlpViewArea, this.Modify);
        }

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.TodoCompleteTaskDisplay = this.RbtnCompleteTaskHidden.Checked ? "hidden" : "strikeout";

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// タスクのパネルサイズが変わった際のイベント
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void FlpViewArea_SizeChanged(object sender, EventArgs e)
        {
            // 前回のイベント時と横方向のサイズが変わっていなければこれ以降の処理は不要
            if (this.PreFlpViewAreaWidth == this.FlpViewArea.Width)
            {
                return;
            }

            // 横方向のサイズが変われば、TodoListの幅を追従させる
            TodoManager todo = TodoManager.GetInstanse();
            foreach (Control pnlControl in this.FlpViewArea.Controls)
            {
                todo.TodoRelayout((Panel)pnlControl, this.FlpViewArea.Width);
            }

            this.PreFlpViewAreaWidth = this.FlpViewArea.Width;
        }

        /// 個人ToDoに関係すること
        /// <summary>
        /// 日付けの値が変更された時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void DtpDeadline_ValueChanged(object sender, EventArgs e)
        {
            this.TbxContents.Focus();
        }

        /// <summary>
        /// 日付けコントロールでキーダウンが発生した時の処理
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void DtpDeadline_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterで登録
            if (e.KeyCode == Keys.Enter)
            {
                this.TodoListExecute();
            }
        }

        /// <summary>
        /// キーダウン時処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時引数</param>
        private void TbxContents_KeyDown(object sender, KeyEventArgs e)
        {
            // Enterで登録
            if (e.KeyCode == Keys.Enter)
            {
                this.TodoListExecute();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 期限が未定の場合のチェックボックスを変更した時、フォーカスは内容のテキストボックスに移す
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void CbxToBeDetermined_CheckedChanged(object sender, EventArgs e)
        {
            this.TbxContents.Focus();
        }

        /// <summary>
        /// 修正キャンセルボタンを押した時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            string id = this.TbxContents.Tag.ToString();

            this.BtnCancel.Visible = false;
            this.TbxContents.Text = string.Empty;
            this.TbxContents.Tag = null;
            this.CbxToBeDetermined.Checked = false;
            this.DtpDeadline.Value = DateTime.Today;

            foreach (Control pnlAreaControl in this.FlpViewArea.Controls)
            {
                if (((Panel)pnlAreaControl).Tag.ToString() == id)
                {
                    pnlAreaControl.BackColor = Todo.MouseLeaveBackGroundColor;

                    // Buttonについている修正のタグを削除する
                    foreach (Control control in pnlAreaControl.Controls)
                    {
                        Button btn = control as Button;

                        if (btn == null || btn.Tag == null)
                        {
                            continue;
                        }

                        btn.Visible = false;
                        btn.Tag = null;
                    }
                }
            }
        }
        #endregion

        #region private Method
        /// <summary>
        /// ToDoデータの登録、変更を行う
        /// </summary>
        private void TodoListExecute()
        {
            try
            {
                bool ret = false;
                string id = (this.TbxContents.Tag == null) ? string.Empty : this.TbxContents.Tag.ToString();

                TodoManager todo = TodoManager.GetInstanse();

                if (id == string.Empty)
                {
                    // ID無 = 修正中ではない
                    if (this.CbxToBeDetermined.Checked)
                    {
                        // 期日未定の時
                        ret = todo.Create(this.FlpViewArea, this.Modify, this.TbxContents.Text);
                    }
                    else
                    {
                        // 期日有りの時
                        ret = todo.Create(this.FlpViewArea, this.Modify, this.TbxContents.Text, this.DtpDeadline.Value);
                    }
                }
                else
                {
                    // ID有 = 修正中
                    if (this.CbxToBeDetermined.Checked)
                    {
                        // 期日未定の時
                        ret = todo.Update(id, this.FlpViewArea, this.TbxContents.Text);
                    }
                    else
                    {
                        // 期日有りの時
                        ret = todo.Update(id, this.FlpViewArea, this.TbxContents.Text, this.DtpDeadline.Value);
                    }

                    // 修正キャンセルボタンの非表示
                    this.BtnCancel.Visible = false;
                }

                // 処理が完了したら初期化しておく
                if (ret)
                {
                    // Formの初期化
                    this.TbxContents.Text = string.Empty;
                    this.CbxToBeDetermined.Checked = false;
                    this.DtpDeadline.Value = DateTime.Today;

                    Todo td = new Todo();
                    foreach (Control pnlAreaControl in this.FlpViewArea.Controls)
                    {
                        foreach (Control control in pnlAreaControl.Controls)
                        {
                            Button btn = control as Button;
                            if (btn == null || btn.Tag == null)
                            {
                                continue;
                            }

                            td.ResetModifyBackGroundColor((Panel)pnlAreaControl);
                        }
                    }
                }

                this.TbxContents.Tag = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.TbxContents.Focus();
        }
        #endregion

        /// <summary>
        /// [個人ToDo]完了タスクを非表示にするラジオボタン変更時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void RbtnCompleteTaskHidden_CheckedChanged(object sender, EventArgs e)
        {
            // フォーカスがはずれたときのイベントは無視する
            if (!this.RbtnCompleteTaskHidden.Focused)
            {
                return;
            }

            // 完了済みタスクの表示を切り替える(非表示)
            TodoManager todo = TodoManager.GetInstanse();
            todo.CompleteTaskStyle = TodoManager.CompleteTaskShowStyle.Hidden;

            todo.ChangeCompleteTaskDisplay(this.FlpViewArea);
        }

        /// <summary>
        /// [個人ToDo]完了タスクを打ち消し表示にするラジオボタン変更時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void RbtnCompleteTaskStrikeout_CheckedChanged(object sender, EventArgs e)
        {
            // フォーカスがはずれたときのイベントは無視する
            if (!this.RbtnComapleteTaskStrikeout.Focused)
            {
                return;
            }

            // 完了済みタスクの表示を切り替える(表示＋打消し線)
            TodoManager todo = TodoManager.GetInstanse();
            todo.CompleteTaskStyle = TodoManager.CompleteTaskShowStyle.Strikeout;
            todo.ChangeCompleteTaskDisplay(this.FlpViewArea);
        }

        /// <summary>
        /// 完了済みタスクの履歴リンクをクリックした際の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void LLblCompleteTaskHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonalToDoHistory form = new PersonalToDoHistory();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            // データの復活が行われたのであれば、DataGridViewの再描画が必要
            if (form.FlagDataRestore)
            {
                TodoManager todo = TodoManager.GetInstanse();
                todo.Show(this.FlpViewArea, this.Modify);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDo
{
    /// <summary>
    /// やらねばならないことを生成するクラス
    /// </summary>
    public class Todo
    {
        #region クラス変数
        /// <summary>
        /// 削除時の背景色
        /// </summary>
        public static readonly Color DeletedBGColor = Color.FromArgb(220, 220, 220);

        /// <summary>
        /// 通常時の背景色
        /// </summary>
        public static readonly Color DefaultBGColor = Color.FromArgb(250, 250, 250);

        /// <summary>
        /// タスク１つのパネルサイズ
        /// </summary>
        public static readonly int PnlAreaWidth = 710;

        /// <summary>
        /// タスク内の内容のパネル内配置位置
        /// </summary>
        public static readonly int LblContentsLeft = 70;

        /// <summary>
        /// タスク内の内容コントローラの幅
        /// </summary>
        public static readonly int LblContentsWidth = Todo.PnlAreaWidth - Todo.LblContentsLeft - 5;

        /// <summary>
        /// タスク内の期限コントローラの幅
        /// </summary>
        public static readonly int LblDeadLineWidth = 100;

        /// <summary>
        /// MouseOverした際の色
        /// </summary>
        public static readonly Color MouseOverBackGroundColor = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// MouseLeaveした際の色
        /// </summary>
        public static readonly Color MouseLeaveBackGroundColor = Color.FromArgb(250, 250, 250);
        #endregion

        #region インスタンス変数
        /// <summary>
        /// 修正ボタン押したときの挙動
        /// </summary>
        private Action<string, string, string> modify = null;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Todo()
        {
        }
        #endregion

        #region インスタンスメソッド(Public)
        /// <summary>
        /// Todoデータを作成する
        /// </summary>
        /// <param name="data">作成に必要なTodoデータ</param>
        /// <param name="completeTaskStyle">完了タスクの表示方法</param>
        /// <param name="modify">修正ボタンを押した際の挙動(フォーム部分の変更を行う受け取る)</param>
        /// <returns>PanelにTodoデータを載せてそれを返す</returns>
        public Panel Create(TodoManager.DataType data, TodoManager.CompleteTaskShowStyle completeTaskStyle, Action<string, string, string> modify)
        {
            // 修正ボタンを押した際に、このメソッドを使うとフォームの変更が行える
            this.modify = modify;

            // 何日後？
            Label lblCountDown = new Label();

            if (data.ToBeDetermined)
            {
                lblCountDown.Text = "未定…";
            }
            else
            {
                int countdown = (data.Deadline - DateTime.Today).Days;
                if (countdown == 0)
                {
                    lblCountDown.ForeColor = Color.Blue;
                    lblCountDown.Text = "本日期限";
                }
                else if (countdown < 0)
                {
                    lblCountDown.ForeColor = Color.Red;
                    lblCountDown.Text = string.Format("{0}日遅延", Math.Abs(countdown).ToString());
                }
                else
                {
                    lblCountDown.Text = string.Format("{0}日後", countdown.ToString());
                }
            }

            lblCountDown.AutoSize = true;
            lblCountDown.Location = new Point(5, 5);
            lblCountDown.Tag = "CountDown";
            lblCountDown.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            lblCountDown.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            lblCountDown.Click += new System.EventHandler(this.LblTodoDeleteClick);

            // 修正ボタン
            Button btnModify = new Button();
            btnModify.AutoSize = false;
            btnModify.BackColor = Color.FromArgb(150, 150, 150);
            btnModify.FlatStyle = FlatStyle.Flat;
            btnModify.Font = new Font("メイリオ", 8);
            btnModify.ForeColor = Color.White;
            btnModify.Location = new Point(5, 25);
            btnModify.Size = new Size(50, 25);
            //// btnModify.Tag = "ModifyButton"; // Tagは、別なところで使っているため指定したらだめ
            btnModify.Text = "修正";
            btnModify.Visible = false;
            btnModify.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            btnModify.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            btnModify.Click += new EventHandler(this.BtnModify_Click);

            // DeadLine
            Label lblDeadLine = new Label();
            lblDeadLine.AutoSize = true;
            lblDeadLine.ForeColor = Color.FromArgb(150, 150, 150);
            lblDeadLine.Location = new Point(635, 30);
            lblDeadLine.Tag = "DeadLine";
            lblDeadLine.Text = data.ToBeDetermined ? string.Empty : data.Deadline.ToString("yyyy-MM-dd(ddd)");
            lblDeadLine.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            lblDeadLine.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            lblDeadLine.Click += new System.EventHandler(this.LblTodoDeleteClick);

            // Contents
            Label lblContents = new Label();
            lblContents.AutoSize = false;
            lblContents.Location = new Point(Todo.LblContentsLeft, 5);
            lblContents.Size = new Size(660, 40); // 555,40
            lblContents.Tag = "Contents";
            lblContents.Text = data.Contents;
            lblContents.Click += new System.EventHandler(this.LblTodoDeleteClick);
            lblContents.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            lblContents.MouseLeave += new System.EventHandler(this.Label_MouseLeave);

            // CreateTime
            Label lblCreateTime = new Label();
            lblCreateTime.Text = data.CreateTime.ToString("yyyy-MM-dd");
            lblCreateTime.Tag = "CreateTime";
            lblCreateTime.Visible = false;

            // Panel
            Panel pnlArea = new Panel();
            pnlArea.BackColor = DefaultBGColor;
            pnlArea.Controls.Add(lblCountDown);
            pnlArea.Controls.Add(lblDeadLine);
            pnlArea.Controls.Add(lblContents);
            pnlArea.Controls.Add(lblCreateTime);
            pnlArea.Controls.Add(btnModify);
            pnlArea.Size = new Size(Todo.PnlAreaWidth, 50);
            pnlArea.Tag = data.Id.ToString();
            pnlArea.MouseEnter += new System.EventHandler(this.PnlArea_MouseEnter);
            pnlArea.MouseLeave += new System.EventHandler(this.PnlArea_MouseLeave);
            pnlArea.Click += new System.EventHandler(this.PnlTodoDeleteClick);

            // 完了タスクの場合は、完了タスク用のしょりを行う
            if (data.Deleted == 1)
            {
                this.ChangeCompleteTaskStyle(pnlArea, completeTaskStyle);
            }

            return pnlArea;
        }

        /// <summary>
        /// 修正のために背景色が、固定となっている部分を元に戻す
        /// </summary>
        /// <param name="pnlArea">ToDoデータ</param>
        public void ResetModifyBackGroundColor(Panel pnlArea)
        {
            // PanelAreaの親(FlowLayoutPanel)の子(PanelAreaのリストに対し、ButtonのTagを調べて編集中があれば色を元に戻す)
            foreach (Control pnlAreaControl in pnlArea.Parent.Controls)
            {
                // PanelArea内にあるコントロールをループしてButtonのコントロールを探す
                foreach (Control control in pnlAreaControl.Controls)
                {
                    Button btn = control as Button;

                    if (btn == null || btn.Tag == null)
                    {
                        continue;
                    }

                    btn.Tag = string.Empty;
                    this.ChangeBackgroundColorMouseLeave((Panel)pnlAreaControl);
                }
            }
        }
        #endregion

        #region インスタンスメソッド(Private)
        /// <summary>
        /// 完了タスクの表示を切り替える
        /// </summary>
        /// <param name="panel">タスクが記載されているパネルコントロール</param>
        /// <param name="completeTaskStyle">タスク完了時の表示方法</param>
        private void ChangeCompleteTaskStyle(Panel panel, TodoManager.CompleteTaskShowStyle completeTaskStyle)
        {
            // 完了タスク表示にする（要は打ち消し線を追加し、背景色を変える)
            foreach (Control control in panel.Controls)
            {
                if (control.Tag == null)
                {
                    // tagが設定されていない場合はnullとなるので次のコントロールに変更する
                    continue;
                }
                else if (control.Tag.ToString() == "CountDown")
                {
                    control.Font = new Font("メイリオ", 9, FontStyle.Strikeout);
                }
                else if (control.Tag.ToString() == "DeadLine")
                {
                    control.Font = new Font("メイリオ", 8, FontStyle.Strikeout);
                }
                else if (control.Tag.ToString() == "Contents")
                {
                    control.Font = new Font("メイリオ", 9, FontStyle.Strikeout);
                }
            }

            // 背景色を変える
            panel.BackColor = DeletedBGColor;
            panel.Visible = (completeTaskStyle == TodoManager.CompleteTaskShowStyle.Hidden) ? false : true;
        }

        /// <summary>
        /// 修正ボタン押した時の挙動
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            Panel pnlArea = (Panel)((Control)sender).Parent;
            string id = pnlArea.Tag.ToString();
            string deadline = string.Empty;
            string contents = string.Empty;

            this.ResetModifyBackGroundColor(pnlArea);

            foreach (Control control in pnlArea.Controls)
            {
                Label lbl = control as Label;
                Button btn = control as Button;

                if (lbl != null && lbl.Tag != null)
                {
                    if (lbl.Tag.ToString() == "DeadLine")
                    {
                        deadline = lbl.Text;
                    }
                    else if (lbl.Tag.ToString() == "Contents")
                    {
                        contents = lbl.Text;
                    }
                }
                else if (btn != null)
                {
                    btn.Tag = "Modify";
                }
            }

            this.modify(id, deadline, contents);
        }

        /// <summary>
        /// ToDoデータ(パネル部分)をクリックした時の処理
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void PnlTodoDeleteClick(object sender, EventArgs e)
        {
            // タスクを削除する際に削除済みタスクなら何もしない
            Panel panel = (Panel)sender;
            if (panel.BackColor == Todo.DeletedBGColor)
            {
                return;
            }
            else
            {
                // 完了タスク状態に切り替える(打ち消し or 打ち消し＋削除)
                TodoManager todo = TodoManager.GetInstanse();
                this.ChangeCompleteTaskStyle(panel, todo.CompleteTaskStyle);

                foreach (Control control in panel.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        control.Visible = false;
                    }
                }
            }

            this.DeleteTodo((Panel)sender);
        }

        /// <summary>
        /// ToDoデータ(ラベル部分)をクリックした時の処理
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void LblTodoDeleteClick(object sender, EventArgs e)
        {
            // タスクを削除する際に削除済みタスクなら何もしない
            Panel panel = (Panel)((Control)sender).Parent;

            if (panel.BackColor == Todo.DeletedBGColor)
            {
                return;
            }
            else
            {
                // 完了タスク状態に切り替える(打ち消し or 打ち消し＋削除)
                TodoManager todo = TodoManager.GetInstanse();
                this.ChangeCompleteTaskStyle(panel, todo.CompleteTaskStyle);

                //// マウスオーバー時に表示される修正ボタンは非表示にする
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        control.Visible = false;
                    }
                }
            }

            this.DeleteTodo((Panel)((Control)sender).Parent);
        }

        /// <summary>
        /// Todoの1データにMouseEnterしたら背景色を返る
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void PnlArea_MouseEnter(object sender, EventArgs e)
        {
            // 削除済み状態であればマウスオーバーで背景色は変えない(Panelの背景色で判断)
            if (((Panel)sender).BackColor == Todo.DeletedBGColor)
            {
                return;
            }

            this.ChangeBackgroundColorMouseEnter((Panel)sender);
        }

        /// <summary>
        /// Todoの1データにMouseLeaveしたら背景色を戻す
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void PnlArea_MouseLeave(object sender, EventArgs e)
        {
            // 削除済み状態であればマウスオーバーで背景色は変えない(Panelの背景色で判断)
            if (((Panel)sender).BackColor == Todo.DeletedBGColor)
            {
                return;
            }

            this.ChangeBackgroundColorMouseLeave((Panel)sender);
        }

        /// <summary>
        /// どのラベルにMouseEnterしてもTodo自体の背景色を返る
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void Label_MouseEnter(object sender, EventArgs e)
        {
            // 削除済み状態であればマウスオーバーで背景色は変えない(Panelの背景色で判断)
            if (((Panel)((Control)sender).Parent).BackColor == Todo.DeletedBGColor)
            {
                return;
            }

            this.ChangeBackgroundColorMouseEnter((Panel)((Control)sender).Parent);
        }

        /// <summary>
        /// どのラベルにMouseEnterしてもTodo自体の背景色を戻す
        /// </summary>
        /// <param name="sender">EventのObject</param>
        /// <param name="e">EventObjectの引数</param>
        private void Label_MouseLeave(object sender, EventArgs e)
        {
            // タスク削除に伴い発生するMouseLeaveの場合、親であるPanelが削除済みのため
            // ParentがNullになっていることに注意

            // 削除済み状態であればマウスオーバーで背景色は変えない(Panelの背景色で判断)
            Control control = (Control)sender;
            if (control.Parent != null && ((Panel)control.Parent).BackColor == Todo.DeletedBGColor)
            {
                return;
            }

            this.ChangeBackgroundColorMouseLeave((Panel)((Control)sender).Parent);
        }

        /// <summary>
        /// Todoの１データに対しMouseEnterした時Panelの背景色を変える
        /// </summary>
        /// <param name="pnlArea">Todoの１データ</param>
        private void ChangeBackgroundColorMouseEnter(Panel pnlArea)
        {
            // 初期状態⇒マウスオーバー
            pnlArea.BackColor = MouseOverBackGroundColor;

            foreach (Control control in pnlArea.Controls)
            {
                if (control.GetType().ToString() == "System.Windows.Forms.Button")
                {
                    control.Visible = true;
                }
            }
        }

        /// <summary>
        /// Todoの１データに対しMouseLeaveした時Panelの背景色を戻す
        /// ただし、修正中なら戻さない
        /// </summary>
        /// <param name="pnlArea">Todoの１データ</param>
        private void ChangeBackgroundColorMouseLeave(Panel pnlArea)
        {
            // マウスオーバー⇒初期状態
            if (pnlArea != null)
            {
                // 現在修正中か調べて修正中なら背景色を戻さない
                if (this.IsModify(pnlArea))
                {
                    return;
                }

                pnlArea.BackColor = Color.FromArgb(250, 250, 250);

                foreach (Control control in pnlArea.Controls)
                {
                    if (control.GetType().ToString() == "System.Windows.Forms.Button")
                    {
                        // カーソルを調べてPanelの範囲内であれば消さない
                        // カーソルを取得し、パネル基準に座標を変換する
                        Point sp = Cursor.Position;
                        Point cp = pnlArea.PointToClient(sp);

                        if (cp.Y < 0 || pnlArea.Height <= cp.Y || cp.X < 0 || pnlArea.Width <= cp.X)
                        {
                            control.Visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 現在修正中かどうか調べる
        /// 修正ボタンタグに修正中はmodifyと文字列が入っている
        /// </summary>
        /// <param name="pnlArea">ToDoデータ</param>
        /// <returns>True:修正中、False:修正中ではない</returns>
        private bool IsModify(Panel pnlArea)
        {
            string tag = string.Empty;
            foreach (Control control in pnlArea.Controls)
            {
                Button btn = control as Button;

                if (btn == null || btn.Tag == null)
                {
                    continue;
                }

                tag = btn.Tag.ToString();
            }

            return (tag == string.Empty) ? false : true;
        }

        /// <summary>
        /// ClickされたTodoを削除する
        /// </summary>
        /// <param name="pnlArea">Todoの一つのGUIデータ</param>
        private void DeleteTodo(Panel pnlArea)
        {
            TodoManager todo = TodoManager.GetInstanse();
            todo.Delete(pnlArea);
        }
        #endregion
    }
}

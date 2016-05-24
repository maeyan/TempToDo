using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDo
{
    /// <summary>
    /// TodoのCRUDを管理
    /// </summary>
    public class TodoManager
    {
        /// <summary>
        /// TodoManagerは、FormからもTodo自身からも呼びたいのでいつでもnewして同じインスタンスを参照させたいので
        /// シングルトン
        /// </summary>
        private static TodoManager instanse = new TodoManager();

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private TodoManager()
        {
        }
        #endregion

        #region 列挙
        /// <summary>
        /// 完了タスクの表示方法
        /// </summary>
        public enum CompleteTaskShowStyle
        {
            /// <summary>
            /// 非表示
            /// </summary>
            Hidden,

            /// <summary>
            /// 打ち消し
            /// </summary>
            Strikeout
        }
        #endregion

        #region インスタンス変数
        /// <summary>
        /// 完了タスクの表示方法
        /// </summary>
        public CompleteTaskShowStyle CompleteTaskStyle { get; set; }

        /// <summary>
        /// やることのリスト
        /// </summary>
        private List<TodoManager.DataType> DataList { get; set; }
        #endregion

        #region ClassMethod
        /// <summary>
        /// TodoManagerのインスタンスを取得
        /// </summary>
        /// <returns>TodoManagerのインスタンス</returns>
        public static TodoManager GetInstanse()
        {
            return instanse;
        }
        #endregion

        #region InstanceMethod-Public
        /// <summary>
        /// Todo１データを削除する
        /// </summary>
        /// <param name="pnlArea">Todo1データ</param>
        public void Delete(Panel pnlArea)
        {
            int id = Convert.ToInt32(pnlArea.Tag.ToString());

            TodoDB db = new TodoDB();
            db.DeleteToDo(id);
        }

        /// <summary>
        /// やることの生成(期日が決まっている時)
        /// </summary>
        /// <param name="flpViewArea">ToDoデータを保持するエリア</param>
        /// <param name="modify">修正ボタンを押した際の挙動(フォーム部分の変更を行うメソッドを受け取る)</param>
        /// <param name="contents">内容</param>
        /// <param name="deadline">期限</param>
        /// <returns>True:作成成功、False:作成失敗</returns>
        public bool Create(FlowLayoutPanel flpViewArea, Action<string, string, string> modify, string contents, DateTime deadline)
        {
            return this.Create(flpViewArea, modify, contents, deadline, false);
        }

        /// <summary>
        /// 期限未定時データ作成
        /// </summary>
        /// <param name="flpViewArea">ToDoデータを保持するエリア</param>
        /// <param name="modify">修正ボタンを押した際の挙動(フォーム部分の変更を行うメソッドを受け取る)</param>
        /// <param name="contents">内容</param>
        /// <returns>True:作成成功、False:作成失敗</returns>
        public bool Create(FlowLayoutPanel flpViewArea, Action<string, string, string> modify, string contents)
        {
            return this.Create(flpViewArea, modify, contents, new DateTime(0), true);
        }

        /// <summary>
        /// ToDoデータのUpdate
        /// </summary>
        /// <param name="id">Update対象のID</param>
        /// <param name="flpViewArea">ToDoが保持されているFlpViewArea</param>
        /// <param name="contents">ToDoの内容</param>
        /// <param name="deadline">期日</param>
        /// <returns>True:Update成功、False:Update失敗</returns>
        public bool Update(string id, FlowLayoutPanel flpViewArea, string contents, DateTime deadline)
        {
            return this.Update(id, flpViewArea, contents, deadline, false);
        }

        /// <summary>
        /// ToDoデータのUpdate
        /// </summary>
        /// <param name="id">Update対象のID</param>
        /// <param name="flpViewArea">ToDoが保持されているFlpViewArea</param>
        /// <param name="contents">ToDoの内容</param>
        /// <returns>True:Update成功、False:Update失敗</returns>
        public bool Update(string id, FlowLayoutPanel flpViewArea, string contents)
        {
            return this.Update(id, flpViewArea, contents, new DateTime(0), true);
        }

        /// <summary>
        /// Todoの並び替えに必要な時間を取得する
        /// 期限がある時は、期限の時間。
        /// 期限がない時は、作成時間。
        /// </summary>
        /// <param name="pnlArea">ToDoのデータ</param>
        /// <returns>時間を返す</returns>
        public DateTime GetTime(Panel pnlArea)
        {
            DateTime dt = new DateTime(0);
            string deadLine = string.Empty;

            foreach (Control control in pnlArea.Controls)
            {
                Label lbl = control as Label;

                if (lbl == null || lbl.Tag == null)
                {
                    continue;
                }

                if (lbl.Tag.ToString() == "CreateTime")
                {
                    if (lbl.Text != string.Empty)
                    {
                        dt = Convert.ToDateTime(lbl.Text);
                    }
                }
                else if (lbl.Tag.ToString() == "DeadLine")
                {
                    deadLine = lbl.Text;
                }
            }

            if (deadLine != string.Empty)
            {
                dt = Convert.ToDateTime(deadLine);
            }

            return dt;
        }

        /// <summary>
        /// 一つのToDoデータを渡しその中で期日が設定されているかチェックする
        /// </summary>
        /// <param name="pnlArea">Todoデータを渡す</param>
        /// <returns>True:期日あり、False:期日無し</returns>
        public bool IsDeadLine(Panel pnlArea)
        {
            bool flag = false;
            foreach (Control control in pnlArea.Controls)
            {
                Label lbl = control as Label;

                if (lbl == null || lbl.Tag == null)
                {
                    continue;
                }

                if (lbl.Tag.ToString() == "DeadLine")
                {
                    if (lbl.Text == string.Empty)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }

                    break;
                }
            }

            return flag;
        }

        /// <summary>
        /// Todo一覧を表示する
        /// </summary>
        /// <param name="viewArea">表示するエリア</param>
        /// <param name="modify">修正ボタンを押した際の挙動(フォーム部分の変更を行うメソッドを受け取る)</param>
        public void Show(FlowLayoutPanel viewArea, Action<string, string, string> modify)
        {
            ////全て削除して綺麗にしておく
            viewArea.Controls.Clear();

            TodoDB db = new TodoDB();
            Todo todo = new Todo();
            List<DataType> dataAll = db.All();

            // 期間ありデータ
            foreach (DataType data in dataAll.Where(d => !d.ToBeDetermined).OrderBy(d => d.Deadline).ToList())
            {
                // 一つずつデータを受け取りパネルデータを受け取り追加する
                Panel panelData = todo.Create(data, this.CompleteTaskStyle, modify);
                this.TodoRelayout(panelData, viewArea.Width);
                viewArea.Controls.Add(panelData);
            }

            // 期間なしデータ
            foreach (DataType data in dataAll.Where(d => d.ToBeDetermined).OrderBy(d => d.Deadline).ToList())
            {
                // 一つずつデータを受け取りパネルデータを受け取り追加する
                Panel panelData = todo.Create(data, this.CompleteTaskStyle, modify);
                this.TodoRelayout(panelData, viewArea.Width);
                viewArea.Controls.Add(panelData);
            }
        }

        /// <summary>
        /// タスクが記載されているPanel内を操作して背景色が完了タスクカラーだった場合、表示の変更を行う
        /// </summary>
        /// <param name="flpViewArea">タスクが配置してあるパネル</param>
        public void ChangeCompleteTaskDisplay(FlowLayoutPanel flpViewArea)
        {
            bool flagVisible = this.CompleteTaskStyle == TodoManager.CompleteTaskShowStyle.Hidden ? false : true;

            // todoを表示するエリア直下にあるコントロール(Panel)をループで処理する
            foreach (Control control in flpViewArea.Controls)
            {
                // 完了済みでないタスクは処理とは関係ないので次のループ
                if (control.BackColor != Todo.DeletedBGColor)
                {
                    continue;
                }

                Application.DoEvents();

                control.Visible = flagVisible;
            }
        }

        /// <summary>
        /// FlpViewAreaの現在の幅に合わせてパネルの幅を決定する
        /// </summary>
        /// <param name="panel">todoが格納されているパネル</param>
        /// <param name="flpViewAreaWidth">Todoが格納されているパネルの幅</param>
        public void TodoRelayout(Panel panel, int flpViewAreaWidth)
        {
            int panelWidth = Todo.PnlAreaWidth + 25 < flpViewAreaWidth ? flpViewAreaWidth - 25 : Todo.PnlAreaWidth;

            int pnaelHeight = panel.Size.Height;
            panel.Size = new Size(panelWidth, pnaelHeight);

            // panel内のコントローラの位置を変更する
            foreach (Control controlInPanel in panel.Controls)
            {
                // Tagが設定されていないものは対象外(操作したいものにはTagを設定してある)
                if (controlInPanel.Tag == null)
                {
                    continue;
                }

                string tagString = controlInPanel.Tag.ToString();

                if (tagString == "DeadLine")
                {
                    int left = panelWidth - Todo.LblDeadLineWidth;
                    int top = controlInPanel.Location.Y;

                    controlInPanel.Location = new Point(left, top);
                }
                else if (tagString == "Contents")
                {
                    int left = panelWidth - Todo.LblContentsLeft - 25;
                    int top = controlInPanel.Size.Height;

                    controlInPanel.Size = new Size(left, top);
                }
            }
        }
        #endregion

        #region InstanceMethod-Private
        /// <summary>
        /// ToDoデータのUpdate
        /// </summary>
        /// <param name="id">Update対象のID</param>
        /// <param name="flpViewArea">ToDoが保持されているFlpViewArea</param>
        /// <param name="contents">ToDoの内容</param>
        /// <param name="deadline">期日</param>
        /// <param name="tobedetermined">未定フラグ</param>
        /// <returns>True:Update成功、False:Update失敗</returns>
        private bool Update(string id, FlowLayoutPanel flpViewArea, string contents, DateTime deadline, bool tobedetermined)
        {
            // データに不備があれば終了する
            if (contents == string.Empty)
            {
                MessageBox.Show("内容が空欄です");
                return false;
            }

            DataType d = new DataType();
            d.Contents = contents;
            d.CreateTime = DateTime.Today;
            d.Deadline = deadline;
            d.ToBeDetermined = tobedetermined;
            d.Id = Convert.ToInt32(id);

            try
            {
                TodoDB db = new TodoDB();
                db.UpdateTodo(d);
            }
            catch
            {
                throw;
            }

            // 表示内容を更新し、必要に応じて順番を入れ替える
            this.RelocateTodo(flpViewArea, d);

            // ID情報から内容の更新をする
            foreach (Control pnlArea in flpViewArea.Controls)
            {
                string tempID = ((Panel)pnlArea).Tag.ToString();

                if (tempID != d.Id.ToString())
                {
                    continue;
                }

                // データを書き換える
                foreach (Control control in pnlArea.Controls)
                {
                    Label lbl = control as Label;

                    if (lbl == null || lbl.Tag == null)
                    {
                        continue;
                    }

                    if (lbl.Tag.ToString() == "DeadLine")
                    {
                        lbl.Text = d.ToBeDetermined ? string.Empty : d.Deadline.ToString("yyyy-MM-dd(ddd)");
                    }
                    else if (lbl.Tag.ToString() == "Contents")
                    {
                        lbl.Text = d.Contents;
                    }
                    else if (lbl.Tag.ToString() == "CountDown")
                    {
                        if (d.ToBeDetermined)
                        {
                            lbl.ForeColor = Color.FromArgb(54, 54, 54);
                            lbl.Text = "未定…";
                        }
                        else
                        {
                            int countdown = (d.Deadline - DateTime.Today).Days;
                            if (countdown == 0)
                            {
                                lbl.ForeColor = Color.Blue;
                                lbl.Text = "本日期限";
                            }
                            else if (countdown < 0)
                            {
                                lbl.ForeColor = Color.Red;
                                lbl.Text = string.Format("{0}日遅延", Math.Abs(countdown).ToString());
                            }
                            else
                            {
                                lbl.ForeColor = Color.FromArgb(54, 54, 54);
                                lbl.Text = string.Format("{0}日後", countdown.ToString());
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 期限有り時データを作成
        /// </summary>
        /// <param name="flpViewArea">ToDoデータを保持するエリア</param>
        /// <param name="modify">修正ボタンを押した際の挙動(フォーム部分の変更を行うメソッドを受け取る)</param>
        /// <param name="contents">内容</param>
        /// <param name="deadline">期限</param>
        /// <param name="tobedetermined">未定フラグ</param>
        /// <returns>true:作成成功, false:作成失敗</returns>
        private bool Create(FlowLayoutPanel flpViewArea, Action<string, string, string> modify, string contents, DateTime deadline, bool tobedetermined)
        {
            // データに不備があれば終了する
            if (contents == string.Empty)
            {
                MessageBox.Show("内容が空欄です");
                return false;
            }

            DataType d = new DataType();
            d.Contents = contents;
            d.CreateTime = DateTime.Today;
            d.Deadline = Convert.ToDateTime(deadline.ToString("yyyy-MM-dd")); // 時間を落とす
            d.ToBeDetermined = tobedetermined;
            d.ExecuteUser = Environment.UserName;

            try
            {
                TodoDB db = new TodoDB();
                d.Id = db.InsertToDo(d);
            }
            catch
            {
                throw;
            }

            // 表示する
            Todo todo = new Todo();
            Panel panelData = todo.Create(d, this.CompleteTaskStyle, modify);

            TodoManager todoMG = TodoManager.GetInstanse();
            todoMG.TodoRelayout(panelData, flpViewArea.Width);
            flpViewArea.Controls.Add(panelData);

            // 並び替えを行う
            this.RelocateTodo(flpViewArea, d);

            return true;
        }

        /// <summary>
        /// Todoデータの並び替え
        /// </summary>
        /// <param name="flpViewArea">ToDoデータ表示されているFlowLayoutPanelのオブジェクト</param>
        /// <param name="todoData">並び替え対象のToDoデータ</param>
        private void RelocateTodo(FlowLayoutPanel flpViewArea, DataType todoData)
        {
            int index = 0;
            for (int i = 0; i < flpViewArea.Controls.Count; i++)
            {
                Panel pnlArea = (Panel)flpViewArea.Controls[i];
                DateTime dt = this.GetTime(pnlArea);

                // 自分自身のデータの時はインクリメントしない
                if (pnlArea.Tag.ToString() == todoData.Id.ToString())
                {
                    continue;
                }

                if (this.IsDeadLine(pnlArea))
                {
                    // DeadLineが設定されていてかつこれから追加するTodoは期日未定の場合
                    if (todoData.ToBeDetermined)
                    {
                        index++; // 無条件でインクリメント
                    }
                    else
                    {
                        TimeSpan ts = todoData.Deadline - dt;
                        if (0 <= ts.Days)
                        {
                            index++; // これから追加するデータの方が古い場合indexをインクリメントする
                        }
                        else
                        {
                            break; // 逆転したらindexをインクメントするのやめる
                        }
                    }
                }
                else
                {
                    // DeadLineが設定されていない場合
                    if (todoData.ToBeDetermined)
                    {
                        TimeSpan ts = todoData.Deadline - dt;
                        if (ts.Milliseconds <= 0)
                        {
                            index++; // これから追加するデータの方が古い場合indexをインクリメントする
                        }
                        else
                        {
                            break; // 逆転したらindexをインクメントするのやめる
                        }
                    }
                    else
                    {
                        // これから追加したいTodoが期限ありで、比較相手が未定義ならここでインクリメントするの終了
                        break;
                    }
                }
            }

            // idから対象のpnlAreaを特定する
            foreach (Control pnlArea in flpViewArea.Controls)
            {
                string id = ((Panel)pnlArea).Tag.ToString();

                if (id == todoData.Id.ToString())
                {
                    flpViewArea.Controls.SetChildIndex(pnlArea, index);
                    flpViewArea.ResumeLayout();
                }
            }
        }
        #endregion

        #region 構造体
        /// <summary>
        /// やることのデータ型
        /// </summary>
        public struct DataType
        {
            /// <summary>
            /// 更新時に使うid
            /// </summary>
            public int Id;

            /// <summary>
            /// 期限
            /// </summary>
            public DateTime Deadline;

            /// <summary>
            /// 削除フラグ
            /// </summary>
            public int Deleted;

            /// <summary>
            /// 完了した日時
            /// </summary>
            public DateTime Deletedtime;

            /// <summary>
            /// 内容
            /// </summary>
            public string Contents;

            /// <summary>
            /// 実行するユーザー
            /// </summary>
            public string ExecuteUser;

            /// <summary>
            /// 作成日時
            /// </summary>
            public DateTime CreateTime;

            /// <summary>
            /// 未定フラグ
            /// </summary>
            public bool ToBeDetermined;
        }

        /// <summary>
        /// 完了したタスクのデータ型
        /// </summary>
        public struct CompleteDataType
        {
            /// <summary>
            /// 更新時に使うid
            /// </summary>
            public int Id;

            /// <summary>
            /// 期限
            /// </summary>
            public DateTime Deadline;

            /// <summary>
            /// 完了した日時
            /// </summary>
            public DateTime Deletedtime;

            /// <summary>
            /// 未定フラグ
            /// </summary>
            public bool ToBeDetermined;

            /// <summary>
            /// タスクの内容
            /// </summary>
            public string Contents;
        }
        #endregion
    }
}

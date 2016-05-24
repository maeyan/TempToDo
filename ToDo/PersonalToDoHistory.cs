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
    /// 完了タスクの履歴フォームのタスク
    /// </summary>
    public partial class PersonalToDoHistory : Form
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PersonalToDoHistory()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Getter/Setter
        /// <summary>
        /// 完了タスクを復元したかどうかのフラグ
        /// </summary>
        public bool FlagDataRestore { get; private set; }
        #endregion

        #region イベント
        /// <summary>
        /// 本日を選択した際のイベント
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void RbtnCompleteToday_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCompleteAll.Visible = false;
            this.ShowCompleteTodayTask();
        }

        /// <summary>
        /// １週間以内を選択した際のイベント
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void RbtnComplete1Week_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCompleteAll.Visible = false;
            this.ShowComplete1WeekTask();
        }

        /// <summary>
        /// 任意の期間指定を選択した際のイベント
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void RbtnCompleteAll_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCompleteAll.Visible = true;
        }

        /// <summary>
        /// フォームロード時の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void PersonalToDoHistory_Load(object sender, EventArgs e)
        {
            this.ShowCompleteTodayTask();
        }
        #endregion

        #region インスタンスメソッド(Private)
        /// <summary>
        /// 本日完了したタスクを表示する
        /// </summary>
        private void ShowCompleteTodayTask()
        {
            // 本日削除したタスクを表示する
            TodoDB db = new TodoDB();
            List<TodoManager.CompleteDataType> dataList = db.CompleteTodayTask();
            this.ShowCompleteTask(dataList);
        }

        /// <summary>
        /// １週間以内に完了したタスクを表示する
        /// </summary>
        private void ShowComplete1WeekTask()
        {
            // 1週間以内に削除したタスクを表示する
            TodoDB db = new TodoDB();
            List<TodoManager.CompleteDataType> dataList = db.Complete1WeekTask();
            this.ShowCompleteTask(dataList);
        }

        /// <summary>
        /// 任意の期間の完了タスクを表示する
        /// </summary>
        /// <param name="startTime">開始日</param>
        /// <param name="endTime">完了日</param>
        private void ShowCompleteAnyPeriodTask(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime)
            {
                MessageBox.Show("開始期日は、終了期日より前に設定してください");
                return;
            }

            // 任意の期間内に削除したタスクを表示する
            TodoDB db = new TodoDB();
            List<TodoManager.CompleteDataType> dataList = db.CompleteAnyPeriodTask(startTime, endTime);
            this.ShowCompleteTask(dataList);
        }

        /// <summary>
        /// 完了タスクの一覧を表示する
        /// </summary>
        /// <param name="dataList">完了タスクの一覧</param>
        private void ShowCompleteTask(List<TodoManager.CompleteDataType> dataList)
        {
            // 初期化
            this.DgvCompleteTaskList.Rows.Clear();

            // データを追加
            foreach (TodoManager.CompleteDataType data in dataList)
            {
                this.DgvCompleteTaskList.Rows.Add();
                int index = this.DgvCompleteTaskList.Rows.Count - 1;
                this.DgvCompleteTaskList.Rows[index].Cells[0].Value = data.Id;
                this.DgvCompleteTaskList.Rows[index].Cells[2].Value = data.Deletedtime.ToString("yyyy-MM-dd(ddd)");
                this.DgvCompleteTaskList.Rows[index].Cells[3].Value = data.ToBeDetermined ? "-" : data.Deadline.ToString("yyyy-MM-dd(ddd)");
                this.DgvCompleteTaskList.Rows[index].Cells[4].Value = data.Contents;
            }
        }
        #endregion

        /// <summary>
        /// 検索ボタンをクリックした際の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void BtnSeartch_Click(object sender, EventArgs e)
        {
            this.ShowCompleteAnyPeriodTask(this.DtpStartTime.Value, this.DtpEndTime.Value);
        }

        /// <summary>
        /// 完了したタスク一覧をクリックした際の処理
        /// </summary>
        /// <param name="sender">Event時Object</param>
        /// <param name="e">Event時Argument</param>
        private void DgvCompleteTaskList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 復帰ボタンの列で１行目以降でイベントが発生したとき意外は何もしない
            if (-1 == e.RowIndex || e.ColumnIndex != 1)
            {
                return;
            }

            // 完了タスクを復帰させる
            int id = Convert.ToInt32(this.DgvCompleteTaskList.Rows[e.RowIndex].Cells[0].Value);
            TodoDB db = new TodoDB();
            try
            {
                // タスクを復活させる。
                db.RestoreCompleteTask(id);

                // データを復活させたらフラグを立てておく(フォームを閉じた後で親のフォームから復帰させた事実を知る方法として保持しておく)
                this.FlagDataRestore = true;

                // DataGridViewの内容を再読み込みをする
                if (this.RbtnCompleteToday.Checked)
                {
                    this.ShowCompleteTodayTask();
                }
                else if (this.RbtnComplete1Week.Checked)
                {
                    this.ShowComplete1WeekTask();
                }
                else
                {
                    DateTime startTime = this.DtpStartTime.Value;
                    DateTime endTime = this.DtpEndTime.Value;
                    this.ShowCompleteAnyPeriodTask(startTime, endTime);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDo
{
    /// <summary>
    /// DBに対しTODOデータを操作する
    /// </summary>
    public class TodoDB
    {
        /// <summary>
        /// DBの名前
        /// </summary>
        private readonly string dbName = @"Todo.sdf";

        /// <summary>
        /// DBのパス
        /// </summary>
        private readonly string password = "Pass@1234";

        /// <summary>
        /// DB接続Object
        /// </summary>
        private SqlCeConnection con = null;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TodoDB()
        {
            try
            {
                string directoryPath = Application.StartupPath;
                this.DBPath = string.Format(@"{0}\{1}", Application.StartupPath, this.dbName);

                // 存在しなければ作成する
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                this.con = new SqlCeConnection("Data Source = " + this.DBPath + ";password=" + this.password);
                this.con.Open();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region インスタンス変数
        /// <summary>
        /// DBPathインスタンス変数
        /// </summary>
        private string DBPath { get; set; }
        #endregion

        #region public method
        /// <summary>
        /// 未処理の全Todoデータを取得
        /// </summary>
        /// <returns>Todoデータのリストを返す</returns>
        public List<TodoManager.DataType> All()
        {
            List<TodoManager.DataType> dataList = new List<TodoManager.DataType>();

            string sql = "SELECT [id], [deadline], [contents], [tobedetermined], [createtime], [deletedtime], [deleted] " +
                         "FROM [ToDo] " +
                         "WHERE ([deleted] = 0 Or [deleted] = 1 AND [deletedtime] = @deletedtime);";

            using (SqlCeCommand cmd = this.con.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.Add("deletedtime", System.Data.DbType.DateTime);
                cmd.Parameters["deletedtime"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                cmd.Prepare();

                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TodoManager.DataType d = new TodoManager.DataType();
                        d.Contents = reader["contents"].ToString();
                        d.CreateTime = Convert.ToDateTime(reader["createtime"]);
                        d.Deadline = Convert.ToDateTime(reader["deadline"]);
                        d.Id = Convert.ToInt32(reader["id"]);
                        d.ToBeDetermined = (Convert.ToInt32(reader["tobedetermined"]) == 0) ? false : true;
                        d.Deleted = Convert.ToInt32(reader["deleted"]);
                        d.Deletedtime = Convert.ToDateTime(reader["deletedtime"]);

                        dataList.Add(d);
                    }
                }
            }

            return dataList;
        }

        /// <summary>
        /// Todo１データを削除する
        /// </summary>
        /// <param name="id">対象ToDoのId</param>
        public void DeleteToDo(int id)
        {
            try
            {
                using (SqlCeTransaction trans = this.con.BeginTransaction())
                {
                    using (SqlCeCommand cmd = this.con.CreateCommand())
                    {
                        string sql = "UPDATE [ToDo] SET [deleted] = 1, [deletedtime] = @deletedtime WHERE [id] = @id;";

                        cmd.CommandText = sql;
                        cmd.Parameters.Add("id", System.Data.DbType.Int32);
                        cmd.Parameters.Add("deletedtime", System.Data.DbType.DateTime);

                        cmd.Parameters["id"].Value = id;
                        cmd.Parameters["deletedtime"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ToDoデータの更新
        /// </summary>
        /// <param name="data">ToDoデータ</param>
        public void UpdateTodo(TodoManager.DataType data)
        {
            try
            {
                using (SqlCeTransaction trans = this.con.BeginTransaction())
                {
                    using (SqlCeCommand cmd = this.con.CreateCommand())
                    {
                        string sql = @"UPDATE [ToDo] "
                                   + "SET deadline = @deadline, contents = @contents, tobedetermined = @tobedetermined, updatetime = @updatetime "
                                   + "WHERE id = @id;";
                        cmd.CommandText = sql;

                        cmd.Parameters.Add("deadline", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("contents", System.Data.DbType.String);
                        cmd.Parameters.Add("tobedetermined", System.Data.DbType.Int32);
                        cmd.Parameters.Add("updatetime", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("id", System.Data.DbType.Int32);

                        cmd.Parameters["deadline"].Value = data.Deadline.ToString("yyyy-MM-dd");
                        cmd.Parameters["contents"].Value = data.Contents;
                        cmd.Parameters["tobedetermined"].Value = data.ToBeDetermined;
                        cmd.Parameters["updatetime"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        cmd.Parameters["id"].Value = data.Id;

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// TODOデータをDBに登録する
        /// </summary>
        /// <param name="data">Todoのデータ</param>
        /// <returns>dbに登録した際のid番号を返す</returns>
        public int InsertToDo(TodoManager.DataType data)
        {
            int id = 0;

            try
            {
                using (SqlCeTransaction trans = this.con.BeginTransaction())
                {
                    using (SqlCeCommand cmd = this.con.CreateCommand())
                    {
                        string sql = @"INSERT INTO [ToDo] (deadline, contents, tobedetermined, createtime, updatetime, deletedtime, deleted) "
                                   + @"values (@deadline, @contents, @tobedetermined, @createtime, @updatetime, @deletedtime, @deleted);";
                        cmd.CommandText = sql;

                        cmd.Parameters.Add("deadline", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("contents", System.Data.DbType.String);
                        cmd.Parameters.Add("tobedetermined", System.Data.DbType.Int32);
                        cmd.Parameters.Add("createtime", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("updatetime", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("deletedtime", System.Data.DbType.String);
                        cmd.Parameters.Add("deleted", System.Data.DbType.Int32);

                        cmd.Parameters["deadline"].Value = data.Deadline.ToString("yyyy-MM-dd");
                        cmd.Parameters["contents"].Value = data.Contents;
                        cmd.Parameters["tobedetermined"].Value = data.ToBeDetermined;
                        cmd.Parameters["createtime"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        cmd.Parameters["updatetime"].Value = new DateTime(0).ToString("yyyy-MM-dd");
                        cmd.Parameters["deletedtime"].Value = DateTime.Now.ToString("yyyy-MM-dd");
                        cmd.Parameters["deleted"].Value = 0;

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        trans.Commit();

                        // 挿入したidを何かしらで取得する
                        cmd.CommandText = "SELECT  MAX(id) FROM ToDo;";
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch
            {
                throw;
            }

            return id;
        }

        /// <summary>
        /// 本日完了したタスク
        /// </summary>
        /// <returns>完了タスクのリスト</returns>
        public List<TodoManager.CompleteDataType> CompleteTodayTask()
        {
            // 本日削除したデータを取得
            return this.CompleteAnyPeriodTask(DateTime.Now, DateTime.Now);
        }

        /// <summary>
        /// １週間以内に完了したタスク
        /// </summary>
        /// <returns>完了タスクのリスト</returns>
        public List<TodoManager.CompleteDataType> Complete1WeekTask()
        {
            // １週間前から本日までに削除したデータを取得
            return this.CompleteAnyPeriodTask(DateTime.Now.AddDays(-6), DateTime.Now);
        }

        /// <summary>
        /// 指定された期限に完了したタスク
        /// </summary>
        /// <param name="startTime">開始日</param>
        /// <param name="endTime">終了日</param>
        /// <returns>完了タスクのリスト</returns>
        public List<TodoManager.CompleteDataType> CompleteAnyPeriodTask(DateTime startTime, DateTime endTime)
        {
            // 開始期日 <= 終了期日かチェック
            if (endTime < startTime)
            {
                throw new ArgumentException("終了期日 < 開始期日になっています");
            }

            List<TodoManager.CompleteDataType> dataList = new List<TodoManager.CompleteDataType>();

            string sql = "SELECT [id], [deadline], [contents], [tobedetermined], [deletedtime] " +
                         "From [ToDo] " +
                         "WHERE [deleted] = 1 and @startTime <= [deletedtime] AND [deletedtime] <= @endTime " +
                         "ORDER BY [deletedtime] DESC, [deadline] DESC";

            using (SqlCeCommand cmd = this.con.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.Add("startTime", System.Data.DbType.String);
                cmd.Parameters.Add("endTime", System.Data.DbType.String);

                cmd.Parameters["startTime"].Value = startTime.ToString("yyyy-MM-dd");
                cmd.Parameters["endTime"].Value = endTime.ToString("yyyy-MM-dd");
                cmd.Prepare();

                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TodoManager.CompleteDataType d = new TodoManager.CompleteDataType();
                        d.Contents = reader["contents"].ToString();
                        d.Deadline = Convert.ToDateTime(reader["deadline"]);
                        d.Deletedtime = Convert.ToDateTime(reader["deletedtime"]);
                        d.Id = Convert.ToInt32(reader["id"]);
                        d.ToBeDetermined = (Convert.ToInt32(reader["tobedetermined"]) == 0) ? false : true;

                        dataList.Add(d);
                    }
                }
            }

            return dataList;
        }

        /// <summary>
        /// 完了したタスクを元に戻す
        /// </summary>
        /// <param name="id">元に戻す完了したタスクのID</param>
        public void RestoreCompleteTask(int id)
        {
            try
            {
                using (SqlCeTransaction trans = this.con.BeginTransaction())
                {
                    using (SqlCeCommand cmd = this.con.CreateCommand())
                    {
                        string sql = @"UPDATE [ToDo] "
                                   + "SET [deleted] = @deleted, [deletedtime] = @deletedtime "
                                   + "WHERE id = @id;";
                        cmd.CommandText = sql;

                        cmd.Parameters.Add("deleted", System.Data.DbType.Int32);
                        cmd.Parameters.Add("deletedtime", System.Data.DbType.DateTime);
                        cmd.Parameters.Add("id", System.Data.DbType.Int32);

                        cmd.Parameters["deleted"].Value = 0;
                        cmd.Parameters["deletedtime"].Value = new DateTime(0).ToString("yyyy-MM-dd");
                        cmd.Parameters["id"].Value = id;

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}

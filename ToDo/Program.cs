﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ToDo
{
    /// <summary>
    /// エントリポイントのクラス
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

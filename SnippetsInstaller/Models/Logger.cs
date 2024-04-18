using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippetsInstaller.Models
{
    public abstract class Logger
    {
        /// <summary>
        /// メッセージを表示し、ログファイルにログを追記します。
        /// </summary>
        /// <param name="str"></param>
        public static void Show(string str)
        {
            //ログ出力
            if (!Directory.Exists($@".\logs"))
            {
                Directory.CreateDirectory($@".\logs");
            }
            string path = $@".\logs\MessageLog.log";

            DateTime dateTime = DateTime.Now;
            string content = $@"{dateTime}: {str}";
            Service.WriteFileAdd(content, path);

            //メッセージを表示
            MessageBox.Show(str);
        }
    }
}

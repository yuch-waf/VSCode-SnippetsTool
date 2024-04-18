using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Prism.Ioc;
using Prism.Services.Dialogs;
using SnippetsInstaller.Interface;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Prism.Regions;

namespace SnippetsInstaller.Models
{
    public class Service : IService
    {
        #nullable enable

        public Service()
        {
        }

        /// <summary>
        /// エクスプローラを開き、フォルダ名を含めたフォルダのパスを取得します。
        /// </summary>
        /// <returns>string</returns>
        public static string OpenFilePass()
        {

            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using CommonOpenFileDialog op = new()
            {
                Title = "Select Folder",
                InitialDirectory = $@"{myDocuments}",
                IsFolderPicker = true,
            };

            if (op.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return "";
            }

            return op.FileName;
        }

        /// <summary>
        /// pathフォルダの有無を確認します。<br></br>
        /// フォルダが無い場合にメッセージを表示し、falseを返します。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Logger.Show("Error. Missing Folder.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// pathに文字列contentを書き込みます。<br></br>
        /// 上書き処理
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="s"></param>
        internal static void WriteFile(string content, string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (path == null) { return; }
            try
            {
                using StreamWriter streamWriter = new(path, false);
                {
                    streamWriter.WriteLine(content);
                }
                return;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// pathに文字列contentを書き込みます。<br></br>
        /// 追加処理
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="s"></param>
        internal static void WriteFileAdd(string content, string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (path == null) { return; }
            try
            {
                using StreamWriter streamWriter = new(path, true);
                {
                    streamWriter.WriteLine(content);
                }
                return;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// pathファイルの内容を読み取ります。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string? ReadFile(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (path == null) { return null; }
            try
            {
                using StreamReader streamReader = new(path, false);
                string? content;
                {
                    content = streamReader.ReadToEnd().TrimEnd();
                }
                return content;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// pathフォルダのファイルをVSCodeのSnippetフォルダにコピーします。
        /// </summary>
        /// <param name="path"></param>
        public static void Import(string path)
        {
            if (path == string.Empty)
            {
                return;
            }

            //フォルダ作成
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!Directory.Exists($@"{userProfile}\AppData\Roaming\Code\User\snippets"))
            {
                Directory.CreateDirectory($@"{userProfile}\AppData\Roaming\Code\User\snippets");
                Logger.Show($@"Created Directory{"\n"}{userProfile}\AppData\Roaming\Code\User\snippets");
            }

            //ファイルのパスを生成
            List<string> files = GetListDirectory.GetList(path);

            //ファイルをコピー
            foreach (string file in files)
            {
                File.Copy(file, @$"{userProfile}\AppData\Roaming\Code\User\snippets\{Path.GetFileName(file)}", true);
            }

            if (files.Count > 0)
            {
                Logger.Show("Import Succeed.");
            }
            else
            {
                Logger.Show("Failed. Missing File.");
            }
        }

        /// <summary>
        /// VSCodeのSnippetフォルダ内のファイルをpathフォルダにコピーします。
        /// </summary>
        /// <param name="path"></param>
        public static void Export(string path)
        {
            if (path == string.Empty)
            {
                return;
            }

            //フォルダがあるかチェック
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!CheckPath($@"{userProfile}\AppData\Roaming\Code\User\snippets"))
            {
                return;
            }

            if (!CheckPath($@"{path}"))
            {
                return;
            }

            if (!Directory.Exists($@"{path}\snippets"))
            {
                Directory.CreateDirectory($@"{path}\snippets");
            }

            //ファイルのパスを生成
            List<string> files = GetListDirectory.GetList($@"{userProfile}\AppData\Roaming\Code\User\snippets");

            //ファイルをコピー
            foreach (string file in files)
            {
                File.Copy(@$"{userProfile}\AppData\Roaming\Code\User\snippets\{Path.GetFileName(file)}", $@"{path}\snippets\{Path.GetFileName(file)}", true);
            }

            if (files.Count > 0)
            {
                Logger.Show("Export Succeed.");
            }

            else
            {
                Logger.Show("Failed. Missing FileTool.");
            }
        }

    }
}

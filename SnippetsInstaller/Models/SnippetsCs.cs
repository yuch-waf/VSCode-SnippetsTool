using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippetsInstaller.Models
{
    internal class SnippetsCs
    {
        #nullable enable
        #region C#

        /// <summary>
        /// Custom C#でOKボタンを押した際の処理です。<br></br>
        /// 入力値、出力先フォルダ、出力先ファイルをそれぞれチェックし、不正があれば終了します。<br></br>
        /// 出力先ファイルがあれば一度読み込み、最後尾の文字を消してから出力し、最後尾の文字を再度出力します。
        /// </summary>
        /// <param name="csTitle">タイトルの入力値</param>
        /// <param name="csPrefix">予測の入力値</param>
        /// <param name="csDescription">説明分の入力値</param>
        /// <param name="_csBody">ボディの入力値</param>
        internal static void CsOK(string csTitle, string csPrefix, string csDescription, string _csBody)
        {
            //入力チェック
            string inputContents = string.Empty;
            if (string.IsNullOrEmpty(csTitle))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Title";
            }
            if (string.IsNullOrEmpty(csPrefix))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Prefix";
            }
            if (string.IsNullOrEmpty(csDescription))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Description";
            }
            if (string.IsNullOrEmpty(_csBody))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Body";
            }
            if (!string.IsNullOrEmpty(inputContents))
            {
                Logger.Show($"{inputContents} is Empty.");
                return;
            }

            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string csBody = GenCsBody(_csBody);
            string code = GenCsCode(csTitle, csPrefix, csDescription, csBody);
            string path = @$"{userProfile}\AppData\Roaming\Code\User\snippets\{"csharp.json"}";

            //フォルダがあるかチェック
            if (!Service.CheckPath($@"{userProfile}\AppData\Roaming\Code\User\snippets"))
            {
                return;
            }

            //ファイルを読み込む
            string? readContent = Service.ReadFile(path);
            if (readContent == null || readContent == string.Empty)
            {
                readContent = "{" + "\n" + "}";
            }
            if (readContent[^1] == '}')
            {
                readContent = readContent.Remove(readContent.Length - 1);
            }
            else
            {
                Logger.Show("Error\n.");
                return;
            }

            string content = $@"{readContent}{code}{"\n}"}";

            Service.WriteFile(content, path);
            Logger.Show("Write File Succees.");
            return;
        }

        /// <summary>
        /// コードをJson形式に整形します。
        /// </summary>
        /// <param name="csTitle">タイトルの入力値</param>
        /// <param name="csPrefix">予測の入力値</param>
        /// <param name="csDescription">説明分の入力値</param>
        /// <param name="csBody">ボディの入力値</param>
        /// <returns></returns>
        private static string GenCsCode(string csTitle, string csPrefix, string csDescription, string csBody)
        {
            string code =
                $@"""{csTitle}"": {{{"\n"}""prefix"": ""{csPrefix}"",{"\n"}""body"": [{"\n"}{csBody}],{"\n"}""description"": ""{csDescription}""{"\n"}}},{"\n"}";
            return code;
        }

        /// <summary>
        /// ボディを整形します。
        /// </summary>
        /// <param name="_csBody">ボディの入力値</param>
        /// <returns></returns>
        private static string GenCsBody(string _csBody)
        {
            string csBody = _csBody.Replace(@"""", $@"\""").Replace("\r\n", $@""",{"\n"}""");
            return $@"""{csBody}""{"\n"}";
        }
        #endregion

    }
}

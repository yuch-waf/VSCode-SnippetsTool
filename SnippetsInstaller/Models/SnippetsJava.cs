using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippetsInstaller.Models
{
    internal class SnippetsJava
    {
#nullable enable
        #region Java

        /// <summary>
        /// Custom JavaでOKボタンを押した際の処理です。<br></br>
        /// 入力値、出力先フォルダ、出力先ファイルをそれぞれチェックし、不正があれば終了します。<br></br>
        /// 出力先ファイルがあれば一度読み込み、最後尾の文字を消してから出力し、最後尾の文字を再度出力します。
        /// </summary>
        /// <param name="javaTitle">タイトルの入力値</param>
        /// <param name="javaPrefix">予測の入力値</param>
        /// <param name="javaDescription">説明分の入力値</param>
        /// <param name="_javaBody">ボディの入力値</param>
        internal static void JavaOK(string javaTitle, string javaPrefix, string javaDescription, string _javaBody)
        {
            //入力チェック
            string inputContents = string.Empty;
            if (string.IsNullOrEmpty(javaTitle))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Title";
            }
            if (string.IsNullOrEmpty(javaPrefix))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Prefix";
            }
            if (string.IsNullOrEmpty(javaDescription))
            {
                if (!string.IsNullOrEmpty(inputContents))
                {
                    inputContents += ", ";
                }
                inputContents += "Description";
            }
            if (string.IsNullOrEmpty(_javaBody))
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
            string javaBody = GenJavaBody(_javaBody);
            string code = GenJavaCode(javaTitle, javaPrefix, javaDescription, javaBody);
            string path = @$"{userProfile}\AppData\Roaming\Code\User\snippets\{"java.json"}";

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
        /// <param name="javaTitle">タイトルの入力値</param>
        /// <param name="javaPrefix">予測の入力値</param>
        /// <param name="javaDescription">説明分の入力値</param>
        /// <param name="javaBody">ボディの入力値</param>
        /// <returns></returns>
        private static string GenJavaCode(string javaTitle, string javaPrefix, string javaDescription, string javaBody)
        {
            string code =
                $@"""{javaTitle}"": {{{"\n"}""prefix"": ""{javaPrefix}"",{"\n"}""body"": [{"\n"}{javaBody}],{"\n"}""description"": ""{javaDescription}""{"\n"}}},{"\n"}";
            return code;
        }

        /// <summary>
        /// ボディを整形します。
        /// </summary>
        /// <param name="_javaBody"></param>
        /// <returns></returns>
        private static string GenJavaBody(string _javaBody)
        {
            string javaBody = _javaBody.Replace(@"""", $@"\""").Replace("\r\n", $@""",{"\n"}""");
            return $@"""{javaBody}""{"\n"}";
        }
        #endregion

    }
}

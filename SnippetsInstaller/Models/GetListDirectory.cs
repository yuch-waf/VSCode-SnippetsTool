using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetsInstaller.Models
{
    // なぜかここが原因でドキュメント生成に失敗するので隔離
    internal class GetListDirectory
    {
        /// <summary>
        /// path\直下のファイルパスをリストにして返します。
        /// </summary>
        public static List<string> GetList(string path)
        {
            return new(Directory.GetFiles($@"{path}\").ToList());
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SnippetsInstaller.Models;
using System;
using System.Collections.Generic;

namespace SnippetsInstaller.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        #region プロパティ

        /// <summary>
        /// ウィンドウのタイトルです。
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _title = "VSCode Snippets Tool";

        /// <summary>
        /// 機能未実装
        /// 記入欄のフォントサイズを変更するプルダウンの項目です。<br></br>
        /// </summary>
        public List<int> SourceFontSize
        {
            get { return _sourceFontSize; }
            set { SetProperty(ref _sourceFontSize, value); }
        }
        private List<int> _sourceFontSize = new()
        {
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,
        };

        /// <summary>
        /// File ToolのImport欄に表示するテキストです。<br></br>
        /// そのままパスとして使用しています。
        /// </summary>
        public string ImportText
        {
            get { return _importText; }
            set { SetProperty(ref _importText, value); }
        }
        private string _importText = string.Empty;

        /// <summary>
        /// File ToolのExport欄に表示するテキストです。<br></br>
        /// そのままパスとして使用しています。
        /// </summary>
        public string ExportText
        {
            get { return _exportText; }
            set { SetProperty(ref _exportText, value); }
        }
        private string _exportText = string.Empty;

        /// <summary>
        /// 記入欄のフォントサイズです。
        /// </summary>
        public double FontSize
        {
            get { return _fontSize; }
            set { SetProperty(ref _fontSize, value); }
        }
        private double _fontSize = 12;

        /// <summary>
        /// UserControllに使用します。
        /// </summary>
        public IRegionManager RegionManager { get; }

        /// <summary>
        /// 画面遷移に使用します。
        /// </summary>
        public DelegateCommand<string> WindowChange { get; }

        /// <summary>
        /// ボタンを押した際の処理に使用します。
        /// </summary>
        public DelegateCommand<string> ButtonCommand { get; }

        #endregion

        #region C# プロパティ

        /// <summary>
        /// C#の予測の入力値です。
        /// </summary>
        public string CsPrefix { get; set; }

        /// <summary>
        /// C#の説明分の入力値です。
        /// </summary>
        public string CsDescription { get; set; }

        /// <summary>
        /// C#のボディの入力値です。
        /// </summary>
        public string CsBody { get; set; }

        /// <summary>
        /// C#のタイトルの入力値です。
        /// </summary>
        public string CsTitle { get; set; }

        #endregion

        #region Java プロパティ

        /// <summary>
        /// Javaの予測の入力値です。
        /// </summary>
        public string JavaPrefix { get; set; }

        /// <summary>
        /// Javaの説明分の入力値です。
        /// </summary>
        public string JavaDescription { get; set; }

        /// <summary>
        /// Javaのボディの入力値です。
        /// </summary>
        public string JavaBody { get; set; }

        /// <summary>
        /// Javaのタイトルの入力値です。
        /// </summary>
        public string JavaTitle { get; set; }

        #endregion

        #region コンストラクタ
        
        /// <summary>
        /// コンストラクタです。<br></br>
        /// RegionManager, DelegateCommandなどを利用可能にします。
        /// </summary>
        /// <param name="regionManager"></param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            WindowChange = new(ExecuteWindowChange);
            ButtonCommand = new(ExcuteButton);
            RegionManager.RequestNavigate("ContentRegion", "FileTool");
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ボタンを押した際の処理です。<br></br>
        /// View xamlの CommandParameterに記述したstringによって処理をスイッチします。<br></br>
        /// タイプミス対策にEnumを用いています。
        /// </summary>
        /// <param name="button">xaml.CommandParameter</param>
        public void ExcuteButton(string button)
        {
            switch (Enum.Parse(typeof(EnumButtons),button))
            {
                case EnumButtons.ImportButton: 
                    ImportText = Service.OpenFilePass(); break;
                case EnumButtons.ExportButton:
                    ExportText = Service.OpenFilePass(); break;
                case EnumButtons.ImportOK:
                    Service.Import(ImportText); break;
                case EnumButtons.ExportOK:
                    Service.Export(ExportText); break;
                case EnumButtons.CsOK:
                    SnippetsCs.CsOK(CsTitle, CsPrefix, CsDescription, CsBody); break;
                case EnumButtons.JavaOK:
                    SnippetsJava.JavaOK(JavaTitle, JavaPrefix, JavaDescription, JavaBody);break;
                default: break;
            }
        }

        /// <summary>
        /// RegionManagerを利用した画面遷移処理です。<br></br>
        /// View xamlの CommandParameterに記述したstringによって処理をスイッチします。<br></br>
        /// windowNameと同じ名前のUserControll、ウィンドウを表示します。
        /// </summary>
        /// <param name="windowName">CommandParameter</param>
        public void ExecuteWindowChange(string windowName)
        {
            RegionManager.RequestNavigate("ContentRegion", windowName);
        }

        #endregion
    }
}

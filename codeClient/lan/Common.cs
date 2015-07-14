using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace nsVicoClient
{
    class Common
    {
        private static string currentLanguageFile = "lan/lanCN.xaml";

        /// <summary>
        /// 获取或设置当前程序使用语言的资源文件
        /// </summary>
        public static string CurrentLanguageFile
        {
            get { return currentLanguageFile; }
            set
            {
                currentLanguageFile = value;
                LoadLanguage(currentLanguageFile);
            }
        }
        public static lanType lan
        {
            set
            {
                switch (value)
                {
                    case lanType.lanCN:
                        CurrentLanguageFile = "lan/lanCN.xaml";
                        Properties.Settings.Default.lan = 1;
                        break;
                    case lanType.lanEN:
                        CurrentLanguageFile = "lan/lanEN.xaml";
                        Properties.Settings.Default.lan = 2;
                        break;
                    default:
                        break;
                }
                Properties.Settings.Default.Save();
            }
            get
            {
                if (currentLanguageFile == "lan/lanCN.xaml")
                    return lanType.lanCN;
                else if (currentLanguageFile == "lan/lanEN.xaml")
                    return lanType.lanEN;
                else
                    return lanType.lanCN;
                         
            }
        }
        /// <summary>
        /// 根据资源文件切换当前应用程序使用的语言
        /// </summary>
        /// <param name="currentLanguageFile"></param>
        private static void LoadLanguage(string currentLanguageFile)
        {
            var rd = new ResourceDictionary() { Source = new Uri(currentLanguageFile, UriKind.RelativeOrAbsolute) };

            if (Application.Current.Resources.MergedDictionaries.Count == 0)
                Application.Current.Resources.MergedDictionaries.Add(rd);
            else
                Application.Current.Resources.MergedDictionaries[0] = rd;
        }
    }

    public enum lanType : byte
    {
        lanCN = 1,
        lanEN = 2
    }
}

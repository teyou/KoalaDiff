using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoalaDiff.Library
{
    /// <summary>
    /// A wrapper class to access setting value
    /// </summary>
    public static class SettingsHelper
    {
        private static bool _enableCache = Properties.Settings.Default.EnableCache;
        public static bool EnableCache
        {
            get { return _enableCache; }
            set
            {
                Properties.Settings.Default.EnableCache = _enableCache = value;
                if (_enableCache == false) CacheHelper.RemoveAllCache();
                SaveSetting();
            }
        }

        private static string _firstPath = Properties.Settings.Default.FirstPath;
        public static string FirstPath
        {
            get { return _firstPath; }
            set
            {
                Properties.Settings.Default.FirstPath = _firstPath = value;
                SaveSetting();
            }
        }

        private static string _secondPath = Properties.Settings.Default.SecondPath;
        public static string SecondPath
        {
            get { return _secondPath; }
            set
            {
                Properties.Settings.Default.SecondPath = _secondPath = value;
                SaveSetting();
            }
        }

        private static string _leftDescription = Properties.Settings.Default.LeftDescription;
        public static string LeftDescription
        {
            get { return _leftDescription; }
            set
            {
                Properties.Settings.Default.LeftDescription = _leftDescription = value;
                SaveSetting();
            }
        }

        private static string _rightDescription = Properties.Settings.Default.RightDescription;
        public static string RightDescription
        {
            get { return _rightDescription; }
            set
            {
                Properties.Settings.Default.RightDescription = _rightDescription = value;
                SaveSetting();
            }
        }

        private static void SaveSetting()
        {
            Properties.Settings.Default.Save();
        }
    }
}

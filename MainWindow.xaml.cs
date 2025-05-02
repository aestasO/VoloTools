using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using MahApps.Metro.Controls;
using System.IO;

namespace volotools
{
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            SideMenu.MainTabs = MainTabs;

        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e) //言語切替（仮）
        {
            // 日本語に切り替え
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja");

            // 画面を再読み込みして言語を更新
            Application.Current.MainWindow.Content = null;
            InitializeComponent(); // 言語変更後に画面を再初期化


        }
        public static async Task RunUpdaterAsync()
        {
            string updateUrl = "https://yourserver.com/updates/latest.zip";
            string tempZipPath = Path.Combine(Path.GetTempPath(), "update.zip");
            string updaterExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe");

            // 1. ZIP をダウンロード
            using (HttpClient client = new HttpClient())
            {
                var data = await client.GetByteArrayAsync(updateUrl);
                await File.WriteAllBytesAsync(tempZipPath, data);
            }

            // 2. Updater.exe を起動
            if (File.Exists(updaterExePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = updaterExePath,
                    Arguments = $"\"{tempZipPath}\"",
                    UseShellExecute = true
                });

                // 3. 自分自身を終了
                Environment.Exit(0);
            }
            else
            {
                // エラー処理
                Console.WriteLine("Updater.exe が見つかりません。");
            }
        }
    }
}
using System.Windows;
using System.Globalization;
using System.Threading;

public partial class App : Application
{
    public App()
    {
        var cultureInfo = new CultureInfo("ja-JP"); // 日本語に設定
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
    }
}

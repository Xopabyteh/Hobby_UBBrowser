using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Hobby_UBBrowser.Services;

public class BrowserService : IDisposable
{
    private readonly ChromeDriver _driver;
    public BrowserService()
    {
        // Start new chrome selenium
        var chromeOptions = new ChromeOptions();
        _driver = new ChromeDriver(chromeOptions);
    }

    public Task NavigateAsync(string url)
    {
        return _driver.Navigate().GoToUrlAsync(url);
    }

    public string GetCurrentUrl() => _driver.Url;

    public Screenshot? GetScreenshot()
    {
        return _driver.GetScreenshot();
    }

    public void Click(double xPercent, double yPercent)
    {
        var windowSize = _driver.Manage().Window.Size;
        var x = (int)(windowSize.Width * xPercent);
        var y = (int)(windowSize.Height * yPercent);

        var action = new Actions(_driver);
        action
            .MoveToLocation(x, y)
            .Click()
            .Perform();
    }

    public void Init()
    {
        _driver.Navigate().GoToUrl("https://www.google.com");
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}
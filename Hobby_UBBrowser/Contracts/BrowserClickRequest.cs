namespace Hobby_UBBrowser.Contracts;

/// <summary>
/// At what portion of the screen did the user click in percentage
/// 0, 0 = top left
/// 1, 1 = bottom right
/// </summary>
/// <param name="XPercent"></param>
/// <param name="YPercent"></param>
public record BrowserClickRequest(double XPercent, double YPercent);
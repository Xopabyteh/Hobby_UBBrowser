using Hobby_UBBrowser.Contracts;
using Hobby_UBBrowser.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hobby_UBBrowser.API;

public static class AppEndpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        // Navigates to a page and returns a serialized screenshot
        app.MapPost("/api/query", async (
            [FromBody] BrowserQueryRequest request,
            BrowserService browser) =>
        {
            await browser.NavigateAsync(request.Url);
            var screenshot = browser.GetScreenshot();

            var responseData = await BrowserStateResponseData.SerializeAsync(
                screenshot!.AsByteArray,
                request.Width,
                request.Height,
                request.Url);

            return Results.Ok(responseData);
        });

        // Creates a screenshot and sends it back
        app.MapPost("/api/screen", async (
            [FromBody] BrowserScreenRequest request,
            BrowserService browser) =>
        {
            var screenshot = browser.GetScreenshot();
            var responseData = await BrowserStateResponseData.SerializeAsync(
                screenshot!.AsByteArray,
                request.Width,
                request.Height,
                browser.GetCurrentUrl());

            return Results.Ok(responseData);
        });

        app.MapPost("/api/click", (
            [FromBody] BrowserClickRequest request,
            BrowserService browser) =>
        {
            browser.Click(request.XPercent, request.YPercent);

            return Results.Ok();
        });
    }
}
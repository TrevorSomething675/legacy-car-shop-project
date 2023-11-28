using MainTz.AuthApi.Services.Abstractions;
using Extensions.SettingsModels;
using MainTz.AuthApi.Services;
using Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITokenService, TokenService>();
var authSettings = Settings.Load<AuthSettings>("AuthSettings");

var app = builder.Build();

app.Run(async (context) =>
{
	if(context.Request.Path == "/GetTokens")
	{
		using StreamReader reader = new StreamReader(context.Request.Body);
		string role = await reader.ReadToEndAsync();
		var tokenService = app.Services.GetRequiredService<ITokenService>();

		var accessToken = tokenService.CreateAccessToken(role);
        var refreshToken = tokenService.CreateRefreshToken(role);

		await context.Response.WriteAsJsonAsync(new {accessToken, refreshToken, role});
	}
	if(context.Request.Path == "/GetTokensWithRefresh")
	{
        using StreamReader reader = new StreamReader(context.Request.Body);
        string role = await reader.ReadToEndAsync();
        var tokenService = app.Services.GetRequiredService<ITokenService>();

        var accessToken = tokenService.CreateAccessToken(role);
        var refreshToken = tokenService.CreateRefreshToken(role);

        await context.Response.WriteAsJsonAsync(new { accessToken, refreshToken, role });
    }
});

app.Run();
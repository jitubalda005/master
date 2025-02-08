using System;

public class TenantResolver
{
	
	private readonly RequestDelegate _next
	public TenantResolver(RequestDelegate next)
	{
		_next=next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		context.Request.Headers.TryGetValue("tenant", out var tenantfromHeader);
		if (string.IsNullOrEmpty(tenantfromHeader) == false)
		{

		}
		await _next(context);

	}
}

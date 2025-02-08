using System;
using Domain.Interfaces.Master;

public class TenantResolver
{

	private readonly RequestDelegate _next;
	public TenantResolver(RequestDelegate next)
	{
		_next=next;
	}

	public async Task InvokeAsync(HttpContext context, ICurrentTenantRespostory currentTenantRespostory)
	{
		context.Request.Headers.TryGetValue("tenant", out var tenantfromHeader);
		if (string.IsNullOrEmpty(tenantfromHeader) == false)
		{
			await currentTenantRespostory.SetTenant(tenantfromHeader);
        }
		await _next(context);

	}
}

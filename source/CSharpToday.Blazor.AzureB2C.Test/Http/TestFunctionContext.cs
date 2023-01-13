using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;

namespace CSharpToday.Blazor.AzureB2C.Test.Http;

internal class TestFunctionContext : FunctionContext
{
    public override string InvocationId { get; }

    public override string FunctionId { get; }

    public override TraceContext TraceContext { get; }

    public override BindingContext BindingContext { get; }

    public override RetryContext RetryContext { get; }

    public override IServiceProvider InstanceServices { get; set; }

    public override FunctionDefinition FunctionDefinition { get; }

    public override IDictionary<object, object> Items { get; set; }

    public override IInvocationFeatures Features { get; }
}

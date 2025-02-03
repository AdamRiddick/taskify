namespace Taskify.UnitTests.TestHelpers.Objects;

using MediatR;

public class TestRequest : IRequest<TestResponse>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
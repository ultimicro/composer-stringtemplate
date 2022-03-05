namespace Composer.StringTemplate.Tests;

using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using Xunit;

public sealed class TemplateTests
{
    private static readonly Guid Id = new("92132935-5fe3-45a8-9ecd-f9a52f6eb5c9");

    [Fact]
    public async Task BuildSubjectAsync_WithCompletedInput_ShouldReturnCorrectResult()
    {
        // Arrange.
        var plain = @"
subject(recipients, data) ::= <<
Hi $first(recipients); format=""name""$,

$data.foo$.
>>";
        var sender = new MailboxAddress("John Doe", "djohn@example.com");
        var recipient = new MailboxAddress("John Roe", "rjohn@example.com");
        var data = new
        {
            Foo = "abc",
        };
        var subject = new Template(Id, CultureInfo.InvariantCulture, sender, plain, Enumerable.Empty<IPropertyRenderer>());

        // Act.
        var result = await subject.BuildSubjectAsync(new[] { recipient }, data);

        // Assert.
        var expected = @"Hi John Roe,

abc.";
        Assert.Equal(expected, result);
    }
}

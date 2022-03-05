# Template provider for Composer using StringTemplate 4

This is a template provider for [Composer](https://github.com/ultimicro/composer) using [StringTemplate 4](https://www.stringtemplate.org).

## Usage

```csharp
services
    .AddComposer()
    .AddSender<SenderImplementation>()
    .AddStringTemplate()
    .AddTemplateRepository<RepositoryImplementation>();
```

`RepositoryImplementation` is an implementation of `ITemplateRepository`.

### Template syntax

We use [formal syntax](https://github.com/antlr/stringtemplate4/blob/master/doc/cheatsheet.md#groups) (AKA. group syntax) with `$` as delimiter. You need to
define the following templates for each email:

- `subject(recipients, data)`: Subject template.
- `plain(recipients, data)`: Plain text body message.

Example:

```
subject(recipients, data) ::= "Welcome to our awesome website!"
plain(recipients, data) ::= <<
Hello $data.username$!,

Thank you for sign up with us. You email address is $first(recipients); format="address"$.
>>
```

#### Template parameters

- `recipients`: A collection of recipients. Each items is is either `MimeKit.MailboxAddress` or `MimeKit.GroupAddress`.
- `data`: Data provided from class definition of the email.

### Property renderer

You can alter how data is rendered by create an implementation of `IPropertyRenderer`. See
[here](https://github.com/antlr/stringtemplate4/blob/master/doc/renderers.md) for a guide. The default behavior is using output from `ToString`. Once finished
you need to register your renderer by invoke `AddPropertyRenderer`:

```csharp
services
    .AddComposer()
    .AddSender<SenderImplementation>()
    .AddStringTemplate()
    .AddTemplateRepository<RepositoryImplementation>()
    .AddPropertyRenderer<YourRenderer>();
```

### Built-in property renderers

#### MimeKit.MailboxAddress

Available formats:

- `name`: Output only display name of the address (e.g. John Doe). Be aware that this may produce empty string in most cases due to usually email address does
  not contain display name.
- `address`: Output only address (e.g. john@example.com).
- `full`: Output both (e.g. John Doe <john@example.com>).

## License

MIT

# ReasonableRTF - Fast RTF to Plain Text Converter 🚀

A lightweight and performant C# library designed for rapidly converting **Rich Text Format (RTF)** files into **plain text**.

- - -

## Installation

Install the library via NuGet:

```
dotnet add package ReasonableRTF.Standard
```

- - -

## Quick Start

Converting an RTF file (as a byte array) to plain text is straightforward. The `Convert` method returns an **`RtfResult`** object, providing the converted text and comprehensive error information, if any.

### Basic Conversion

Here is how to convert an RTF byte array to a plain text string:

```cs
using ReasonableRTF;
using ReasonableRTF.Models;
using System.IO;

// ...

// 1. Initialize the converter
RtfToTextConverter converter = new RtfToTextConverter();

// 2. Load the RTF data (e.g., from a file) and convert
RtfResult result = converter.Convert(File.ReadAllBytes(Context.Path));

if (result.Error == RtfError.OK)
{
    // Conversion was successful
    string plainText = result.Text;
    Console.WriteLine("Converted Text:\n" + plainText);
}
else
{
    // Handle conversion errors
    Console.WriteLine($"Conversion Error: {result.Error} at byte position: {result.BytePositionOfError}");
    if (result.Exception != null)
    {
        Console.WriteLine($"Exception Details: {result.Exception.Message}");
    }
}
```

- - -

## The RtfResult Class

The `RtfResult` object provides full details about the conversion process, ensuring you can robustly handle success and failure cases.

| Property | Type | Description |
| --- | --- | --- |
| `Text` | `string` | The converted plain text. |
| `Error` | `RtfError` | The error code. This will be `RtfError.OK` upon successful conversion. |
| `BytePositionOfError` | `int` | The approximate position in the data stream where the error occurred, or `-1` if no error. |
| `Exception` | `Exception?` | The caught exception, or `null` if no exception occurred during conversion. |

- - -

## Conversion Options

For more control over the output, you can provide an instance of the **`RtfToTextConverterOptions`** class to the `Convert` method. This allows customization of line breaks, special character handling, and hidden text inclusion.

### Applying Options

```cs
RtfToTextConverter converter = new RtfToTextConverter();
RtfToTextConverterOptions options = new RtfToTextConverterOptions
{
    ConvertHiddenText = true, // Include text marked as hidden
    LineBreakStyle = LineBreakStyle.LF // Use Unix-style line breaks
};

RtfResult result = converter.Convert(File.ReadAllBytes(Context.Path), options);
// ... check result
```

### RtfToTextConverterOptions Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `SwapUppercaseAndLowercasePhiSymbols` | `bool` | `true` | If set to `true`, swaps the uppercase and lowercase Greek phi characters in the Symbol font translation. This addresses a common reversal in the Windows Symbol font. |
| `SymbolFontA0Char` | `SymbolFontA0Char` | `SymbolFontA0Char.EuroSign` | Sets the character at index 0xA0 (160) in the Symbol font to Unicode translation table. Important for compatibility with older Symbol font versions. |
| `LineBreakStyle` | `LineBreakStyle` | `LineBreakStyle.CRLF` | Specifies the line break style (CRLF or LF) for the converted plain text output. |
| `ConvertHiddenText` | `bool` | `false` | Determines whether text marked as **hidden** in the RTF file should be included in the plain text output. |

- - -

## License and Attribution

### Code License

The **original code** for this RTF converter was written by **Brian Tobin** and is licensed under the **MIT License** (Copyright (c) 2024 Brian Tobin). For the full license text, please refer to the [LICENSE file ReasonableRTF](https://github.com/FenPhoenix/ReasonableRTF/blob/main/LICENSE).
This project is a version of that code, adapted and converted to **.NET Standard**.
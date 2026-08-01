# SpectreConsoleLibrary Code Documentation

This document summarizes the classes and helper methods in the SpectreConsoleLibrary project.

## Project overview

The project is a small .NET library that uses Spectre.Console to render styled console UI elements, collect validated user input, and display formatted output.

## Source files

- [Core/Pill.cs](Core/Pill.cs)
- [Core/PillType.cs](Core/PillType.cs)
- [Core/Prompts.cs](Core/Prompts.cs)
- [Core/PromptValidations.cs](Core/PromptValidations.cs)
- [Core/SpectreConsoleHelpers.cs](Core/SpectreConsoleHelpers.cs)
- [Core/Utilities.cs](Core/Utilities.cs)

---

## 1. Pill

File: [Core/Pill.cs](Core/Pill.cs)

### Purpose
A sealed class that renders a pill-shaped console UI element using Spectre.Console.

### Responsibilities
- Stores the text content and visual style for the pill.
- Chooses a visual style based on the selected pill type.
- Measures the pill width for layout purposes.
- Renders the pill with optional Unicode caps.

### Key members
- Constructor: `Pill(string text, PillType type)`
  - Stores the text and resolves an appropriate style.
- `GetStyleForType(PillType type)`
  - Maps each pill type to a color combination.
- `Measure(RenderOptions options, int maxWidth)`
  - Calculates the width of the pill.
- `Render(RenderOptions options, int maxWidth)`
  - Produces the rendered segments for display.

### Notes
The pill uses rounded Unicode caps when supported and falls back to plain text when Unicode is unavailable.

---

## 2. PillType

File: [Core/PillType.cs](Core/PillType.cs)

### Purpose
An enum that defines available pill visual styles.

### Values
- `Success`
- `Warning`
- `Error`
- `Info`
- `Pink`

---

## 3. Prompts

File: [Core/Prompts.cs](Core/Prompts.cs)

### Purpose
A static helper class that provides prompt-based user input methods for common console scenarios.

### Key members
- `GetInt(string prompt = "Enter an integer")`
  - Prompts for an integer input.
- `GetDecimal(string prompt = "Enter a decimal")`
  - Prompts for a decimal input.
- `SecretPrompt(string text)`
  - Prompts for a secret or password-like value.
- `GetDate(string text = "Enter a date")`
  - Prompts for a date value and allows empty input.
- `GetBirthDate()`
  - Prompts for a birth date and validates that it is earlier than the year 2000.
- `GetBirthDate1(int year)`
  - Prompts for a birth date and validates it using the shared validation helper.
- `QuestionOptions`
  - A list containing the valid response options `Y` and `N`.
- `Continue(string questionText, string color)`
  - Prompts for a yes/no-style response and validates it.
- `Question(string questionText, string color = "white")`
  - Converts a continue prompt into a boolean result.
- `Get<T>(string prompt, T defaultValue)`
  - A generic prompt helper for custom input types.

### Notes
The class uses styled prompts and validation messages to create a more polished interactive console experience.

---

## 4. PromptValidations

File: [Core/PromptValidations.cs](Core/PromptValidations.cs)

### Purpose
A small internal helper class for input validation logic.

### Key member
- `ValidateDate(DateOnly dateOnly, int year)`
  - Rejects dates that are in the future.
  - Rejects dates that are in the specified year or later.
  - Returns a successful validation result when the date is acceptable.

---

## 5. SpectreConsoleHelpers

File: [Core/SpectreConsoleHelpers.cs](Core/SpectreConsoleHelpers.cs)

### Purpose
A helper class that wraps common console display patterns using Spectre.Console.

### Key members
- `WindowTitle(Justify alignment, string text = "Home screen")`
  - Displays a title pill centered or aligned in the console.
- `InfoPill(Justify alignment, string text = "Information")`
  - Renders an informational pill.
- `SuccessPill(Justify alignment, string text = "Success")`
  - Renders a success pill.
- `ErrorPill(Justify alignment, string text = "Error occurred")`
  - Renders an error pill.
- `ErrorPill(Justify alignment, string text, Exception exception)`
  - Renders an error pill alongside the exception message.
- `WarningPill(Justify alignment, string text = "Warning")`
  - Renders a warning pill.
- `PinkPill(Justify alignment, string text = "Pink")`
  - Renders a pink pill.
- `ExitPrompt(Justify alignment = Justify.Center)`
  - Shows a prompt asking the user to press any key to exit.
- `AlignTable(Table table, Justify alignment)`
  - Applies left, right, or center alignment to a table.
- `SetEncoding()`
  - Configures UTF-8 input and output encoding.
- `ExceptionSettingsStyle`
  - Provides a reusable style configuration for rendering exceptions.
- `WriteJson(string json)`
  - Prints formatted JSON with syntax highlighting.
- `PrintPink([CallerFilePath] string? filePath = null, [CallerMemberName] string? methodName = null)`
  - Writes a small formatted pink message showing the file and method names.

### Notes
This class is the main presentation layer for the library and ties together the custom pill rendering and common console output features.

---

## 6. Utilities

File: [Core/Utilities.cs](Core/Utilities.cs)

### Purpose
Provides utility methods for locating project information from a file path.

### Key member
- `GetProjectName(string filePath)`
  - Walks upward through parent directories to find a `.csproj` file.
  - Returns the project name if found, or a fallback name if not.

---

## Summary of the design

The library follows a simple structure:

1. UI rendering is handled by the pill and helper classes.
2. User input is collected through the prompt helpers.
3. Validation rules are isolated in the prompt validation helper.
4. Shared utility logic is kept in the utilities class.

This makes the code easy to reuse in console applications that need rich text rendering and interactive prompts.

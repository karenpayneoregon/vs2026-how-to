---
description: "Use when adding or refactoring C# code in SpectreConsoleLibrary, especially prompt, validation, and console-rendering helpers built with Spectre.Console."
name: "Spectre Console Library Conventions"
applyTo: "Core/Prompts.cs, Core/PromptValidations.cs, Core/SpectreConsoleHelpers.cs"
---
# Spectre Console Library Conventions

Treat the following as preferred guidelines. Exceptions are acceptable when they improve correctness, readability, or maintainability.

- Keep public API helpers in static classes under the SpectreConsoleLibrary.Core namespace when they are stateless.
- Use Spectre.Console primitives (AnsiConsole, TextPrompt, Table, Align, Style, JsonText) for terminal UX instead of raw Console output, except simple spacing or key input behavior.
- Keep prompt methods small and focused: one prompt concern per method, with sensible defaults for prompt text and style.
- Place reusable validation logic in PromptValidations instead of duplicating inline validation rules across methods.
- Return ValidationResult.Error with user-facing guidance that explains how to recover from invalid input.
- Preserve consistent markup styling in prompts and output (for example, explicit foreground colors and readable contrast).
- Keep XML documentation on public members, including concise summary, parameters, and return behavior.
- Prefer DateOnly for date-only input flows and validate against clear business rules (for example, no future dates, year thresholds).
- Avoid introducing framework dependencies outside the current library style unless there is a clear need and the change is discussed.
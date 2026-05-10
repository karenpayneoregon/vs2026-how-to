# About


In .NET 10, natural sorting is natively supported through the new CompareOptions.NumericOrdering flag. This allows string comparisons to treat numeric sequences as actual numbers rather than individual characters, so "file2.txt" correctly sorts before "file10.txt".

## Key Features

- **Atomic Number Treatment** : It treats multi-digit numbers as a single unit rather than a sequence of digits.
- **Performance** : Built into the core libraries, it is generally more efficient than older P/Invoke or regex-based workarounds.
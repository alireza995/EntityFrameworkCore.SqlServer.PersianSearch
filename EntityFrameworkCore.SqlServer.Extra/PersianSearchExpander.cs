using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EntityFrameworkCore.SqlServer.Extra;

public static class PersianSearchExpander
{
  private const int MaxExpansionPerChar = 7;

#if SPAN_SUPPORTED
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  // public static string ExpandPersianCharsForSearch(this ReadOnlySpan<char> input)
  public static string ExpandPersianCharsForSearch(this string input)
  {
    if (string.IsNullOrWhiteSpace(input)) return string.Empty;

    var maxOutputLength = input.Length * MaxExpansionPerChar;

    Span<char> outputBuffer = stackalloc char[maxOutputLength];
    var pos = 0;

    foreach (char c in input)
    {
      switch (c)
      {
        case 'ا' or 'آ' or 'أ' or 'إ' or 'ٱ':
          Append("[اآأإٱ]", outputBuffer, ref pos);
          break;
        case 'ی' or 'ي' or 'ئ':
          Append("[یيئ]", outputBuffer, ref pos);
          break;
        case 'ک' or 'ك':
          Append("[کك]", outputBuffer, ref pos);
          break;
        case 'و' or 'ؤ':
          Append("[وؤ]", outputBuffer, ref pos);
          break;
        case 'ه' or 'ة' or 'ۀ':
          Append("[هةۀ]", outputBuffer, ref pos);
          break;
        case 'ء':
          Append("[یيئء]", outputBuffer, ref pos);
          break;
        default:
          Debug.Assert(pos < outputBuffer.Length);
          outputBuffer[pos++] = c;
          break;
      }
    }

    return outputBuffer.Slice(0, pos).ToString();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void Append(string value, Span<char> buffer, ref int pos)
  {
    var span = value.AsSpan();
    Debug.Assert(pos + span.Length <= buffer.Length);
    span.CopyTo(buffer.Slice(pos));
    pos += span.Length;
  }

  // fallback for string callers
  // public static string ExpandPersianCharsForSearch(this string input) => input.AsSpan().ExpandPersianCharsForSearch();
#else
  // Fallback implementation that works on older TFMs without Span
  public static string ExpandPersianCharsForSearch(this string input)
  {
    if (string.IsNullOrEmpty(input)) return string.Empty;

    var sb = new System.Text.StringBuilder(input.Length * MaxExpansionPerChar);

    foreach (char c in input)
    {
      switch (c)
      {
        case 'ا':
        case 'آ':
        case 'أ':
        case 'إ':
        case 'ٱ':
          sb.Append("[اآأإٱ]");
          break;
        case 'ی':
        case 'ي':
        case 'ئ':
          sb.Append("[یيئ]");
          break;
        case 'ک':
        case 'ك':
          sb.Append("[کك]");
          break;
        case 'و':
        case 'ؤ':
          sb.Append("[وؤ]");
          break;
        case 'ه':
        case 'ة':
        case 'ۀ':
          sb.Append("[هةۀ]");
          break;
        case 'ء':
          sb.Append("[یيئء]");
          break;
        default:
          sb.Append(c);
          break;
      }
    }

    return sb.ToString();
  }
#endif
}
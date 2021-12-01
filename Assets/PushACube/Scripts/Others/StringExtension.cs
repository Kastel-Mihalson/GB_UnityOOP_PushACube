public static class StringExtension
{
    public static int CharCount(this string str, char ch)
    {
        int charCount = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == ch) charCount++;
        }

        return charCount;
    }
}

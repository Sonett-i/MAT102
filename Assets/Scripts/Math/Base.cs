using MathU;
public class Base
{
	public int numBase;

	public static string Convert(int A, int numBase)
	{
		if (numBase < 2 && numBase > 36 || A == 0)
		{
			return "0";
		}

		string output = string.Empty;
		char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

		while (A > 0)
		{
			int remainder = A % numBase;
			output = baseChars[remainder] + output;
			A /= numBase;
		}

		return output;
	}

    public static string Convert(decimal A, int numBase, int maxFractionalDigits = 16)
    {
        if (numBase < 2 || numBase > 36)
        {
            //throw new ArgumentException("Base must be between 2 and 36.");
        }

        if (A == 0)
        {
            return "0"; // Special case for A = 0
        }

        char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string integerPart = string.Empty;
        string fractionalPart = string.Empty;

        int integerPartValue = int.Parse(Convert((int)A, numBase));

        decimal fractionalPartValue = A - integerPartValue;


        int fractionalDigits = 0;

        while (fractionalPartValue > 0 && fractionalDigits < maxFractionalDigits)
        {
            fractionalPartValue *= numBase;
            int digit = (int)Math.Floor((float)fractionalPartValue);
            fractionalPartValue -= digit;
            fractionalPart += baseChars[digit];
            fractionalDigits++;
        }

        if (integerPart == "")
        {
            integerPart = "0";
        }

        return integerPart + (fractionalPart != "" ? "." + fractionalPart : "");
    }

    public static string Convert(decimal A, int originalBase, int numBase, int maxFractionalDigits = 16)
    {
        if (numBase < 2 || numBase > 36)
        {
            //throw new ArgumentException("Base must be between 2 and 36.");
        }

        if (A == 0)
        {
            return "0"; // Special case for A = 0
        }

        char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string integerPart = string.Empty;
        string fractionalPart = string.Empty;

        int integerPartValue = int.Parse(Convert((int)A, numBase));

        decimal fractionalPartValue = A - integerPartValue;


        int fractionalDigits = 0;

        while (fractionalPartValue > 0 && fractionalDigits < maxFractionalDigits)
        {
            fractionalPartValue *= numBase;
            int digit = (int)Math.Floor((float)fractionalPartValue);
            fractionalPartValue -= digit;
            fractionalPart += baseChars[digit];
            fractionalDigits++;
        }

        if (integerPart == "")
        {
            integerPart = "0";
        }

        return integerPart + (fractionalPart != "" ? "." + fractionalPart : "");
    }
}

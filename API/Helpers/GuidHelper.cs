namespace API.Helpers;

public static class GuidHelper
{
    public static Guid IntToGuid(int value)
    {
        byte[] byteArray = new byte[16];

        BitConverter.GetBytes(value).CopyTo(byteArray, 0);

        var rng = new Random();
        rng.NextBytes(byteArray.AsSpan(4, 12));

        return new Guid(byteArray);
    }

    public static int GuidToInt(Guid guid)
    {
        byte[] byteArray = guid.ToByteArray();

        return BitConverter.ToInt32(byteArray, 0);
    }
}
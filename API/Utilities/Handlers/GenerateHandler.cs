namespace API.Utilities.Handlers;

public class GenerateHandler
{
    public static string Nik(string? lastNik = null)
    {
        if (lastNik is null)
        {
            return "111111";
        }

        var generateNik = Convert.ToInt32(lastNik) + 1;
        return generateNik.ToString();
    }

    public static string OverNumber(string? OverNumber = null)
    {
        if (OverNumber is null)
        {
            return "70100000";
        }

        var generateNumber = Convert.ToInt32(OverNumber) + 1;
        return generateNumber.ToString();
    }
}

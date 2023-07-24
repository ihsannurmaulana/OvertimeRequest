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
}

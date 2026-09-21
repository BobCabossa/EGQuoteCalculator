namespace GordonFancyTool;

public static class DoubleCal
{
    public static double Round(double value, int digits = 2) => double.Round(value, digits, MidpointRounding.ToPositiveInfinity);
    public static double TryParse(string value)
    {
        _ = double.TryParse(value, out double parsed);
        return parsed;
    }
}

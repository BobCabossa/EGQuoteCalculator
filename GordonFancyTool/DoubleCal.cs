namespace GordonFancyTool;

public static class DoubleCal
{
    public static double Round(double value, int digits = 2) => double.Round(value, digits, MidpointRounding.ToPositiveInfinity);
}

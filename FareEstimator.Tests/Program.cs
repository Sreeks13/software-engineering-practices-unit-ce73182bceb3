using FareEstimator;

internal class Program
{
    private static int Main()
    {
        int failures = 0;

        failures += Check("Standard fare", 8.5m, 0, "20.00");
        failures += Check("Loyalty discount", 8.5m, 150, "18.00");
        failures += Check("Threshold boundary", 8.5m, 100, "18.00");
        failures += Check("101 loyalty points", 8.5m, 101, "18.00");

        failures += CheckInvalidDistance();
        failures += CheckInvalidPoints();

        Console.WriteLine(
            failures == 0
                ? "All tests passed."
                : $"{failures} test(s) failed.");

        return failures == 0 ? 0 : 1;
    }

    private static int Check(
        string name,
        decimal distance,
        int points,
        string expected)
    {
        try
        {
            string actual = FareCalculator
                .CalculateFare(distance, points)
                .ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

            bool passed = actual == expected;

            Console.WriteLine(
                $"{(passed ? "PASS" : "FAIL")}: {name}" +
                $" (expected {expected}, got {actual})");

            return passed ? 0 : 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL: {name} ({ex.Message})");
            return 1;
        }
    }

    private static int CheckInvalidDistance()
    {
        try
        {
            FareCalculator.CalculateFare(0, 0);
            Console.WriteLine("FAIL: Zero distance should be rejected.");
            return 1;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("PASS: Zero distance rejected.");
            return 0;
        }
    }

    private static int CheckInvalidPoints()
    {
        try
        {
            FareCalculator.CalculateFare(8.5m, -1);
            Console.WriteLine("FAIL: Negative points should be rejected.");
            return 1;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("PASS: Negative points rejected.");
            return 0;
        }
    }
}
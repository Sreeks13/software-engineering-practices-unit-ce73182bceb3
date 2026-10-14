using System.Globalization;

namespace FareEstimator;

internal class Program
{
    private static void Main()
    {
        try
        {
            Console.Write("Enter ride distance in km: ");
            string? distanceInput = Console.ReadLine();

            Console.Write("Enter loyalty points: ");
            string? pointsInput = Console.ReadLine();

            if (!decimal.TryParse(
                    distanceInput,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal distanceKm))
            {
                throw new FormatException("Distance must be a valid number.");
            }

            if (!int.TryParse(pointsInput, out int loyaltyPoints))
            {
                throw new FormatException("Loyalty points must be a whole number.");
            }

            decimal fare = FareCalculator.CalculateFare(
                distanceKm, loyaltyPoints);

            Console.WriteLine($"Estimated fare: {fare:F2}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Invalid input: {ex.Message}");
            Environment.ExitCode = 1;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Invalid input: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }
}
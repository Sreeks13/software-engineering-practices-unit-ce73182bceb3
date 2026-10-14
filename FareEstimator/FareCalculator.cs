namespace FareEstimator;

public static class FareCalculator
{
    private const decimal BaseFare = 3.00m;
    private const decimal RatePerKm = 2.00m;
    private const int LoyaltyDiscountThreshold = 100;
    private const decimal LoyaltyDiscountRate = 0.10m;

    public static decimal CalculateFare(decimal distanceKm, int loyaltyPoints)
    {
        if (distanceKm <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(distanceKm), "Distance must be positive.");

        if (loyaltyPoints < 0)
            throw new ArgumentOutOfRangeException(
                nameof(loyaltyPoints), "Loyalty points cannot be negative.");

        decimal fare = BaseFare + (distanceKm * RatePerKm);

        // Set a conditional breakpoint on this line:
        // loyaltyPoints == 101
        if (loyaltyPoints >= LoyaltyDiscountThreshold)
        {
            fare *= (1 - LoyaltyDiscountRate);
        }

        return Math.Round(fare, 2, MidpointRounding.AwayFromZero);
    }
}
namespace Spg.BowlingKata.BowlingGame
{
    public class BowlingCalculator
    {
        private int _sum = 0;
        Stack<int> _frame = new Stack<int>();

        public int Roll(int thrownPins)
        {
            if (thrownPins < 0 || thrownPins > 10)
            {
                throw new BowlingServiceException("Es sind aber nur 0-10 Kegel (Schlingel)!");
            }

            // Anzahl der umgeworfenen Pins in Summe speichern
            GameState.SumThronPins += thrownPins;

            // Wurfnummer inkrementieren
            GameState.RollNumber++;

            // Stack verwenden _frame
            // _sum reinschreiben

        }
    }
}
namespace Spg.BowlingKata.BowlingGame
{
    public class BowlingCalculator
    {
        private int _sum = 0;
        private Stack<int> _frame = new Stack<int>();

        public int Roll(int thrownPins)
        {
            if (thrownPins < 0 || thrownPins > 10)
            {
                throw new BowlingServiceException("Es sind aber nur 0-10 Kegel (Schlingel)!");
            }

            _sum = _sum + thrownPins;

            // Anzahl der umgeworfenen Pins in Summe speichern

            // Wurfnummer inkrementieren

            // Stack verwenden _frame
            // _sum reinschreiben

            return -1;

        }
    }
}
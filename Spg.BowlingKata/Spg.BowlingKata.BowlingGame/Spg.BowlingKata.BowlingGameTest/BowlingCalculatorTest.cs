using Spg.BowlingKata.BowlingGame;

namespace Spg.BowlingKata.BowlingGameTest
{
    public class BowlingCalculatorTest
    {
        /// <summary>
        /// Verifiziert ob ThrownPins im State gespeichert wird.
        /// </summary>
        [Fact()]
        public void Verify_ThrownPins_StoredInState()
        {
            // Arrange
            BowlingCalculator unitToTest = new BowlingCalculator();

            // Act
            unitToTest.Roll(2);

            // Assert
            Assert.Equal(2, unitToTest.GameState.SumThronPins);
        }

        [Fact()]
        public void Verify_Sum_TwoRolls()
        {
            // Arrange
            BowlingCalculator unitToTest = new BowlingCalculator();

            // Act
            unitToTest.Roll(2);
            unitToTest.Roll(2);

            // Assert
            Assert.Equal(4, unitToTest.GameState.SumThronPins);
        }

        [Fact()]
        public void Verify_RollNumer_Increments()
        {
            // Arrange
            BowlingCalculator unitToTest = new BowlingCalculator();

            // Act
            unitToTest.Roll(3);
            unitToTest.Roll(5);

            // Assert
            Assert.Equal(2, unitToTest.GameState.RollNumber);
        }

        [Fact()]
        public void ToManyPins_BowlingServiceException_Expected()
        {
            // Arrange
            BowlingCalculator unitToTest = new BowlingCalculator();

            // Act + Assert
            Assert.Throws<BowlingServiceException>(() => unitToTest.Roll(12));
        }

        [Fact()]
        public void ToMuchNegativePins_BowlingServiceException_Expected()
        {
            // Arrange
            BowlingCalculator unitToTest = new BowlingCalculator();

            // Act + Assert
            Assert.Throws<BowlingServiceException>(() => unitToTest.Roll(-3));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UwUSnake;

namespace UwUSnakeTests
{
    [TestClass]
    public class UwUTests
    {
        [TestMethod]
        public void StartGame_ShouldInitializeCorrectly()
        {
            UwU.StartGame();

            Assert.AreEqual(20, UwU.snakeX);
            Assert.AreEqual(10, UwU.snakeY);
            Assert.AreEqual(1, UwU.snakeBody.Count);

            Assert.IsTrue(UwU.foodX >= 0 && UwU.foodX <= UwU.width);
            Assert.IsTrue(UwU.foodY >= 0 && UwU.foodY <= UwU.height);
        }

        [TestMethod]
        public void Logic_ShouldMoveSnakeCorrectly()
        {
            UwU.velocityX = 1;
            UwU.velocityY = 0;

            UwU.snakeX = 5;
            UwU.snakeY = 5;

            UwU.Logic();

            Assert.AreEqual(6, UwU.snakeX);
            Assert.AreEqual(5, UwU.snakeY);

            UwU.velocityX = 0;
            UwU.velocityY = 1;

            UwU.snakeX = 5;
            UwU.snakeY = 5;

            UwU.Logic();

            Assert.AreEqual(5, UwU.snakeX);
            Assert.AreEqual(6, UwU.snakeY);
        }

        [TestMethod]
        public void Logic_ShouldDetectWallCollision()
        {
            UwU.snakeX = 0;
            UwU.velocityX = -1;

            UwU.Logic();

            Assert.IsTrue(UwU.gameOver);
        }

        [TestMethod]
        public void Logic_ShouldIncreaseScoreAndGrowSnake_OnEatingFood()
        {
            int initialLength = UwU.snakeBody.Count;

            UwU.snakeX = 5;
            UwU.snakeY = 5;

            UwU.velocityX = 1;

            UwU.foodX = 6;
            UwU.foodY = 5;

            UwU.Logic();
            UwU.Logic();

            Assert.AreEqual(1, UwU.score);
            Assert.AreEqual(initialLength + 1, UwU.snakeBody.Count);
        }
    }
}
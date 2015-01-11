using System.Linq;
using CoffeVendingMaсhine.Calculator;
using CoffeVendingMaсhine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    ///     Summary description for ChangeCalculatorTest
    /// </summary>
    [TestClass]
    public class ChangeCalculatorTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void TestFoundedChange()
        {
            var wallet = new Wallet();

            wallet.AddCoins(1, 3);
            wallet.AddCoins(2, 1);
            wallet.AddCoins(5, 1);
            wallet.AddCoins(10, 1);

            int summ = 14;

            var calculator = new ChangeCalculator();
            Wallet change = calculator.FindChange(wallet, summ);

            Assert.IsTrue(change.Coins.FirstOrDefault(c => c.Value == 10).Count == 1);
            Assert.IsTrue(change.Coins.FirstOrDefault(c => c.Value == 2).Count == 1);
            Assert.IsTrue(change.Coins.FirstOrDefault(c => c.Value == 1).Count == 2);
        }

        [TestMethod]
        public void TestReturnNull()
        {
            var wallet = new Wallet();

            wallet.AddCoins(1, 3);
            wallet.AddCoins(2, 1);
            wallet.AddCoins(5, 1);
            wallet.AddCoins(10, 1);

            int summ = -14;

            var calculator = new ChangeCalculator();
            Wallet change = calculator.FindChange(wallet, summ);

            Assert.IsTrue(change == null);

            summ = 14;
            change = calculator.FindChange(null, summ);
            Assert.IsTrue(change == null);
        }

        [TestMethod]
        public void TestNotFoundChange()
        {
            var wallet = new Wallet();

            wallet.AddCoins(1, 3);
            wallet.AddCoins(2, 1);
            wallet.AddCoins(5, 1);

            int summ = 14;

            var calculator = new ChangeCalculator();
            Wallet change = calculator.FindChange(wallet, summ);

            Assert.IsTrue(change.Total == 0);
        }
    }
}

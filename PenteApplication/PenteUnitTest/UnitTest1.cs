using System;
using PenteApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PenteUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //Jordon and Collin
        [TestMethod]
        public void Player1NameChangeIfNull()
        {
            MainWindow mw = new MainWindow();
            mw.Player1Naming(null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player1NameChangeIfEmpty()
        {
            MainWindow mw = new MainWindow();
            mw.Player1Naming("");
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player1NameChangeIfWhiteSpace()
        {
            MainWindow mw = new MainWindow();
            mw.Player1Naming("     ");
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player1NameChangeIfLetter()
        {
            MainWindow mw = new MainWindow();
            mw.Player1Naming("S");
            Assert.AreEqual("S", mw.Player1Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player1NameChangeIfName()
        {
            MainWindow mw = new MainWindow();
            mw.Player1Naming("Derrick");
            Assert.AreEqual("Derrick", mw.Player1Name);
        }

        //Jordon and Collin
        [TestMethod]
        public void Player2NameChangeIfNull()
        {
            MainWindow mw = new MainWindow();
            mw.Player2Naming(null);
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player2NameChangeIfEmpty()
        {
            MainWindow mw = new MainWindow();
            mw.Player2Naming("");
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player2NameChangeIfWhiteSpace()
        {
            MainWindow mw = new MainWindow();
            mw.Player2Naming("     ");
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player2NameChangeIfLetter()
        {
            MainWindow mw = new MainWindow();
            mw.Player2Naming("L");
            Assert.AreEqual("L", mw.Player2Name);
        }
        //Jordon and Collin
        [TestMethod]
        public void Player2NameChangeIfName()
        {
            MainWindow mw = new MainWindow();
            mw.Player2Naming("Derrick");
            Assert.AreEqual("Derrick", mw.Player2Name);
        }

        //Austin and Jarrett
        [TestMethod]
        public void Draw8x8Board()
        {
            MainWindow mw = new MainWindow();
            int size = mw.FillGameGrid(8);
            Assert.AreEqual(8, size);
        }
        //Austin and Jarrett
        [TestMethod]
        public void Draw38x38Board()
        {
            MainWindow mw = new MainWindow();
            int size = mw.FillGameGrid(38);
            Assert.AreEqual(38, size);
        }
        //Austin and Jarrett
        [TestMethod]
        public void Draw16x16Board()
        {
            MainWindow mw = new MainWindow();
            int size = mw.FillGameGrid(16);
            Assert.AreEqual(16, size);
        }
        //Austin and Jarrett
        [TestMethod]
        public void Draw24x24Board()
        {
            MainWindow mw = new MainWindow();
            int size = mw.FillGameGrid(24);
            Assert.AreEqual(24, size);
        }
    }
}

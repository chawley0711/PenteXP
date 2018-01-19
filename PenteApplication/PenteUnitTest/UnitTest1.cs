using System;
using PenteApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PenteUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PlayerNameChangeIfNull()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = null;
            mw.Player1SubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void PlayerNameChangeIfEmpty()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "";
            mw.Player1SubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void PlayerNameChangeIfWhiteSpace()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "        ";
            mw.Player1SubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void PlayerNameChangeIfLetter()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "        ";
            mw.Player1SubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void PlayerNameChangeIfName()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = false;
            mw.Player1Name = "Derrick";
            mw.Player1SubmitButton_Click(new object(), null);
            Assert.AreEqual("Derrick", mw.Player1Name);
        }
    }
}

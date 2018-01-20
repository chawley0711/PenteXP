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
            mw.pvp = true;
            mw.Player1Name = null;
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void Player1NameChangeIfEmpty()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void Player1NameChangeIfWhiteSpace()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "        ";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void Player1NameChangeIfLetter()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player1Name = "        ";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 1", mw.Player1Name);
        }
        [TestMethod]
        public void Player1NameChangeIfName()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = false;
            mw.Player1Name = "Derrick";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Derrick", mw.Player1Name);
        }

        [TestMethod]
        public void Player2NameChangeIfNull()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player2Name = null;
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        [TestMethod]
        public void Player2NameChangeIfEmpty()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player2Name = "";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        [TestMethod]
        public void Player2NameChangeIfWhiteSpace()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player2Name = "        ";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        [TestMethod]
        public void Player2NameChangeIfLetter()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = true;
            mw.Player2Name = "        ";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Player 2", mw.Player2Name);
        }
        [TestMethod]
        public void Player2NameChangeIfName()
        {
            MainWindow mw = new MainWindow();
            mw.pvp = false;
            mw.Player2Name = "Derrick";
            mw.PlayerSubmitButton_Click(new object(), null);
            Assert.AreEqual("Derrick", mw.Player2Name);
        }
    }
}

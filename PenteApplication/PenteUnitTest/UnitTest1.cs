using System;
using PenteApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace PenteUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        RoutedEventArgs e = new RoutedEventArgs();
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

        //Austin and Jarrett
        [TestMethod]
        public void PlaceStoneP1()
        {
            MainWindow mw = new MainWindow();
            mw.P1Turn = true;
            mw.gameIntersections = new List<Intersection>();
            Button intersection = new Button();
            Intersection inter = new Intersection();
            intersection.Opacity = 1;
            intersection.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            intersection.VerticalContentAlignment = VerticalAlignment.Stretch;
            intersection.DataContext = inter;
            intersection.Click += mw.PlaceStone_Click;
            mw.gameIntersections.Add(inter);
            mw.PlaceStone_Click(intersection, e);
            Assert.AreEqual(Fill.Black, mw.gameIntersections[0].IntersectionFill);
        }
        //Austin and Jarrett
        [TestMethod]
        public void PlaceStoneP2()
        {
            MainWindow mw = new MainWindow();
            mw.P1Turn = false;
            mw.FillGameGrid(8);
            mw.PlaceStone_Click(mw.gameIntersections[0], e);
            Assert.AreEqual(Fill.White, mw.gameIntersections[0].IntersectionFill);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalBlackCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[1].IntersectionFill = Fill.White;
            mw.gameIntersections[2].IntersectionFill = Fill.White;
            mw.gameIntersections[3].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalBlackNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[1].IntersectionFill = Fill.White;
            mw.gameIntersections[2].IntersectionFill = Fill.Black;
            mw.gameIntersections[3].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalBlackCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[18].IntersectionFill = Fill.White;
            mw.gameIntersections[27].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalBlackNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[18].IntersectionFill = Fill.Black;
            mw.gameIntersections[27].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalBlackCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            mw.gameIntersections[20].IntersectionFill = Fill.White;
            mw.gameIntersections[30].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalBlackNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            mw.gameIntersections[20].IntersectionFill = Fill.Black;
            mw.gameIntersections[30].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalBlackNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[1].IntersectionFill = Fill.Black;
            mw.gameIntersections[2].IntersectionFill = Fill.Black;
            mw.gameIntersections[3].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalBlackNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[9].IntersectionFill = Fill.Black;
            mw.gameIntersections[18].IntersectionFill = Fill.Black;
            mw.gameIntersections[27].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalBlackNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.Black;
            mw.gameIntersections[10].IntersectionFill = Fill.Black;
            mw.gameIntersections[20].IntersectionFill = Fill.Black;
            mw.gameIntersections[30].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalWhiteCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[1].IntersectionFill = Fill.Black;
            mw.gameIntersections[2].IntersectionFill = Fill.Black;
            mw.gameIntersections[3].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalWhiteNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[1].IntersectionFill = Fill.White;
            mw.gameIntersections[2].IntersectionFill = Fill.Black;
            mw.gameIntersections[3].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalWhiteCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[9].IntersectionFill = Fill.Black;
            mw.gameIntersections[18].IntersectionFill = Fill.Black;
            mw.gameIntersections[27].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalWhiteNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[18].IntersectionFill = Fill.Black;
            mw.gameIntersections[27].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalWhiteCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[10].IntersectionFill = Fill.Black;
            mw.gameIntersections[20].IntersectionFill = Fill.Black;
            mw.gameIntersections[30].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(true, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalWhiteNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            mw.gameIntersections[20].IntersectionFill = Fill.Black;
            mw.gameIntersections[30].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalWhiteNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[1].IntersectionFill = Fill.White;
            mw.gameIntersections[2].IntersectionFill = Fill.White;
            mw.gameIntersections[3].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(3);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckVerticalWhiteNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[18].IntersectionFill = Fill.White;
            mw.gameIntersections[27].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(27);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckDiagonalWhiteNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[0].IntersectionFill = Fill.White;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            mw.gameIntersections[20].IntersectionFill = Fill.White;
            mw.gameIntersections[30].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(30);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalEdgeWhiteNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[7].IntersectionFill = Fill.White;
            mw.gameIntersections[8].IntersectionFill = Fill.White;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(10);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalEdgeBlackNonCaptureWhite()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[7].IntersectionFill = Fill.Black;
            mw.gameIntersections[8].IntersectionFill = Fill.White;
            mw.gameIntersections[9].IntersectionFill = Fill.White;
            mw.gameIntersections[10].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(10);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalEdgeBlackNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[7].IntersectionFill = Fill.Black;
            mw.gameIntersections[8].IntersectionFill = Fill.Black;
            mw.gameIntersections[9].IntersectionFill = Fill.Black;
            mw.gameIntersections[10].IntersectionFill = Fill.Black;
            bool test = mw.CheckForCapture(10);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        [TestMethod]
        public void CheckHorizontalEdgeWhiteNonCaptureBlack()
        {
            MainWindow mw = new MainWindow();
            mw.FillGameGrid(8);
            mw.gameIntersections[7].IntersectionFill = Fill.White;
            mw.gameIntersections[8].IntersectionFill = Fill.Black;
            mw.gameIntersections[9].IntersectionFill = Fill.Black;
            mw.gameIntersections[10].IntersectionFill = Fill.White;
            bool test = mw.CheckForCapture(10);
            Assert.AreEqual(false, test);
        }
        //Austin and Jarrett
        //[TestMethod]
        //public void CheckVerticalTria()
        //{
        //    MainWindow mw = new MainWindow();
        //    mw.FillGameGrid(8);
        //    mw.gameIntersections[0].IntersectionFill = Fill.Black;
        //    mw.gameIntersections[9].IntersectionFill = Fill.Black;
        //    mw.gameIntersections[18].IntersectionFill = Fill.Black;
        //    bool test = mw.CheckForTria(Fill.Black);
        //    Assert.AreEqual(true, test);
        //}
    }
}

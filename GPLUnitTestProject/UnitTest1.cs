using System;
using System.Windows.Forms;
using GPL_Appn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GPLUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ChkTriangleValidity()
        {
            Triangle T = new Triangle();
            T.GetValue(1, 2, 0, 3);
            bool exceptedResult = false;
            bool actualResult = T.chkTriangleValidity();
            Assert.AreEqual(exceptedResult, actualResult);
        }

        [TestMethod]
        public void ChkTriangleValidity1()
        {
            Triangle T = new Triangle();
            T.GetValue(50, 50, 50, 50);
            bool exceptedResult = true;
            bool actualResult = T.chkTriangleValidity();
            Assert.AreEqual(exceptedResult, actualResult);
        }

        [TestMethod]
        public void Validation()
        {
            TextBox TB = new TextBox();
            TB.Text = "Circle 50";
            Validation V = new Validation(TB);
            bool exceptedResult = true;
            bool actualResult = V.IsCmdValid;
            Assert.AreEqual(exceptedResult, actualResult);
        }

        [TestMethod]
        public void Validation1()
        {
            TextBox TB = new TextBox();
            TB.Text = "Circle 50,50";
            Validation V = new Validation(TB);
            bool exceptedResult = false;
            bool actualResult = V.IsCmdValid;
            Assert.AreEqual(exceptedResult, actualResult);
        }

        [TestMethod]
        public void CheckCmdLineValidation()
        {
            TextBox TB = new TextBox();
            TB.Text = "moveto 60,40";
            Validation V = new Validation(TB);
            bool expectedResult = true;
            bool actualResult;
            V.CheckCmdLineValidation(TB.Text);
            actualResult = V.IsSyntaxValid;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

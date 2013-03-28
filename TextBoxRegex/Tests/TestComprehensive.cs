#region Copyright (c) 2004 Marek Grzenkowicz
/*
 * Copyright (c) 2004 Marek Grzenkowicz
 * 
 * This software is provided 'as-is', without any warranty.
 * 
 * Permission is granted to anyone to use this software for any purpose.
 * 
 * This notice may not be removed from any source distibution; if you are
 * using this software in a product, this notice should be included in
 * materials distributed with your product.
 */
#endregion

#if DEBUG
using System.Drawing;
using NUnit.Framework;

namespace Chopeen
{
    /// <summary>
    /// <exclude/>
    /// </summary>
    [TestFixture]
    public class TestComprehensive
    {
        #region Tests' methods
        private class TestsMethods
        {
            internal static void TestPatterns(TextBoxRegex.Patterns pattern, string str, bool[] results)
            {
                tbr.Pattern = pattern;
                tbr.PatternString = str;

                tbr.Text = " ";
                tbr.SetBackColor();
                Assert.AreEqual(results[0] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "a";
                tbr.SetBackColor();
                Assert.AreEqual(results[1] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "A";
                tbr.SetBackColor();
                Assert.AreEqual(results[2] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "1";
                tbr.SetBackColor();
                Assert.AreEqual(results[3] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "=";
                tbr.SetBackColor();
                Assert.AreEqual(results[4] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "$";
                tbr.SetBackColor();
                Assert.AreEqual(results[5] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "foo@bar.net";
                tbr.SetBackColor();
                Assert.AreEqual(results[6] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "foo@bar.pl";
                tbr.SetBackColor();
                Assert.AreEqual(results[7] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "index.htm";
                tbr.SetBackColor();
                Assert.AreEqual(results[8] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "index.html";
                tbr.SetBackColor();
                Assert.AreEqual(results[9] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "fao@bar.net";
                tbr.SetBackColor();
                Assert.AreEqual(results[10] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "AADDee55ff";
                tbr.SetBackColor();
                Assert.AreEqual(results[11] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = "AADD^ee55ff";
                tbr.SetBackColor();
                Assert.AreEqual(results[12] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = ";'.,{}[]%$";
                tbr.SetBackColor();
                Assert.AreEqual(results[13] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);

                tbr.Text = ";'.1,{}[]%$";
                tbr.SetBackColor();
                Assert.AreEqual(results[14] ? tbr.ValidTextBackColor : tbr.InvalidTextBackColor,
                    tbr.BackColor, "\nTest string: " + tbr.Text);
            }
        }
        #endregion

        private static TextBoxRegex tbr;

        [SetUp]
        public void SetUp()
        {
            tbr = new TextBoxRegex();

            tbr.UseColors = true;
            tbr.ValidTextBackColor = Color.Green;
            tbr.InvalidTextBackColor = Color.Red;
        }

        [Test]
        public void TestNone()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = false;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.None, "", results);
        }

        [Test]
        public void TestSmallLetters()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = true;
            results[2] = false;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.SmallLetters, "", results);
        }

        [Test]
        public void TestCapitalLetters()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = true;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.CapitalLetters, "", results);
        }

        [Test]
        public void TestDigits()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = false;
            results[3] = true;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.Digits, "", results);
        }

        [Test]
        public void TestNonAlphaNumericCharacters()
        {
            bool[] results = new bool[15];

            results[0] = true;
            results[1] = false;
            results[2] = false;
            results[3] = false;
            results[4] = true;
            results[5] = true;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = true;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.NonAlphaNumericCharacters, "", results);
        }

        public void TestCharacterCollection()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = true;
            results[3] = false;
            results[4] = false;
            results[5] = true;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = true;
            results[12] = false;
            results[13] = true;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.CharacterCollection, ";'.,{}[]%$ADe5f", results);
        }

        [Test]
        public void TestWildcardPattern1()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = false;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = true;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.WildcardPattern, "?o?@*.n*", results);
        }

        [Test]
        public void TestWildcardPattern2()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = false;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = true;
            results[10] = false;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.WildcardPattern, "i?de?.htm*", results);
        }

        [Test]
        public void TestRegexPattern1()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = false;
            results[2] = false;
            results[3] = false;
            results[4] = true;
            results[5] = false;
            results[6] = true;
            results[7] = false;
            results[8] = true;
            results[9] = true;
            results[10] = true;
            results[11] = false;
            results[12] = false;
            results[13] = false;
            results[14] = false;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.RegexPattern, "^(.+@bar\\.net)|=$|^([indexhtm\\.]+.?)$", results);
        }

        [Test]
        public void TestRegexPattern2()
        {
            bool[] results = new bool[15];

            results[0] = false;
            results[1] = true;
            results[2] = true;
            results[3] = false;
            results[4] = false;
            results[5] = false;
            results[6] = false;
            results[7] = false;
            results[8] = false;
            results[9] = false;
            results[10] = false;
            results[11] = true;
            results[12] = true;
            results[13] = true;
            results[14] = true;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.RegexPattern, "^[aA;].?", results);
        }

        [Test]
        public void TestAll()
        {
            bool[] results = new bool[15];

            results[0] = true;
            results[1] = true;
            results[2] = true;
            results[3] = true;
            results[4] = true;
            results[5] = true;
            results[6] = true;
            results[7] = true;
            results[8] = true;
            results[9] = true;
            results[10] = true;
            results[11] = true;
            results[12] = true;
            results[13] = true;
            results[14] = true;

            TestsMethods.TestPatterns(TextBoxRegex.Patterns.All, "", results);        	
        }
    }
}
#endif
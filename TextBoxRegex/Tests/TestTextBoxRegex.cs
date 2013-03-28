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
    public class TestTextBoxRegex
    {
        #region Tests' methods
        internal class TestsMethods
        {
            internal static void TestGetRegexStringAssert(TextBoxRegex.Patterns pattern, string str, string expected)
            {
                TestTextBoxRegex.tbr.Pattern = pattern;
                TestTextBoxRegex.tbr.PatternString = str;
                Assert.AreEqual(expected, TestTextBoxRegex.tbr.GetRegexString());

                TestTextBoxRegex.tbr.Pattern = TextBoxRegex.Patterns.None;
                TestTextBoxRegex.tbr.PatternString = "";                
            }

            internal static void TestIsTextValidAssert(TextBoxRegex.Patterns pattern, string text, object expected)
            {
                tbr.Pattern = pattern;
                tbr.Text = text;
                Assert.AreEqual(expected, tbr.IsTextValid());
            }
        }
        #endregion

        private static TextBoxRegex tbr;

        [SetUp]
        public void CreateTextBoxRegex()
        {
            tbr = new TextBoxRegex();
        }

        [Test]
        public void TestConstructor1()
        {
            Assert.AreEqual("", tbr.Text);
            Assert.AreEqual(tbr.ValidTextBackColor, Color.LightGreen);
            Assert.AreEqual(tbr.InvalidTextBackColor, Color.LightPink);
            Assert.AreEqual(tbr.UseColors, true);
            Assert.AreEqual(tbr.Pattern, TextBoxRegex.Patterns.None);
            Assert.AreEqual(tbr.PatternString, "");
            Assert.AreEqual(tbr.UseInvalidTextException, true);
        }

        [Test]
        public void TestConstructor2()
        {
        	TextBoxRegex t = new TextBoxRegex(Color.LightCyan, Color.LightSeaGreen, false,
                TextBoxRegex.Patterns.CapitalLetters, "",
                false);

            Assert.AreEqual("", tbr.Text);
            Assert.AreEqual(t.ValidTextBackColor, Color.LightCyan);
            Assert.AreEqual(t.InvalidTextBackColor, Color.LightSeaGreen);
            Assert.AreEqual(t.UseColors, false);
            Assert.AreEqual(t.Pattern, TextBoxRegex.Patterns.CapitalLetters);
            Assert.AreEqual(t.PatternString, "");
            Assert.AreEqual(t.UseInvalidTextException, false);
        }

        [Test]
        public void TestText()
        {
            tbr.Text = "foobar";
            Assert.AreEqual("foobar", tbr.Text);
        }

        [Test]
        public void TestValidTextBackColor()
        {
            tbr.ValidTextBackColor = Color.Fuchsia;
            Assert.AreEqual(Color.Fuchsia, tbr.ValidTextBackColor);
        }

        [Test]
        public void TestInvalidTextBackColor()
        {
            tbr.InvalidTextBackColor = Color.Gainsboro;
            Assert.AreEqual(Color.Gainsboro, tbr.InvalidTextBackColor);
        }

        [Test]
        public void TestUseColors()
        {
            tbr.UseColors = false;
            Assert.AreEqual(false, tbr.UseColors);
        }

        [Test]
        public void TestPattern()
        {
            tbr.Pattern = TextBoxRegex.Patterns.Digits;
            Assert.AreEqual(TextBoxRegex.Patterns.Digits, tbr.Pattern);
        }

        [Test]
        public void TestPatternString()
        {
        	tbr.PatternString = "[abc123]";
            Assert.AreEqual("[abc123]", tbr.PatternString);

            tbr.PatternString = "100.*.???.aa";
            Assert.AreEqual("100.*.???.aa", tbr.PatternString);
        }

        [Test]
        public void TestUseInvalidTextException()
        {
        	tbr.UseInvalidTextException = true;
            Assert.AreEqual(true, tbr.UseInvalidTextException);

            tbr.UseInvalidTextException = false;
            Assert.AreEqual(false, tbr.UseInvalidTextException);
        }

        [Test]
        public void TestTextValidated1()
        {
        	tbr.UseInvalidTextException = false;
            tbr.Pattern = TextBoxRegex.Patterns.CapitalLetters;
            tbr.Text = "A";
            Assert.AreEqual("A", tbr.TextValidated);

            tbr.UseInvalidTextException = false;
            tbr.Pattern = TextBoxRegex.Patterns.CapitalLetters;
            tbr.Text = "a";
            Assert.AreEqual("", tbr.TextValidated);

            tbr.UseInvalidTextException = true;
            tbr.Pattern = TextBoxRegex.Patterns.CapitalLetters;
            tbr.Text = "A";
            Assert.AreEqual("A", tbr.TextValidated);
        }

        [Test]
        [ExpectedException(typeof(InvalidTextException))]
        public void TestTextValidated2()
        {
            tbr.UseInvalidTextException = true;
            tbr.Pattern = TextBoxRegex.Patterns.CapitalLetters;
            tbr.Text = "ABCDe";
            string foo = tbr.TextValidated;
        }

        [Test]
        public void TestGetRegex()
        {
            // TODO: I don't know how to test GetRegex method
        }

        [Test]
        public void TestIsTextValidNone()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, "", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, " ", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, "a", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, "ab", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, "1", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.None, "A1c", false);
        }

        [Test]
        public void TestIsTextValidSmallLetters()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "a", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "ab", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "dasdasab", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "1", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, "A1c", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, ",a.sd", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.SmallLetters, " ", false);
        }

        [Test]
        public void TestIsTextValidCapitalLetters()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "A", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "AB", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "AHJAKKHKJAHK", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "1", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, "A1c", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, ",A.sd", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.CapitalLetters, " ", false);
        }

        [Test]
        public void TestIsTextValidDigits()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "5", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "01", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "456465465", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "(", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, "A1c", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, ",a.sd", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.Digits, " ", false);
        }

        [Test]
        public void TestIsTextValidNonAlphaNumericCharacters()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, ".", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, ",%", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, ":}{++_)(*^%$$%#@!@$@$%", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, " ", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, "g", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, "A1c", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, ",a.sd", false);
        }

        [Test]
        public void TestIsTextValidAll()
        {
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "a", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "Ad", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "12", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, " ", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, ";a", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "%$^&**(", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.All, "^^gg^^66jjJJ", true);
        }

        [Test]
        public void TestIsTextValidWildcardPattern()
        {
            tbr.PatternString = "?";
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "1", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ",", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "aa", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ";a", false);

            tbr.PatternString = "*";
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "1", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ",", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "aa", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ";a", true);

            tbr.PatternString = "*@*.??";
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "foo@bar.pl", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "foo@bar.com", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "@bar.pl", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "foo@bar.bar.pl", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "foo_bar@bar.foo.pl", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "", false);

            tbr.PatternString = "??$*\\?";
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a6$fds\\0", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a6$fds\\\\0", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a6$\\0", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a6$.()\\0", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "a6$fds\\{", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "", false);

            tbr.PatternString = ". $ ^ { [* ( | ) ?+ \\";
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ". $ ^ { [a ( | )  + \\", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ". $ ^ { [... ( | ) a+ \\", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ". $ ^ { [*?* ( | ) ?+ \\", true);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ". $ ^ { [*?* ( | ) ?+ \\\\", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, ".. $ ^ { [*?* ( | ) ?+ \\", false);
            TestsMethods.TestIsTextValidAssert(TextBoxRegex.Patterns.WildcardPattern, "", false);
        }

        [Test]
        public void TestGetRegexString()
        {   
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.None, "", "^()$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.SmallLetters, "", "^([a-z]+)$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CapitalLetters, "", "^([A-Z]+)$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.Digits, "", "^([0-9]+)$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.NonAlphaNumericCharacters, "", "^([^a-zA-Z0-9]+)$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "", "^()$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "1", "^(1)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "#$", "^(#\\$)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "asbvcb", "^(asbvcb)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "foo.bar", "^(foo\\.bar)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "*@*.com", "^((.+)@(.+)\\.com)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.WildcardPattern, "*.???", "^((.+)\\.(.)(.)(.))$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CharacterCollection, "", "^()$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CharacterCollection, "ab", "^([ab]+)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CharacterCollection, "HGJKF", "^([HGJKF]+)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CharacterCollection, "123", "^([123]+)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.CharacterCollection, "1^a]s$LK([", "^([1\\^a\\]s\\$LK\\(\\[]+)$");

            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.RegexPattern, "", "");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.RegexPattern, "^(aA1<<)$", "^(aA1<<)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.RegexPattern, "^([ab][.]+)$", "^([ab][.]+)$");
            TestsMethods.TestGetRegexStringAssert(TextBoxRegex.Patterns.RegexPattern, "^(1([ab]*))$", "^(1([ab]*))$");
        }        

        [Test]
        public void TestEscapeRegularExpressionsOperators()
        {
            string pattern;
            
            pattern = ".a$b^e?{r[g(g|f)d*a+sd\\a";
            tbr.Pattern = TextBoxRegex.Patterns.WildcardPattern;
            Assert.AreEqual("\\.a\\$b\\^e?\\{r\\[g\\(g\\|f\\)d*a\\+sd\\\\a",
                tbr.EscapeRegularExpressionsOperators(pattern));

            pattern = ".a$b^e?{r[g(g|f)d*a+sd\\a";
            tbr.Pattern = TextBoxRegex.Patterns.Digits;
            Assert.AreEqual("\\.a\\$b\\^e\\?\\{r\\[g\\(g\\|f\\)d\\*a\\+sd\\\\a",
                tbr.EscapeRegularExpressionsOperators(pattern));

            pattern = ".a$b^e?{r[g(]g|f)d*a+s}d\\a";
            tbr.Pattern = TextBoxRegex.Patterns.CapitalLetters;
            Assert.AreEqual("\\.a\\$b\\^e\\?\\{r\\[g\\(\\]g\\|f\\)d\\*a\\+s\\}d\\\\a",
                tbr.EscapeRegularExpressionsOperators(pattern));

            pattern = "abc]";
            tbr.Pattern = TextBoxRegex.Patterns.SmallLetters;
            Assert.AreEqual("abc\\]",
                tbr.EscapeRegularExpressionsOperators(pattern));
        }

        [Test]
        public void TestReplaceWildcardCharacters()
        {
            string pattern = "foo*bar?";

            Assert.AreEqual("foo(.+)bar(.)", tbr.ReplaceWildcardCharacters(pattern));
        }

        [Test]
        public void TestAddBeginningOfStringAndEndOfStringMetacharacters()
        {
            Assert.AreEqual("^(foo)$", tbr.AddBeginningOfStringAndEndOfStringMetacharacters("foo"));
        }

        [Test]
        public void TestAddCharacterCollectionMetacharacters()
        {
            Assert.AreEqual("[as;'12AS]+", tbr.AddCharacterCollectionMetacharacters("as;'12AS"));
        }
    }
}
#endif
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
using NUnit.Framework;

namespace Chopeen
{
    /// <summary>
    /// <exclude/>
    /// </summary>
    [TestFixture]
    public class TestTextBoxRegexPattern
    {
        private TextBoxRegex.Patterns pattern;

        [SetUp]
        public void CreatePatterns()
        {
            pattern = new TextBoxRegex.Patterns();
        }

        [Test]
        public void TestPatterns()
        {
            Assert.AreEqual(0, (int) TextBoxRegex.Patterns.None);
            Assert.AreEqual(1, (int) TextBoxRegex.Patterns.SmallLetters);
            Assert.AreEqual(2, (int) TextBoxRegex.Patterns.CapitalLetters);
            Assert.AreEqual(4, (int) TextBoxRegex.Patterns.Digits);
            Assert.AreEqual(8, (int) TextBoxRegex.Patterns.NonAlphaNumericCharacters);
            Assert.AreEqual(16, (int) TextBoxRegex.Patterns.CharacterCollection);
            Assert.AreEqual(32, (int) TextBoxRegex.Patterns.WildcardPattern);
            Assert.AreEqual(64, (int) TextBoxRegex.Patterns.RegexPattern);
            Assert.AreEqual(15, (int) TextBoxRegex.Patterns.All);
        }

        [Test]
        public void TestPatternAssignment()
        {
            pattern = TextBoxRegex.Patterns.SmallLetters;
            Assert.AreEqual(TextBoxRegex.Patterns.SmallLetters, pattern);
            Assert.AreEqual(1, (int) pattern);

            pattern = TextBoxRegex.Patterns.CapitalLetters | TextBoxRegex.Patterns.NonAlphaNumericCharacters;
            Assert.AreEqual(TextBoxRegex.Patterns.CapitalLetters | TextBoxRegex.Patterns.NonAlphaNumericCharacters, pattern);
            Assert.AreEqual(10, (int) pattern);
        }
    }
}
#endif
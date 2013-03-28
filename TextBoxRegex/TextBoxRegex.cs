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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Chopeen
{
	/// <summary>
	/// <para>TextBoxRegex is an exhanced text box control.</para>
	/// <para>It can check the entered text against a regular expression pattern.</para>
	/// <para>The pattern can be specified using a regular expression string or a wildcard string as well as chosen from
	/// the predefined patterns.</para>
	/// </summary>
	public class TextBoxRegex : System.Windows.Forms.TextBox
	{
        #region Property variables
        private Color validTextBackColor;
        private Color invalidTextBackColor;
        private bool useColors;
        private string patternString;
        private Patterns pattern;
        private bool useInvalidTextException;
        #endregion

        #region Private variables
        private Regex patternRegex;
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TextBoxRegex"/> class.</para>
        /// </summary>
        public TextBoxRegex()
		{
            textBoxRegex(Color.LightGreen, Color.LightPink, true,
                TextBoxRegex.Patterns.None, "",
                true);
		}

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TextBoxRegex"/> class with specified data.</para>
        /// </summary>
        /// <param name="ValidTextBackColor">The background color of the control when user's input matches the chosen
        /// pattern.</param>
        /// <param name="InvalidTextBackColor">The background color of the control when user's input does not match
        /// the chosen pattern.</param>
        /// <param name="UseColors">When <b>true</b>, <see cref="ValidTextBackColor"/> and <see cref="InvalidTextBackColor"/> colors
        /// are used.</param>
        /// <param name="Pattern">The pattern.</param>
        /// <param name="PatternString">The pattern string (used only with <see cref="Patterns.CharacterCollection"/>,
        /// <see cref="Patterns.WildcardPattern"/> or <see cref="Patterns.RegexPattern"/>).</param>
        /// <param name="UseInvalidTextException">When <b>true</b>, an <see cref="InvalidTextException"/> is thrown
        /// by <see cref="TextValidated"/> to indicate that user's input is not valid.</param>
        public TextBoxRegex(Color ValidTextBackColor, Color InvalidTextBackColor, bool UseColors,
            TextBoxRegex.Patterns Pattern, string PatternString,
            bool UseInvalidTextException)
        {
            textBoxRegex(ValidTextBackColor, InvalidTextBackColor, UseColors,
                Pattern, PatternString,
                UseInvalidTextException);
        }

        private void textBoxRegex(Color ValidTextBackColor, Color InvalidTextBackColor, bool UseColors,
            TextBoxRegex.Patterns Pattern, string PatternString,
            bool UseInvalidTextException)
        {
            this.patternRegex = new Regex("^()$");

            this.ValidTextBackColor = ValidTextBackColor;
            this.InvalidTextBackColor = InvalidTextBackColor;
            this.UseColors = UseColors;
            this.Pattern = Pattern;
            this.PatternString = PatternString;
            this.UseInvalidTextException = UseInvalidTextException;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// <para>Gets or sets a color used as a background color of the control when user's input matches
        /// the chosen pattern.</para>
        /// <seealso cref="InvalidTextBackColor"/>
        /// <seealso cref="UseColors"/>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="PatternString"/>
        /// </summary>
        [Description("A color used as a background color of the control when user's input matches the pattern.")]
        [Category("Regex")]
        public Color ValidTextBackColor
        {
            get
            {
                return validTextBackColor;
            }
            set
            {
                validTextBackColor = value;
                
                Refresh();
            }
        }

        /// <summary>
        /// <para>Gets or sets a color used as a background color of the control when user's input does not match
        /// the chosen pattern.</para>
        /// <seealso cref="ValidTextBackColor"/>
        /// <seealso cref="UseColors"/>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="PatternString"/>
        /// </summary>
        [Description("A color used as a background color of the control when user's input does not match the pattern.")]
        [Category("Regex")]
        public Color InvalidTextBackColor
        {
            get
            {
                return invalidTextBackColor;
            }
            set
            {
                invalidTextBackColor = value;

                Refresh();
            }
        }

        /// <summary>
        /// <para>Gets or sets a value indicating whether the background color of the control should change
        /// to show that user's input does or does not match the chosen pattern.</para>
        /// <seealso cref="InvalidTextBackColor"/>
        /// <seealso cref="ValidTextBackColor"/>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="PatternString"/>
        /// </summary>
        [Description("When true, colors are used to indicate whether user's input matches the pattern or not.")]
        [Category("Regex")]
        public bool UseColors
        {
            get
            {
                return useColors;
            }
            set
            {
                useColors = value;

                Refresh();
            }
        }

        /// <summary>
        /// <para>Gets or sets the pattern used to check user's input.</para>
        /// <para>Pattern property takes one of the values defined in <see cref="Patterns"/> enumeration.</para>
        /// <seealso cref="PatternString"/>
        /// <seealso cref="Patterns"/>
        /// </summary>
        [Description("The pattern.")]
        [Category("Regex")]
        public Patterns Pattern
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;

                SetPatternRegex();
                Refresh();
            }
        }

        /// <summary>
        /// <para>Gets or sets a string defining the correct pattern.</para>
        /// <para>Used only when <see cref="Pattern"/> property equals <see cref="Patterns.CharacterCollection"/>,
        /// <see cref="Patterns.WildcardPattern"/> or <see cref="Patterns.RegexPattern"/>.</para>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="Patterns"/>
        /// </summary>
        /// <example>
        /// <code>
        /// // example 1 - a collection of characters ("a", "b", "c", "D", "E", "F", "1", "2", "3", "@")
        /// TextBoxRegex TextBoxRegex1 = new TextBoxRegex();
        /// TextBoxRegex1.PatternString = "abcDEF123@";
        /// TextBoxRegex1.Pattern = TextBoxRegex.Patterns.CharacterCollection;
        /// 
        /// // example 2 - a wildcard pattern (Polish e-mail address)
        /// TextBoxRegex TextBoxRegex2 = new TextBoxRegex();
        /// TextBoxRegex2.PatternString = "*@*.pl";
        /// TextBoxRegex2.Pattern = TextBoxRegex.Patterns.WildcardPattern;
        /// 
        /// // example 3 - a regular expression (Polish ZIP code)
        /// TextBoxRegex TextBoxRegex3 = new TextBoxRegex();
        /// TextBoxRegex3.PatternString = "^([0-9][0-9]-[0-9][0-9][0-9])$";
        /// TextBoxRegex3.Pattern = TextBoxRegex.Patterns.RegexPattern;
        /// </code>
        /// </example>
        [Description("If using CharacterCollection, WildcardPattern or RegexPattern, specify the pattern string here.")]
        [Category("Regex")]
        public string PatternString
        {
            get
            {
                return patternString;
            }
            set
            {
                patternString = value;

                SetPatternRegex();
                Refresh();
            }
        }

        /// <summary>
        /// <para>Gets or sets a value indicating how <see cref="TextValidated"/> property works.</para>
        /// <para>If <b>true</b>, an <see cref="InvalidTextException"/> is thrown by <see cref="TextValidated"/> property
        /// in case the text content of the <see cref="TextBoxRegex"/> control does not match chosen pattern.</para>
        /// <para>If <b>false</b>, the <see cref="TextValidated"/> property returns an empty string in such case.</para>
        /// </summary>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="PatternString"/>
        [Description("If true, InvalidTextException is thrown by TextValidated, when Text doesn't match the pattern; else TextValidated returns an empty string in such case.")]
        [Category("Regex")]
        public bool UseInvalidTextException
        {
            get
            {
                return useInvalidTextException;
            }
            set
            {
                useInvalidTextException = value;
            }
        }

        /// <summary>
        /// <para>Gets the text content of the <see cref="TextBoxRegex"/> control, on condition that it matches the chosen
        /// pattern.</para>
        /// <para>Otherwise, it throws an <see cref="InvalidTextException"/> when <see cref="UseInvalidTextException"/>
        /// property is <b>true</b> or returns an empty string when it is <b>false</b>.</para>
        /// <seealso cref="Pattern"/>
        /// <seealso cref="PatternString"/>
        /// </summary>
        /// <example>
        /// <code>
        /// // an example of using TextValidated property in a <b>try-catch</b> block
        /// textBoxRegex1.UseInvalidTextException = true;
        /// 
        /// try
        /// {   // there is no match
        ///     MessageBox.Show("You entered" + textBoxRegex1.TextValidated + "and you were right.");
        /// }
        /// catch (Chopeen.InvalidTextException ex)
        /// {   // there is no match
        ///     MessageBox.Show("You were wrong.");
        /// }
        /// </code>
        /// </example>
        [Description("Read-only. Gets the control text.\n" + "Refer to UseInvalidTextException description before using it.")]
        [Category("Regex")]
        public string TextValidated
        {
            get
            {
                if (IsTextValid())
                {
                    return this.Text;
                }
                else
                {
                    if (this.UseInvalidTextException)
                    {
                        throw(new InvalidTextException("The Text property value (\"" +
                            this.Text + "\") does not match chosen pattern (" +
                            this.Pattern.ToString() + ")."));
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }
        #endregion

        #region Public enums
        /// <summary>
        /// Patterns that the text is checked against.
        /// </summary>
        [Flags]
        public enum Patterns : int
        {
            /// <summary>
            /// an empty string
            /// </summary>
            None = 0,
            /// <summary>
            /// a string consisting of small letters (<c>[a-z]</c>)
            /// </summary>
            SmallLetters = 1,
            /// <summary>
            /// a string consisting of capital letters (<c>[A-Z]</c>)
            /// </summary>
            CapitalLetters = 2,
            /// <summary>
            /// a string consisting of digits (<c>[0-9]</c>)
            /// </summary>
            Digits = 4,
            /// <summary>
            /// a string consisting of characters other than letters or digits (. , ? ! / etc)
            /// </summary>
            NonAlphaNumericCharacters = 8,
            /// <summary>
            /// a string of characters from a user-defined collection
            /// </summary>
            CharacterCollection = 16,
            /// <summary>
            /// a string specified with a DOS-like wildcard pattern (* represents a collection of one or more characters; ? represents a single character)
            /// </summary>
            WildcardPattern = 32,
            /// <summary>
            /// a string specified with a regular expression pattern
            /// </summary>
            RegexPattern = 64,
            /// <summary>
            /// a string consisting of any characters<br/>
            /// (<c>SmallLetters | CapitalLetters | Digits | NonAlphaNumericCharacters</c>)
            /// </summary>
            All = SmallLetters |
                CapitalLetters |
                Digits |
                NonAlphaNumericCharacters
        }
        #endregion

        #region Internal methods
        internal bool IsTextValid()
        {
            return this.patternRegex.IsMatch(this.Text);
        }

        internal Regex GetRegex(string str)
        {
            try
            {
                return new Regex(str);
            }
            catch (System.ArgumentException ex)
            {
                ExceptionMessageBox(ex,
                    "A problem occured while creating a Regex object.\nProbably the PatternString property is not correct.",
                    "Error");

                return null;
            }
        }

        internal void SetPatternRegex()
        {
            this.patternRegex = GetRegex(GetRegexString());
        }

        internal string GetRegexString()
        { // TODO: I don't like duplication in this method, but I don't what to do about it
            string str = "";

            switch (this.Pattern)
            {
                case TextBoxRegex.Patterns.None:
                    str = "";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.SmallLetters:
                    str = "[a-z]+";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.CapitalLetters:
                    str = "[A-Z]+";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.Digits:
                    str = "[0-9]+";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.NonAlphaNumericCharacters:
                    str = "[^a-zA-Z0-9]+";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.WildcardPattern:
                    str = this.PatternString;
                    str = EscapeRegularExpressionsOperators(str);
                    str = ReplaceWildcardCharacters(str);
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.CharacterCollection:
                    str = this.PatternString;
                    str = EscapeRegularExpressionsOperators(str);
                    str = AddCharacterCollectionMetacharacters(str);
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
                case TextBoxRegex.Patterns.RegexPattern:
                    str = this.PatternString;
                    break;
                case TextBoxRegex.Patterns.All:
                    str = ".+";
                    str = AddBeginningOfStringAndEndOfStringMetacharacters(str);
                    break;
            }

            return str;
        }

        internal string EscapeRegularExpressionsOperators(string str)
        {
            str = str.Replace("\\", "\\\\");   // it is crucial to replace "\" first
            str = str.Replace(".", "\\.");
            str = str.Replace("^", "\\^");
            str = str.Replace("$", "\\$");
            str = str.Replace("+", "\\+");
            str = str.Replace("|", "\\|");
            str = str.Replace("{", "\\{");
            str = str.Replace("}", "\\}");
            str = str.Replace("[", "\\[");
            str = str.Replace("]", "\\]");
            str = str.Replace("(", "\\(");
            str = str.Replace(")", "\\)");
            
            if (this.Pattern != TextBoxRegex.Patterns.WildcardPattern)
            {
                str = str.Replace("*", "\\*");
                str = str.Replace("?", "\\?");
            }

            return str;
        }

        internal string ReplaceWildcardCharacters(string str)
        {
            str = str.Replace("*", "(.+)");
            str = str.Replace("?", "(.)");

            return str;
        }

        internal string AddBeginningOfStringAndEndOfStringMetacharacters(string str)
        {
            str = "^(" + str + ")$";

            return str;
        }

        internal string AddCharacterCollectionMetacharacters(string str)
        {
            if (str.Length != 0)
            {
                // I don't like this condition but new Regex("^{[]+}$") results in:
                //  An unhandled exception of type 'System.ArgumentException' occurred in system.dll
                //  Additional information: parsing "^{[]+}$" - Unterminated [] set.
                str = "[" + str + "]+";
            }

            return str;
        }

        internal void SetBackColor()
        {   
			if (this.Enabled)
			{
				if (this.UseColors && this.Text.Length > 0)
				{
					this.BackColor = (this.IsTextValid() ? ValidTextBackColor : InvalidTextBackColor);
				}
				else
				{
					this.BackColor = SystemColors.Window;
				}
			}
			else
			{
				this.BackColor = SystemColors.Control;
			}
        }

        internal void ExceptionMessageBox(SystemException pEx, string pMsg, string pCaption)
        {
            MessageBox.Show(pMsg + "\n\n" +
                "Message: " + pEx.Message + "\n\n" +
                "Source: " + pEx.Source + "\n" +
                "TargetSite: " + pEx.TargetSite + "\n\n" +
                "StackTrace:\n" + pEx.StackTrace,
                pCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        #endregion

        #region Overridden methods
        /// <summary>
        /// <para>Raises the <see cref="Control.KeyUp"/> event and calls <see cref="Refresh"/> method.</para>
        /// </summary>
        /// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyUp(e);

            Refresh();
        }

        /// <summary>
        /// <para>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</para>
        /// <para>The background color of the control is set to <see cref="ValidTextBackColor"/>
        /// or <see cref="InvalidTextBackColor"/> (unless <see cref="UseColors"/> is <b>false</b> or the control is
        /// disabled).</para>
        /// <seealso cref="ValidTextBackColor"/>
        /// <seealso cref="InvalidTextBackColor"/>
        /// <seealso cref="UseColors"/>
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            SetBackColor();
        }

		/// <summary>
		/// <para>Raises the System.Windows.Forms.Control.EnabledChanged event.</para>
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);

			SetBackColor();
		}
        #endregion
	}
}
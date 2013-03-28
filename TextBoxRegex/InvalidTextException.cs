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
using System.Runtime.Serialization;

namespace Chopeen
{
    /// <summary>
    /// <para>The exception that is thrown by <see cref="TextBoxRegex"/> control when its <see cref="TextBoxRegex.TextValidated"/>
    /// property is used to read the text that does not match the chosen pattern.</para>
    /// </summary>
    [Serializable]
    public class InvalidTextException : Exception
    {
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InvalidTextException"/> class.</para>
        /// </summary>
        public InvalidTextException() : base()
        {
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InvalidTextException"/> class with a specified error message.</para>
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidTextException(string message) : base(message)
        {
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InvalidTextException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.</para>
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException
        /// parameter is not a null reference (<b>Nothing</b> in Visual Basic), the current exception is raised in a catch block
        /// that handles the inner exception.</param>
        public InvalidTextException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InvalidTextException"/> class with serialized data.</para>
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds
        /// the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.</param>
        protected InvalidTextException(SerializationInfo info, StreamingContext context) : base (info, context)
        {
        }
    }
}
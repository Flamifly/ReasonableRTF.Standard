/*
 * MIT License
 * 
 * Copyright (c) 2024 Brian Tobin
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

namespace ReasonableRTF.Helper
{
	/// <summary>
	/// Contains Methods, which throws an Exception.
	/// </summary>
    internal static class ThrowHelper
    {
		/// <summary>
		/// Throws an <see cref="IndexOutOfRangeException"/>
		/// </summary>
		/// <exception cref="IndexOutOfRangeException"></exception>
		internal static void IndexOutOfRange() => throw new IndexOutOfRangeException();
		/// <summary>
		/// Throws an <see cref="System.ArgumentException"/>
		/// </summary>
		/// <param name="message">The Message of the <see cref="System.ArgumentException"/>.</param>
		/// <param name="paramName">The Name of the Parameter, which was invalid.</param>
		/// <exception cref="System.ArgumentException"></exception>
		internal static void ArgumentException(string? message, string? paramName) => throw new ArgumentException(message, paramName);
		/// <summary>
        /// Throws an <see cref="System.IO.IOException"/>
        /// </summary>
        /// <param name="message">The Message of the <see cref="System.IO.IOException"/>.</param>
        /// <exception cref="System.IO.IOException"></exception>
        internal static void IOException(string message) => throw new IOException(message);
	}
}

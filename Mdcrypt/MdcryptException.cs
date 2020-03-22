/*
	Copyright (c) 2015 Ki

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in
	all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	THE SOFTWARE.
*/

using System;

namespace Mdcrypt {
	/// <summary>
	/// The exception that is thrown when a handled error occurred during the protection process.
	/// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
	public class MdcryptException : Exception {
#pragma warning restore CA1032 // Implement standard exception constructors
		/// <summary>
		///     Initializes a new instance of the <see cref="MdcryptException" /> class.
		/// </summary>
		/// <param name="innerException">The inner exception, or null if no exception is associated with the error.</param>
		public MdcryptException(Exception innerException) : base("Exception occurred during the protection process.", innerException) {
		}
	}
}
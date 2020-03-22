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
	/// Various presets of protections.
	/// </summary>
	public enum ProtectionPreset {
		/// <summary> The protection does not belong to any preset. </summary>
		None = 0,

		/// <summary> The protection provides basic security. </summary>
		Minimum = 1,

		/// <summary> The protection provides normal security for public release. </summary>
		Normal = 2,

		/// <summary> The protection provides better security with observable performance impact. </summary>
		Aggressive = 3,

		/// <summary> The protection provides strongest security with possible incompatibility. </summary>
		Maximum = 4
	}

	/// <summary>
	/// Indicates the <see cref="Protection" /> must initialize before the specified protections.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class BeforeProtectionAttribute : Attribute {
		/// <summary>
		/// Gets the full IDs of the specified protections.
		/// </summary>
		/// <value>The IDs of protections.</value>
		public string[] Ids { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BeforeProtectionAttribute" /> class.
		/// </summary>
		/// <param name="ids">The full IDs of the specified protections.</param>
		public BeforeProtectionAttribute(params string[] ids) {
			Ids = ids;
		}
	}

	/// <summary>
	/// Indicates the <see cref="Protection" /> must initialize after the specified protections.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class AfterProtectionAttribute : Attribute {
		/// <summary>
		/// Gets the full IDs of the specified protections.
		/// </summary>
		/// <value>The IDs of protections.</value>
		public string[] Ids { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BeforeProtectionAttribute" /> class.
		/// </summary>
		/// <param name="ids">The full IDs of the specified protections.</param>
		public AfterProtectionAttribute(params string[] ids) {
			Ids = ids;
		}
	}

	/// <summary>
	/// Base class of Mdcrypt protections.
	/// </summary>
	/// <remarks>
	/// A parameterless constructor must exists in derived classes to enable plugin discovery.
	/// </remarks>
	public abstract class Protection {
		/// <summary>
		/// Gets the identifier of component used by users.
		/// </summary>
		/// <value>The identifier of component.</value>
		public abstract string Id { get; }

		/// <summary>
		/// Gets the name of component.
		/// </summary>
		/// <value>The name of component.</value>
		public abstract string Name { get; }

		/// <summary>
		/// Gets the description of component.
		/// </summary>
		/// <value>The description of component.</value>
		public abstract string Description { get; }

		/// <summary>
		/// Gets the preset this protection is in.
		/// </summary>
		/// <value>The protection's preset.</value>
		public abstract ProtectionPreset Preset { get; }
	}
}

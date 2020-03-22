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
using System.Collections.Generic;
using dnlib.DotNet;

namespace Mdcrypt {
	/// <summary>
	/// Identifies a member that is a setting item.
	/// </summary>
	public sealed class ProtectionSettingAttribute : Attribute {
	}

	/// <summary>
	/// Protection settings for a certain component
	/// </summary>
	public abstract class ProtectionSettings {
	}

	/// <summary>
	/// Parameters of <see cref="Protection" />.
	/// </summary>
	public sealed class ProtectionParameters {
		/// <summary>
		/// Gets the targets of protection.
		/// Possible targets are module, types, methods, fields, events, properties.
		/// </summary>
		/// <value>A list of protection targets.</value>
		public IList<IDnlibDef> Targets { get; }

		/// <summary>
		/// Get default settings of the protection.
		/// </summary>
		public ProtectionSettings DefaultSettings { get; }

		/// <summary>
		/// Get overridden settings of the specified target.
		/// </summary>
		public IDictionary<IDnlibDef, ProtectionSettings> OverrideSettings { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProtectionParameters" /> class.
		/// </summary>
		/// <param name="targets">The protection targets.</param>
		/// <param name="defaultSettings">The protection default settings</param>
		internal ProtectionParameters(IList<IDnlibDef> targets, ProtectionSettings defaultSettings) {
			if (targets is null)
				throw new ArgumentNullException(nameof(targets));
			if (defaultSettings is null)
				throw new ArgumentNullException(nameof(defaultSettings));

			Targets = targets;
			DefaultSettings = defaultSettings;
			OverrideSettings = new Dictionary<IDnlibDef, ProtectionSettings>();
		}
	}
}

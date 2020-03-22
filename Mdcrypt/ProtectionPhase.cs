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
	/// Targets of protection.
	/// </summary>
	[Flags]
	public enum ProtectionTargets {
		/// <summary> Type definitions. </summary>
		Types = 1,

		/// <summary> Method definitions. </summary>
		Methods = 2,

		/// <summary> Field definitions. </summary>
		Fields = 4,

		/// <summary> Event definitions. </summary>
		Events = 8,

		/// <summary> Property definitions. </summary>
		Properties = 16,

		/// <summary> All member definitions (i.e. type, methods, fields, events and properties). </summary>
		AllMembers = Types | Methods | Fields | Events | Properties,

		/// <summary> Module definitions. </summary>
		Modules = 32,

		/// <summary> All definitions (i.e. All member definitions and modules). </summary>
		AllDefinitions = AllMembers | Modules
	}

	/// <summary>
	/// Base class of protection phases.
	/// </summary>
	public abstract class ProtectionPhase {
		/// <summary>
		/// Gets the parent component.
		/// </summary>
		/// <value>The parent component.</value>
		public Protection Parent { get; }

		/// <summary>
		/// Gets the name of the phase.
		/// </summary>
		/// <value>The name of phase.</value>
		public abstract string Name { get; }

		/// <summary>
		/// Gets the targets of protection.
		/// </summary>
		/// <value>The protection targets.</value>
		public abstract ProtectionTargets Targets { get; }

		/// <summary>
		/// Gets a value indicating whether this phase process all targets, not just the targets that requires the component.
		/// </summary>
		/// <value><c>true</c> if this phase process all targets; otherwise, <c>false</c>.</value>
		public virtual bool ProcessAll => false;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProtectionPhase" /> class.
		/// </summary>
		/// <param name="parent">The parent component of this phase.</param>
		protected ProtectionPhase(Protection parent) {
			Parent = parent;
		}

		/// <summary>
		/// Executes the protection phase.
		/// </summary>
		/// <param name="context">The working context.</param>
		/// <param name="parameters">The parameters of protection.</param>
		protected abstract void Execute(MdcryptContext context, ProtectionParameters parameters);
	}
}

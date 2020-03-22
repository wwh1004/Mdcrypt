using System;

namespace Mdcrypt {
	/// <summary>
	/// Mdcrypt engine
	/// </summary>
	public sealed class MdcryptEngine {
		/// <summary>
		/// Context
		/// </summary>
		public MdcryptContext Context { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">Context for engine</param>
		public MdcryptEngine(MdcryptContext context) {
			if (context is null)
				throw new ArgumentNullException(nameof(context));

			Context = context;
		}
	}
}

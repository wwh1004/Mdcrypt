using System;

namespace Mdcrypt {
	/// <summary>
	/// Mdcrypt engine
	/// </summary>
	public sealed class MdcEngine {
		private readonly MdcContext _context;

		public MdcContext Context => _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">Context for engine</param>
		public MdcEngine(MdcContext context) {
			if (context is null)
				throw new ArgumentNullException(nameof(context));

			_context = context;
		}
	}
}

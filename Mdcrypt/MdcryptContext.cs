using System;
using System.Collections.Generic;
using System.Threading;
using dnlib.DotNet;

namespace Mdcrypt {
	/// <summary>
	/// Mdcrypt context
	/// </summary>
	public sealed class MdcryptContext {
		/// <summary>
		/// Manifest assembly
		/// </summary>
		public AssemblyDef Assembly { get; }

		/// <summary>
		/// Manifest module in <see cref="Assembly"/>
		/// </summary>
		public ModuleDefMD ManifestModule => (ModuleDefMD)Assembly.ManifestModule;

		/// <summary>
		/// Additional assemblies, which will be merged or embedded
		/// </summary>
		public IList<AssemblyDef> AdditionalAssemblies { get; } = new List<AssemblyDef>();

		/// <summary>
		/// Gets the current processing pipeline.
		/// </summary>
		public ProtectionPipeline Pipeline { get;  }

		/// <summary>
		///	Gets the token used to indicate cancellation
		/// </summary>
		public CancellationToken CancellationToken { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="module">The module to obfuscate and encrypt</param>
		/// <param name="cancellationToken">Cancellation token</param>
		public MdcryptContext(ModuleDefMD module, CancellationToken cancellationToken) {
			if (module is null)
				throw new ArgumentNullException(nameof(module));

			Assembly = module.Assembly;
			Pipeline = new ProtectionPipeline();
			CancellationToken = cancellationToken;
		}

		/// <summary>
		/// Throws a System.OperationCanceledException if protection process has been canceled.
		/// </summary>
		/// <exception cref="OperationCanceledException">The protection process is canceled.</exception>
		public void CheckCancellation() {
			CancellationToken.ThrowIfCancellationRequested();
		}
	}
}

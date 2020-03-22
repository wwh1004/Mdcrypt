using dnlib.DotNet;

namespace Mdcrypt {
	/// <summary>
	/// Mdcrypt utilities
	/// </summary>
	public static class MdcryptUtils {
		/// <summary>
		/// Load <see cref="ModuleDefMD"/>
		/// </summary>
		/// <param name="data">.NET module/assembly</param>
		/// <param name="assemblyResolver">Assembly resolver</param>
		/// <returns></returns>
		public static ModuleDefMD LoadModule(byte[] data, out AssemblyResolver assemblyResolver) {
			assemblyResolver = new AssemblyResolver();
			var context = new ModuleContext(assemblyResolver);
			assemblyResolver.EnableTypeDefCache = false;
			assemblyResolver.DefaultModuleContext = context;
			var options = new ModuleCreationOptions() {
				Context = context,
				TryToLoadPdbFromDisk = false
			};
			return ModuleDefMD.Load(data, options);
		}
	}
}

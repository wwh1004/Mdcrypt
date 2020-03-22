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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using dnlib.DotNet;

namespace Mdcrypt {
	/// <summary>
	/// Various stages in <see cref="ProtectionPipeline" />.
	/// </summary>
	public enum PipelineStage {
		/// <summary>
		/// Mdcrypt engine inspects the loaded modules and makes necessary changes.
		/// This stage occurs only once per pipeline run.
		/// </summary>
		Inspection,

		/// <summary>
		/// Mdcrypt engine merges the assemblies if merger is present.
		/// This stage occurs only once per pipeline run.
		/// </summary>
		Merge,

		/// <summary>
		/// Mdcrypt engine begins to process a module.
		/// This stage occurs once per module.
		/// </summary>
		BeginModule,

		/// <summary>
		/// Mdcrypt engine processes a module.
		/// This stage occurs once per module.
		/// </summary>
		ProcessModule,

		/// <summary>
		/// Mdcrypt engine optimizes opcodes of the method bodys.
		/// This stage occurs once per module.
		/// </summary>
		OptimizeMethods,

		/// <summary>
		/// Mdcrypt engine finishes processing a module.
		/// This stage occurs once per module.
		/// </summary>
		EndModule,

		/// <summary>
		/// Mdcrypt engine writes the module to byte array.
		/// This stage occurs once per module, after all processing of modules are completed.
		/// </summary>
		WriteModule,

		///// <summary>
		///// Mdcrypt engine generates debug symbols.
		///// This stage occurs only once per pipeline run.
		///// </summary>
		//Debug,

		/// <summary>
		/// Mdcrypt engine packs up the output if packer is present.
		/// This stage occurs only once per pipeline run.
		/// </summary>
		Pack,

		/// <summary>
		/// Mdcrypt engine saves the output.
		/// This stage occurs only once per pipeline run.
		/// </summary>
		SaveModule
	}

	/// <summary>
	/// Protection processing pipeline.
	/// </summary>
	public class ProtectionPipeline {
		private readonly Dictionary<PipelineStage, List<ProtectionPhase>> postStage;
		private readonly Dictionary<PipelineStage, List<ProtectionPhase>> preStage;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProtectionPipeline" /> class.
		/// </summary>
		internal ProtectionPipeline() {
			var stages = (PipelineStage[])Enum.GetValues(typeof(PipelineStage));
			preStage = stages.ToDictionary(stage => stage, _ => new List<ProtectionPhase>());
			postStage = stages.ToDictionary(stage => stage, _ => new List<ProtectionPhase>());
		}

		/// <summary>
		/// Inserts the phase into pre-processing pipeline of the specified stage.
		/// </summary>
		/// <param name="stage">The pipeline stage.</param>
		/// <param name="phase">The protection phase.</param>
		public void InsertPreStage(PipelineStage stage, ProtectionPhase phase) {
			preStage[stage].Add(phase);
		}

		/// <summary>
		/// Inserts the phase into post-processing pipeline of the specified stage.
		/// </summary>
		/// <param name="stage">The pipeline stage.</param>
		/// <param name="phase">The protection phase.</param>
		public void InsertPostStage(PipelineStage stage, ProtectionPhase phase) {
			postStage[stage].Add(phase);
		}

		/// <summary>
		/// Gets the phase with the specified type in the pipeline.
		/// </summary>
		/// <typeparam name="T">The type of the phase.</typeparam>
		/// <param name="phase">The phase with specified type in the pipeline.</param>
		/// <returns></returns>
		public bool TryGetPhase<T>([NotNullWhen(true)] out T? phase) where T : ProtectionPhase {
			foreach (var phases in preStage.Values)
				foreach (ProtectionPhase item in phases) {
					if (item is T t) {
						phase = t;
						return true;
					}
				}
			foreach (var phases in postStage.Values)
				foreach (ProtectionPhase item in phases) {
					if (item is T t) {
						phase = t;
						return true;
					}
				}
			phase = default;
			return false;
		}

		///// <summary>
		///// Execute the specified pipeline stage with pre-processing and post-processing.
		///// </summary>
		///// <param name="stage">The pipeline stage.</param>
		///// <param name="func">The stage function.</param>
		///// <param name="targets">The target list of the stage.</param>
		///// <param name="context">The working context.</param>
		//internal void ExecuteStage(PipelineStage stage, Action<MdcryptContext> func, Func<IList<IDnlibDef>> targets, MdcryptContext context) {
		//	foreach (ProtectionPhase pre in preStage[stage]) {
		//		context.CheckCancellation();
		//		context.Logger.DebugFormat("Executing '{0}' phase...", pre.Name);
		//		pre.Execute(context, new ProtectionParameters(pre.Parent, Filter(context, targets(), pre)));
		//	}
		//	context.CheckCancellation();
		//	func(context);
		//	context.CheckCancellation();
		//	foreach (ProtectionPhase post in postStage[stage]) {
		//		context.Logger.DebugFormat("Executing '{0}' phase...", post.Name);
		//		post.Execute(context, new ProtectionParameters(post.Parent, Filter(context, targets(), post)));
		//		context.CheckCancellation();
		//	}
		//}

		///// <summary>
		///// Returns only the targets with the specified type and used by specified component.
		///// </summary>
		///// <param name="context">The working context.</param>
		///// <param name="targets">List of targets.</param>
		///// <param name="phase">The component phase.</param>
		///// <returns>Filtered targets.</returns>
		//private static IList<IDnlibDef> Filter(MdcryptContext context, IList<IDnlibDef> targets, ProtectionPhase phase) {
		//	ProtectionTargets targetType = phase.Targets;

		//	IEnumerable<IDnlibDef> filter = targets;
		//	if ((targetType & ProtectionTargets.Modules) == 0)
		//		filter = filter.Where(def => !(def is ModuleDef));
		//	if ((targetType & ProtectionTargets.Types) == 0)
		//		filter = filter.Where(def => !(def is TypeDef));
		//	if ((targetType & ProtectionTargets.Methods) == 0)
		//		filter = filter.Where(def => !(def is MethodDef));
		//	if ((targetType & ProtectionTargets.Fields) == 0)
		//		filter = filter.Where(def => !(def is FieldDef));
		//	if ((targetType & ProtectionTargets.Properties) == 0)
		//		filter = filter.Where(def => !(def is PropertyDef));
		//	if ((targetType & ProtectionTargets.Events) == 0)
		//		filter = filter.Where(def => !(def is EventDef));

		//	if (phase.ProcessAll)
		//		return filter.ToList();
		//	return filter.Where(def => {
		//		ProtectionSettings parameters = ProtectionParameters.GetParameters(context, def);
		//		Debug.Assert(parameters != null);
		//		if (parameters == null) {
		//			context.Logger.ErrorFormat("'{0}' not marked for obfuscation, possibly a bug.", def);
		//			throw new MdcryptException(null);
		//		}
		//		return parameters.ContainsKey(phase.Parent);
		//	}).ToList();
		//}
	}
}

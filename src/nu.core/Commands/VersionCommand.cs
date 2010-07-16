// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace nu.core.Commands
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using Magnum;
	using Magnum.Logging;

	public class VersionCommand :
		Command
	{
		readonly HashSet<Assembly> _alreadyOutput = new HashSet<Assembly>();
		readonly IEnumerable<Extension> _extensions;
		readonly ILogger _log = Logger.GetLogger<VersionCommand>();
		readonly bool _verbose;
		Assembly _main;

		public VersionCommand(bool verbose, IEnumerable<Extension> extensions)
		{
			_verbose = verbose;
			_extensions = extensions;
		}

		public void Execute()
		{
			_main = Assembly.GetEntryAssembly();

			OutputAssembly(_main);

			if (_verbose)
			{
				_extensions
					.Select(x => x.GetType().Assembly).
					Each(OutputAssembly);
			}
		}

		void OutputAssembly(Assembly assembly)
		{
			if (_alreadyOutput.Contains(assembly))
				return;

			_alreadyOutput.Add(assembly);

			AssemblyName name = assembly.GetName();

			string path = (assembly.Location != null) ? assembly.Location.Replace(Path.GetDirectoryName(_main.Location) + "\\", "") : "";

			_log.Info(x => x.Write("{0}, {1}, {2}", name.Name, path, name.Version));
		}
	}
}
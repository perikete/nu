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
	using Magnum.CommandLineParser;
	using Magnum.Monads.Parser;

	public class VersionCommandExtension :
		Extension
	{
		public void Initialize(ExtensionInitializer cli)
		{
			Parser<IEnumerable<ICommandLineElement>, ISwitchElement> switches =
				(from verbose in cli.Switch("verbose") select verbose);

			cli.Add(from arg in cli.Argument("version")
			        from verbose in switches.Optional("verbose", false)
			        select cli.GetCommand<VersionCommand>(new {verbose = verbose.Value}));
		}
	}
}
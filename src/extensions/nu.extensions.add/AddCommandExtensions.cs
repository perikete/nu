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
namespace nu.extensions.add
{
    using core;
    using core.Commands;
    using Magnum.Monads.Parser;

    public class AddCommandExtensions :
        Extension
    {
        public void Initialize(ExtensionInitializer cli)
        {
            cli.Add(from add in cli.Argument("add")
                    from nam in cli.Argument()
                    from v in cli.Definition("version")
                    select cli.GetCommand<AddPackageCommand>(new {name = nam.Id, version = v.Value}));

            cli.Add(from add in cli.Argument("add")
                    from nam in cli.Argument()
                    select cli.GetCommand<AddPackageCommand>(new {name = nam.Id, version = AddPackageCommand.MAX}));

            cli.Add(from nuggify in cli.Argument("nuggify")
                    from n in cli.Definition("name")
                    from v in cli.Definition("version")
                    select cli.GetCommand<NuggifyCommand>(new {version = v.Value, name = n.Value}));

        }
    }
}
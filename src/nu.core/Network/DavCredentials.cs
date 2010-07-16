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
namespace nu.core.Network
{
	using System;
	using System.Net;

	public class DavCredentials :
		RemoteCredentials
	{
		readonly string _password;
		readonly string _username;

		public DavCredentials(string username, string password)
		{
			_username = username;
			_password = password;
		}

		public ICredentials For(Uri location)
		{
			var networkCredential = new NetworkCredential(_username, _password);

			var credentials = new CredentialCache();
			credentials.Add(location, "Basic", networkCredential);

			return credentials;
		}
	}
}
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
namespace nu.core.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ICSharpCode.SharpZipLib.Zip;
    using Internal;

	public class ZippedDirectory :
        Directory
    {
        public ZippedDirectory(ZipDirectoryName name)
        {
            Name = name;
        }

    	public ZippedDirectory(DirectoryName name, RelativePathName relativeDirectoryName)
    	{
    		Name = name.Combine(relativeDirectoryName);
    		FileName = FileName.GetFileName(name.GetPath());
    	}

		protected FileName FileName { get; set; }

		public DirectoryName Name { get; set; }

        public string Path
        {
            get { return Name.ToString(); }
        }

        public Directory Parent
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasParentDir
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<File> GetFiles()
        {
			string innerPath = ZippedPath.GetPathInsideZip(Path);
			innerPath += "/";
			string zipFile = ZippedPath.GetZip(Path);
			var zf = new ZipFile(zipFile);
			bool result = false;

			return zf
				.Cast<ZipEntry>()
				.Where(entry => !entry.IsDirectory)
				.Where(entry => entry.Name.StartsWith(innerPath))
				.Select(entry => new ZippedFile(Name, new RelativePathName(entry.Name)))
				.Cast<File>();
		}

        public IEnumerable<Directory> GetDirectories()
        {
            throw new NotImplementedException(); 
        }

        public Directory GetChildDirectory(string name)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            var innerPath = ZippedPath.GetPathInsideZip(Path);
            innerPath += "/";
            var zipFile = ZippedPath.GetZip(Path);
            var zf = new ZipFile(zipFile);
            var result = false;

            foreach (ZipEntry entry in zf)
            {
                if(entry.Name.StartsWith(innerPath))
                {
                    result = true;
                    break;
                }
            }


            return result;
        }

        public File GetChildFile(string name)
        {
            return new ZippedFile(new ZipPathName(Path, name));
        }

        public bool IsRoot()
        {
            throw new NotImplementedException();
        }
    }
}
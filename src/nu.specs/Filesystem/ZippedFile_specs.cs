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
namespace nu.Specs.Filesystem
{
    using core.FileSystem;
    using core.FileSystem.Internal;
    using core.FileSystem.Zip;
    using NUnit.Framework;

    [TestFixture]
    public class Accessing_an_existing_zip_file
    {
        string _zippedFile = "nug.zip";

        ZipFileDirectory zf;

        [TestFixtureSetUp]
        public void SetUp()
        {
            zf = new ZipFileDirectory(new RelativePathName(_zippedFile));
        }

        [Test]
        public void GetChildDirectory()
        {
            var c = zf.GetChildDirectory("test");
            Assert.AreEqual("nug.zip\\test", c.Name.GetPath());
        }


        [Test]
        public void GetChildFileExists()
        {
            var c = zf.GetChildFile("MANIFEST.json");
            Assert.IsTrue(c.Exists());

            var c2 = zf.GetChildDirectory("lib").GetChildFile("yo.txt");
            Assert.IsTrue(c2.Exists());
        }

        [Test]
        public void ZippedDirectoryExists()
        {
            var c = zf.GetChildDirectory("lib");
            Assert.IsTrue(c.Exists());
            
        }

        [Test]
        public void GetChildFile()
        {
            var c = zf.GetChildFile("ho.txt");
            Assert.AreEqual("nug.zip\\ho.txt", c.Name.GetPath());
        }

        [Test]
        public void PathWithRelative()
        {
            Assert.AreEqual("nug.zip", zf.Name.GetPath());
        }

        [Test]
        public void ZipPathHelpers()
        {
            var result = ZippedPath.GetPathInsideZip("nug.zip\\test\\ho.txt");
            Assert.AreEqual("test/ho.txt", result);
        }

        [Test]
        public void MorePath()
        {
            var result = ZippedPath.GetZip("nug.zip\\test\\ho.txt");
            Assert.AreEqual("nug.zip", result);
        }

        [Test]
        public void ParentPath()
        {
            var result = ZippedPath.GetParentPath("nug.zip\\test\\ho.txt");
            Assert.AreEqual("test", result);


            var result2 = ZippedPath.GetParentPath("nug.zip\\test\\test2");
            Assert.AreEqual("test", result2);


            var result3 = ZippedPath.GetParentPath("nug.zip\\test");
            Assert.AreEqual("nug.zip", result3);


            var result4 = ZippedPath.GetParentPath("nug.zip\\ho.txt");
            Assert.AreEqual("nug.zip", result4);
        }
    }
}
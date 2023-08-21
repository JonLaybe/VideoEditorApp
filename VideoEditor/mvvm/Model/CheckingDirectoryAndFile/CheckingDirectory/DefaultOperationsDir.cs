using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.CheckingDirectoryAndFile.CheckingDirectory
{
	public class DefaultOperationsDir : DefaultOperations
	{
		public override bool IsExistsr(string dir)
		{
			DirectoryInfo directory = new DirectoryInfo(dir);

			return directory.Exists;
		}

		public override bool IsExistsAndCreate(string dir)
		{
			DirectoryInfo directory = new DirectoryInfo(dir);
			bool isExistsr = directory.Exists;

			if (!isExistsr)
				directory.Create();

			return isExistsr;
		}
	}
}

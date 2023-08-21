using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.CheckingDirectoryAndFile.CheckingFile
{
	public class DefaultOperationsFile : DefaultOperations
	{
		public override bool IsExistsAndCreate(string path)
		{
			bool isExistsr = IsExistsr(path);

			if (!isExistsr)
				File.Create(path).Close();

			return isExistsr;
		}

		public override bool IsExistsr(string path)
		{
			FileInfo fileInfo = new FileInfo(path);

			return fileInfo.Exists;
		}
	}
}

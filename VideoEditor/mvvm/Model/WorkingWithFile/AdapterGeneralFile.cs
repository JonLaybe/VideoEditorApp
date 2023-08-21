using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.WorkingWithFile;
using VideoEditor.mvvm.Model.WorkingWithFile.Interface;

namespace VideoEditor.mvvm.Model
{
	public class AdapterGeneralFile : GeneralFile // адаптирует FileInfo в GeneralFile
	{
		public AdapterGeneralFile(FileInfo file)
		{
			Path = file.FullName;
			Extension = System.IO.Path.GetExtension(file.FullName);
			NameFile = System.IO.Path.GetFileNameWithoutExtension(file.FullName);
			TimeCreate = file.CreationTime;
			TimeExport = DateTime.Now;
		}
	}
}

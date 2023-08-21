using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.WorkingWithFile.Interface
{
	public interface IFunctionSaveFile
	{
		string Save(string fromPath, string dir); // сохраняет файл в проект (fromPath - файл, dir - путь для сохранения)
	}
}

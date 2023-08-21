using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.WorkingWithFile.Interface
{
	public interface IFunctionImportFile
	{
		GeneralFile Import(string path); // импортирует файл в проект (path - путь к файлу)
	}
}

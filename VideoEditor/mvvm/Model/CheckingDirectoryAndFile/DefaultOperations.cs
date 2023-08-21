using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.CheckingDirectoryAndFile
{
	public abstract class DefaultOperations
	{
		public abstract bool IsExistsr(string name); // проверяет есть ли директория

		public abstract bool IsExistsAndCreate(string name); // проверяет есть ли дериктория. Если директории нет создает. Возвращает существовал ли файл на момент проверки.
	}
}

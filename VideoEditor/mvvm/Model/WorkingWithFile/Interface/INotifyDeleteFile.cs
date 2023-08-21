using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.WorkingWithFile.Interface
{
	public interface INotifyDeleteFile // Оповещает что file удален
	{
		void OnFileDelete(GeneralFile file); 
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model.WorkingWithFile;
using VideoEditor.mvvm.Model.WorkingWithFile.Interface;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase
{
	public class GeneralModelNotify<T> : GeneralModel<T>, INotifyDeleteFile where T : GeneralTable<T>, new()
	{
		public GeneralModelNotify(ConnactDb connact) : base(connact)
		{
		}

		public void OnFileDelete(GeneralFile file)
		{
			DeleteById(file.Id);
		}
	}
}

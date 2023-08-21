using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.ChangePage
{
	public interface IChangePage
	{
		void ChangePage(INotifyPropertyChanged page);
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.ChangePage.Interface
{
	public interface IPages
	{
		// Сводка:
		//	Возвращает INotifyPropertyChange и создание если не существует
		// Параметры:
		//	T - класс наследуемый от INotifyPropertyChanged
		// Возврат:
		//	Экземпляр класс наследуемый от INotifyPropertyChanged
		INotifyPropertyChanged GetAndPut<T>() where T : INotifyPropertyChanged, new();
		INotifyPropertyChanged GetAndPut(INotifyPropertyChanged model);
	}
}

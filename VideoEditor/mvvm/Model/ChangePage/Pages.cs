using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model.ChangePage.Interface;

namespace VideoEditor.mvvm.Model.ChangePage
{
	public class Pages : IPages // Работает с ViewModel
	{
		private List<INotifyPropertyChanged> _listViewModels;
		private Type _type;

		public Pages()
		{
			_listViewModels = new List<INotifyPropertyChanged>();
		}

		public INotifyPropertyChanged GetAndPut<T>() where T : INotifyPropertyChanged, new() // Возвращает INotifyPropertyChange и создание если не существует (T - класс наследуемый от INotifyPropertyChanged)
		{
			INotifyPropertyChanged notifyPropertyChanged = (from itemViewModel in _listViewModels where itemViewModel is T select itemViewModel).FirstOrDefault();

			if (notifyPropertyChanged == null) {
				notifyPropertyChanged = new T();
				_listViewModels.Add(notifyPropertyChanged);
			}

			return notifyPropertyChanged;
		}

		public INotifyPropertyChanged GetAndPut(INotifyPropertyChanged model)
		{
			INotifyPropertyChanged notifyPropertyChanged = (from itemViewModel in _listViewModels where itemViewModel == model select itemViewModel).FirstOrDefault();

			if (notifyPropertyChanged == null)
			{
				notifyPropertyChanged = model;
				_listViewModels.Add(notifyPropertyChanged);
			}

			return notifyPropertyChanged;
		}

		public INotifyPropertyChanged Pop()
		{
			if (_listViewModels.Count <= 0)
				throw new Exception("Pages->Pop error: collection is empty.");

			var viewModel = _listViewModels.LastOrDefault();

			if (viewModel == null)
				throw new Exception("Pages->Pop error: element not found.");

			_listViewModels.RemoveAt(_listViewModels.Count - 1);

			return viewModel;
		}
	}
}

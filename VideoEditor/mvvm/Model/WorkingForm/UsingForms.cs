using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoEditor.mvvm.Model.WorkingForm
{
	public static class UsingForms
	{
		private static List<Form> _listForms; // все окна

		static UsingForms()
		{
			_listForms = new List<Form>();
		}

		public static Form GetLastForm()
		{
			if (_listForms.Count > 0 )
				return _listForms.Last();
			throw new Exception("UsingForms GetLastFormError: count of windows is zero");
		}
		public static Form Push<T>(object parameter = null) where T : Window, new()
		{
			if (_listForms.Count() < 10)
			{
				Form newForm = new Form(parameter);
				_listForms.Add(newForm);
				newForm.SetWindow(new T());
				newForm.Window.Closing += Window_Closing;
				return newForm;
			}
			else
			{
				throw new Exception("UsingForms PushError: More than 10 additional windows open");
			}
		}

		public static void Remove(Window window)
		{
			Form result = null;

			foreach (var item in _listForms)
			{
				if (item.Window == window)
					result = item;
			}

			if (result != null)
				_listForms.Remove(result);
			else
				throw new Exception("UsingForms RemoveError: element not found");
		}

		private static void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Remove((Window)sender);
		}

		public static Form PushAndOpen<T>(object parameter = null) where T : Window, new()
		{
			Form newForm = Push<T>(parameter);
			newForm.Window.Show();

			return newForm;
		}
	}
}

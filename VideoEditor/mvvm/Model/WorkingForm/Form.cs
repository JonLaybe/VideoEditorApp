using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoEditor.mvvm.Model.WorkingForm
{
	public class Form
	{
		public Window Window { get; private set; }
		public object Parameter { get; private set; }
		public bool IsOpenForm { get; private set; }

		public Form(object parameter = null)
		{
			Parameter = parameter;
		}

		public void SetWindow(Window window)
		{
			if (this.Window == null)
			{
				this.Window = window;
			}
		}

		public void Close()
		{
			IsOpenForm = false;
			Window.Close();
		}
	}
}

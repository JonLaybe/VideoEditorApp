using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Settings.Interfaces
{
	public interface ISettings
	{
		string Name { get; set; } // имя настройки
		string Chapter { get; set; } // раздел настройки
	}
}

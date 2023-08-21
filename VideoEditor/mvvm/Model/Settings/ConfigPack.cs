using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Settings
{
	public abstract class ConfigPack
	{
		public string NameFile { get; private set; } = "ConfigSetting.json";
	}
}

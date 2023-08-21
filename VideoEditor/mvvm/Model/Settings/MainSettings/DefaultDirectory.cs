using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.CheckingDirectoryAndFile.CheckingDirectory;
using VideoEditor.mvvm.Model.Settings.Interfaces;

namespace VideoEditor.mvvm.Model.Settings.MainSettings
{
	public class DefaultDirectory : Setting
	{
		public List<string> Dirs { get; set; }

		public DefaultDirectory() : base("DefaultDirectory", "Directory")
		{
			Dirs = new List<string>();
		}

		public override void MakeyDefaultSetting()
		{
			Dirs = new List<string>()
			{
				"Video",
				"Clips",
			};
		}

		public override void ApplaySetting()
		{
			DefaultOperationsDir defaultOperationsDir = new DefaultOperationsDir();

			foreach(var item in Dirs)
				defaultOperationsDir.IsExistsAndCreate(item);
		}
	}
}

using System;
using System.Collections.Generic;
using LongoToDo.Base;
using Xamarin.Forms;

namespace LongoToDo.Features
{	
	public partial class NewItemView : NewItemViewXaml
    {	
		public NewItemView ()
		{
			InitializeComponent ();
		}
	}
    public partial class NewItemViewXaml : BaseContentPage<NewItemVM> { }
}


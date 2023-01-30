using DS.WpfApp.Licensing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DS.WpfApp.Licensing.Model
{
    internal class MyModel
    {

        public MyModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        private readonly MainWindowViewModel _mainWindowViewModel;


        #region Methods


        public void Task1()
        {
            _mainWindowViewModel.Text = "Process completed!";
        }

        #endregion

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.UI.Core;

namespace Bom.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditOrderDetailView.xaml
    /// </summary>
    public partial class EditOrderDetailView : UserControlViewBase
    {
        public EditOrderDetailView()
        {
            InitializeComponent();
        }

        private void ComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            var selectedPart = ((sender as ComboBox).SelectionBoxItem as Part);
            if (selectedPart == null) return;
            var viewModel = (EditOrderDetailViewModel)DataContext;
            if (viewModel == null) return;

            viewModel.OrderDetail.PartDescription = selectedPart.Description;
            //viewModel.Component.SubassemblyId = selectedPart.Id;
        }
    }
}

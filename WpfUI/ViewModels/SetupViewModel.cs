﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen
    {
        public void CancelEdits()
        {
            this.TryClose();
        }
    }
}
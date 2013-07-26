﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigNotes.ViewModels;
using GigNotes.Model;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace GigNotes
{
    public class Bootstrapper : PhoneBootstrapper
    {
        public PhoneContainer container { get; set; }

        protected override void Configure()
        {
            container = new PhoneContainer(RootFrame);

            container.RegisterPhoneServices();
            container.PerRequest<MainPageViewModel>(); 
            container.PerRequest<NewSetlistPageViewModel>();
            container.PerRequest<SetlistDataContext>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            // App Bar Conventions
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");

            // Toolkit DatePicker Convention
            ConventionManager.AddElementConvention<DatePicker>(
                DatePicker.ValueProperty, "Value", "ValueChanged");

            ConventionManager.AddElementConvention<LongListMultiSelector>(
                LongListMultiSelector.ItemsSourceProperty, "DataContext", "Loaded");
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
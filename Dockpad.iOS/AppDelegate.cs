﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dockpad.Services;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace Dockpad.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionAPIKey);

            global::Xamarin.Forms.Forms.Init();
            Syncfusion.SfCalendar.XForms.iOS.SfCalendarRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        static IAPIService<IPRMAPI> PrmAPI = new APIService<IPRMAPI>(Config.DomainURL);
        static IAPIManager ApiManager = new APIManager(PrmAPI);

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAPIManager>(ApiManager);
        }
    }
}

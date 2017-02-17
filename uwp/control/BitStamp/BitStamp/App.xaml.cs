﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BitStamp.View;
using BitStamp.ViewModel;

namespace BitStamp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            var globalization = ApplicationLanguages.ManifestLanguages;
            ApplicationLanguages.PrimaryLanguageOverride = "en";

            this.InitializeComponent();
            UnhandledException += App_UnhandledException;
            this.Suspending += OnSuspending;

            //int i = 3;
            //int k = (++i)+(++i)+(+i);
            //System.Console.WriteLine(k);
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //记录
            e.Handled = true;

        }

        private void ApplicationViewPreferred()
        {
            //窗口大小
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;


            ApplicationView.PreferredLaunchViewSize = new Size(600, 700);

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(600, 600));

            //标题
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var applicationView = ApplicationView.GetForCurrentView();
                applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
                var statusbar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                statusbar.BackgroundColor = Colors.Beige;
                statusbar.BackgroundOpacity = 0.2;
                statusbar.ForegroundColor=Colors.Black;

                



            }
            //if (ApiInformation::IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    //设置全屏模式 
            //    auto applicationView = ApplicationView::GetForCurrentView();
            //    applicationView->SetDesiredBoundsMode(ApplicationViewBoundsMode::UseCoreWindow);
            //    auto statusbar = ViewManagement::StatusBar::GetForCurrentView();
            //    statusbar->BackgroundColor = Colors::Red; //背景色
            //                                              //透明度0-1之间，0为全透明，	1为不透明  
            //                                              //全透明时候可能 前景色与默认色一致导致 信号等信息显示不出来，可改不透明或者改前景色
            //                                              //全屏模式下 与底色做透明运算。。可类似沉浸式状态栏
            //    statusbar->BackgroundOpacity = 0;
            //    statusbar->ForegroundColor = Colors::Blue; //信号 时间等绘制颜色
            //                                               //statusbar->ProgressIndicator->Text = "test statusbar";  //显示提示字和 。。。
            //                                               //statusbar->ProgressIndicator->ShowAsync();
            //}
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

          
            ApplicationViewPreferred();

            //if (rootFrame == null)
            //{
            //    SplashPage page=new SplashPage();
            //    Window.Current.Content = page;
            //    Window.Current.Activate();

            //}

            if (e.PrelaunchActivated == true)
            {
                return;
            }

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

                    //ApplicationView.PreferredLaunchViewSize = new Size(600, 700);
                    //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200,600));

                    //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

                    //当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(SplashPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            await AccoutGoverment.AccountModel.Storage();
            deferral.Complete();
        }
    }
}

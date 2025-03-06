using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfQLSV.Services;
using WpfQLSV.ViewModels;
using WpfQLSV.Views;

namespace WpfQLSV
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Khởi tạo MainWindow thông qua DI
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký dịch vụ
            services.AddSingleton<IFileDialogService, FileDialogService>();
            services.AddSingleton<IMessageService, MessageService>();

            // Đăng ký ViewModel
            services.AddTransient<StudentsViewModel>();
            services.AddTransient<SubjectsViewModel>();
            services.AddTransient<ClassesViewModel>();
            services.AddTransient<ScoresViewModel>();


            // Đăng ký View
            services.AddTransient<MainWindow>();
            services.AddTransient<StudentsView>();
            services.AddTransient<SubjectsView>();
            services.AddTransient<ClassesView>();
            services.AddTransient<ScoresView>();
        }
    }
}
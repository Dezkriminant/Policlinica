using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Policlinica.DB;
using Policlinica.Views;

namespace Policlinica.ViewModels;

public partial class PasswordWindowViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceProvider _provider;
    [ObservableProperty] string username;
    [ObservableProperty] string password;
    [ObservableProperty] List<User> _usersList;
    [ObservableProperty] UserRepository _repository;

    public PasswordWindowViewModel(IServiceProvider provider, UserRepository repository )
    {
        _provider = provider;
        _usersList = repository.GetUsersByTest();
       // _repository = repository;
    }

    [RelayCommand]
    public void StartTest()
    {
        
        var vm = ActivatorUtilities.CreateInstance<AdminWindowViewModel>(
            _provider,
            Username);
        var win = _provider.GetRequiredService<AdminWindow>();
        //vm.SetClose(win.Close);
        win.DataContext = vm;
        win.Show();
        // close();

    }
    [RelayCommand]
    public void SaveDB()
    {
        User user = new User
        {
            Name = Username,
            Password = Password
        };
       // if(Users user )
        _repository.CheckLoginAndPassword(name,password);
        var vm = _serviceProvider.GetRequiredService<AdminWindowViewModel>();
        var win = _serviceProvider.GetRequiredService<AdminWindow>();
        
        //vm.SetClose(win.Close);
        win.DataContext = vm;
        win.Show();
        //close();
    }
}
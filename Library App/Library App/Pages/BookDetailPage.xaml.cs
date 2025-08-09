using CommunityToolkit.Mvvm.ComponentModel;
using Library_App.Models.ViewModel;
using Library_App.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Library_App.Pages;

public partial class BookDetailPage : ContentPage
{
    public BookDetailPage(Book book)
	{
        
		InitializeComponent();
        BindingContext = book;
    }
}
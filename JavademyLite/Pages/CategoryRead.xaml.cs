using JavademyLite.Models;
using System.Collections.Generic;

namespace JavademyLite.Pages;

public partial class CategoryRead : ContentPage
{
	public CategoryRead()
	{
		InitializeComponent();
	}

    public void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Category> people = App.CategoryRepo.GetAllCategories();
        categoriesList.ItemsSource = people;
    }

    public void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        App.CategoryRepo.AddNewCategory(name.Text, description.Text);
        statusMessage.Text = App.CategoryRepo.StatusMessage;
    }
}
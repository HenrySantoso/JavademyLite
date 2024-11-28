using JavademyLite.Models;
using System.Collections.Generic;

namespace JavademyLite.Pages;

public partial class CategoryRead : ContentPage
{
    public CategoryRead()
    {
        InitializeComponent();
    }

    // Fetch all categories
    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Category> categories = await App.CategoryRepo.GetAllCategories();
        categoriesList.ItemsSource = categories;
    }

    // Add a new category
    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        await App.CategoryRepo.AddNewCategory(name.Text, description.Text);
        statusMessage.Text = App.CategoryRepo.StatusMessage;
        OnGetButtonClicked(sender, args); // Refresh categories list
    }

    // Edit a category
    public async void OnEditButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.CommandParameter is int categoryId)
        {
            try
            {
                // Retrieve the category by ID
                var categories = await App.CategoryRepo.GetAllCategories();
                var category = categories.Find(c => c.CategoryId == categoryId);

                if (category == null)
                {
                    await DisplayAlert("Error", "Category not found", "OK");
                    return;
                }

                // Prompt user for new name and description
                string newName = await DisplayPromptAsync("Edit Category", "Enter new name:", initialValue: category.Name);
                string newDescription = await DisplayPromptAsync("Edit Category", "Enter new description:", initialValue: category.Description);

                if (!string.IsNullOrWhiteSpace(newName) && !string.IsNullOrWhiteSpace(newDescription))
                {
                    await App.CategoryRepo.UpdateCategory(categoryId, newName, newDescription);
                    statusMessage.Text = App.CategoryRepo.StatusMessage;
                    OnGetButtonClicked(sender, e); // Refresh categories list
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to edit category. {ex.Message}", "OK");
            }
        }
    }

    // Delete a category
    public async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.CommandParameter is int categoryId)
        {
            bool confirm = await DisplayAlert("Delete Category", "Are you sure you want to delete this category?", "Yes", "No");

            if (confirm)
            {
                try
                {
                    await App.CategoryRepo.DeleteCategory(categoryId);
                    statusMessage.Text = App.CategoryRepo.StatusMessage;
                    OnGetButtonClicked(sender, e); // Refresh categories list
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete category. {ex.Message}", "OK");
                }
            }
        }
    }
}

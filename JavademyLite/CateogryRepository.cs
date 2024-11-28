using SQLite;
using JavademyLite.Models;

namespace JavademyLite;

public class CategoryRepository
{
    private SQLiteAsyncConnection conn;
    string _dbPath;

    public string StatusMessage { get; set; }

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);

        await conn.CreateTableAsync<Category>();
    }

    public CategoryRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    // Add a new category
    public async Task AddNewCategory(string name, string desc)
    {
        int result = 0;
        try
        {
            await Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            if (string.IsNullOrEmpty(desc))
                throw new Exception("Valid description required");

            result = await conn.InsertAsync(new Category { Name = name, Description = desc });

            StatusMessage = string.Format("{0} record(s) added (Name: {1}, Description: {2})", result, name, desc);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }
    }

    // Retrieve all categories
    public async Task<List<Category>> GetAllCategories()
    {
        try
        {
            await Init();
            return await conn.Table<Category>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Category>();
    }

    // Update an existing category
    public async Task UpdateCategory(int id, string newName, string newDesc)
    {
        try
        {
            await Init();

            var categoryToUpdate = await conn.FindAsync<Category>(id);

            if (categoryToUpdate == null)
                throw new Exception($"Category with ID {id} not found.");

            categoryToUpdate.Name = newName;
            categoryToUpdate.Description = newDesc;

            int result = await conn.UpdateAsync(categoryToUpdate);

            StatusMessage = string.Format("{0} record(s) updated (ID: {1}, Name: {2}, Description: {3})", result, id, newName, newDesc);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to update category with ID {0}. Error: {1}", id, ex.Message);
        }
    }

    // Delete a category
    public async Task DeleteCategory(int id)
    {
        try
        {
            await Init();

            var categoryToDelete = await conn.FindAsync<Category>(id);

            if (categoryToDelete == null)
                throw new Exception($"Category with ID {id} not found.");

            int result = await conn.DeleteAsync(categoryToDelete);

            StatusMessage = string.Format("{0} record(s) deleted (ID: {1})", result, id);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to delete category with ID {0}. Error: {1}", id, ex.Message);
        }
    }
}

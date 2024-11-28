using SQLite;
using JavademyLite.Models;

namespace JavademyLite;

public class CategoryRepository
{
    private SQLiteConnection conn;
    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Category>();
    }

    public CategoryRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewCategory(string name, string desc)
    {
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");
            if (string.IsNullOrEmpty(desc))
                throw new Exception("Valid description required");

            // TODO: Insert the new person into the database
            result = conn.Insert(new Category { Name = name , Description = desc});

            StatusMessage = string.Format("{0} record(s) added (Name: {1}, Description: {2})", result, name, desc);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Category> GetAllCategories()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return conn.Table<Category>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Category>();
    }


}

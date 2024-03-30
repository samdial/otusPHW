using DelegatesEvents;

internal class FileFinder
{
    public event EventHandler<FileEventArgs>? FileFound;
    private bool _isSearchCancelled = false;

    protected virtual void OnFileFound(FileEventArgs e)
    {
        if (!_isSearchCancelled)
            FileFound?.Invoke(this, e);
    }

    public void CancelSearch() => _isSearchCancelled = true;

    public void StartSearch(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new ArgumentException("Invalid folder path.");

        try
        {
            string[] fileEntries = Directory.GetFiles(folderPath);
            foreach (string fileName in fileEntries)
            {
                if (_isSearchCancelled)
                    break;
                OnFileFound(new FileEventArgs(fileName));
            }
            Console.WriteLine("Search completed.");
        }
        catch (Exception exc)
        {
            Console.WriteLine("File search failed: " + exc.Message);
        }
    }
}
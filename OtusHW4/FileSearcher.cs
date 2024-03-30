
public class FileSearcher
{
    public event EventHandler<FileEventArgs> FileFound;
    public event EventHandler SearchCancelled;

    public void SearchFiles(string directoryPath)
    {
        try
        {
            SearchFilesRecursive(directoryPath);
        }
        catch (OperationCanceledException)
        {
            SearchCancelled?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SearchFilesRecursive(string directoryPath)
    {
        foreach (string filePath in Directory.GetFiles(directoryPath))
        {
            var fileInfo = new FileInfo(filePath);
            OnFileFound(fileInfo.Name);
        }

        foreach (string subDirectoryPath in Directory.GetDirectories(directoryPath))
        {
            SearchFilesRecursive(subDirectoryPath);
        }
    }

    protected virtual void OnFileFound(string fileName)
    {
        FileFound?.Invoke(this, new FileEventArgs(fileName));
    }

    public void CancelSearch()
    {
        throw new OperationCanceledException("Search operation cancelled.");
    }
}

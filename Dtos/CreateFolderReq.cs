namespace WeSyncBackend.Dtos
{
    public class CreateFolderReq
    {
        public string folderName { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Virtualpath { get; set; } = string.Empty;
    }
}

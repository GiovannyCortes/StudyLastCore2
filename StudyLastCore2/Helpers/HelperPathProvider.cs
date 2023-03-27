namespace StudyLastCore2.Helpers {

    public enum Folders { Images = 0, Documents = 1 }

    public class HelperPathProvider {

        private IWebHostEnvironment hostEnvironment;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment) {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder) {
            string carpeta = "";
            if (folder == Folders.Images) {
                carpeta = "images";
            } else if (folder == Folders.Documents) {
                carpeta = "documents";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

    }
}

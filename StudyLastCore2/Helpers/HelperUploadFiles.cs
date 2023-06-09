﻿using StudyLastCore2.Helpers;

namespace StudyLastCore2.Helpers  {

    public class HelperUploadFiles {

        private HelperPathProvider helperPath;

        public HelperUploadFiles(HelperPathProvider pathProvider) {
            this.helperPath = pathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile file, Folders folder, string filename) {
            string path = this.helperPath.MapPath(filename, folder);
            using (Stream stream = new FileStream(path, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files, Folders folder) {
            List<string> paths = new List<string>();
            foreach (IFormFile file in files) {
                string fileName = file.FileName;
                string path = this.helperPath.MapPath(fileName, folder);
                using (Stream stream = new FileStream(path, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                }
                paths.Add(path);
            }
            return paths;
        }
    }
}

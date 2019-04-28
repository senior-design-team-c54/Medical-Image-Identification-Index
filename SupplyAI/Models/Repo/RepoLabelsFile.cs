using MI3.Models.FormData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models.Repo
{
    public class RepoLabelsFile : RepoFile
    {

        public RepoLabelsFile(UploadFile file) {
            this.name = file.name;
            fileType = FileType.Labels;

        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mushrooms
{
    public interface IFileService
    {
        string SavePicture(string name, Stream data, string location = "temp");

    }
}

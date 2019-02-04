using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public interface ICrytpoService
    {
        string GetHash(string toHash);

        string GenerateSalt(int keyLength);
    }
}

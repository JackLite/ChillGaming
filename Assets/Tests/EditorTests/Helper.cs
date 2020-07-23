using System.IO;
using UnityEngine;

namespace Tests.EditorTests
{
    static class Helper 
    {
        public static string ReadTestData()
        {
            return File.ReadAllText("Assets/Tests/testData.txt");
        }
    }
}

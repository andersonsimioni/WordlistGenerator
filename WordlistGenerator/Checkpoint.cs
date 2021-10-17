using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WordlistGenerator
{
    public class Checkpoint
    {
        private readonly string Path;
        private Engine Data;

        public Engine GetData() 
        {
            return Data;
        }

        /// <summary>
        /// Convert save data to json and save it on path file
        /// </summary>
        public void CreatePoint() 
        {
            if (this.Data == null)
                throw new Exception("Save data is null");

            var json = JsonConvert.SerializeObject(this.Data);

            File.WriteAllText(Path, json);
        }

        /// <summary>
        /// Load saved data and convert to object again
        /// </summary>
        public void RestorePoint() 
        {
            if (File.Exists(Path) == false)
                throw new Exception("Path not found");

            var json = File.ReadAllText(this.Path);
            var obj = JsonConvert.DeserializeObject<Engine>(json);

            this.Data = obj;
        }

        public Checkpoint(string path) 
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("Path is null");

            this.Path = path;
        }

        public Checkpoint(Engine engineRuntimeStatus, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("Path is null");
            if (engineRuntimeStatus == null)
                throw new Exception("Save data is null");

            this.Path = path;
            this.Data = engineRuntimeStatus;
        }
    }
}

using System;
using UnityEngine;
using System.IO;
using Padrao.Core.Singleton;

namespace Save
{
    [DefaultExecutionOrder(-1)]
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField]
        private SaveSetup _saveSetup;
        private string _path;
        private string _fileName = "save.txt";

        protected override void Awake()
        {
            base.Awake();
            _path = Application.streamingAssetsPath;  
            Load();       
            DontDestroyOnLoad(gameObject);
        }

        private void Save()
        {
            string jsonSetup = JsonUtility.ToJson(_saveSetup);
            SaveFile(jsonSetup);
        }

        private void SaveFile(string json)
        {
            if(!Directory.Exists(_path)){
                Directory.CreateDirectory(_path);
            }
            File.WriteAllText(_path+"/"+_fileName, json);
        }

        public void DeleteSave()
        {
            _saveSetup = new SaveSetup();
            Save();
        }
        public void Load()
        {
            string fileLoaded = "";

            if(File.Exists(_path+"/"+_fileName))
            {
                fileLoaded = File.ReadAllText(_path+"/"+_fileName);
                _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);         
            }
            else
            {
                _saveSetup = new SaveSetup();
                Save();
            }
        }

        public void NewHighscore(int score)
        {
            _saveSetup = new SaveSetup{
                score = score,
                date = DateTime.Now.Ticks
            };
            Save();
        }

        public int GetHighscore()
        {
            return _saveSetup.score;
        }
    }

    [System.Serializable]
    public class SaveSetup
    {
        public int score;
        public long date;
        //date.Ticks

        public DateTime GetDateTime()
        {
            return new DateTime(date);
        }

        public SaveSetup()
        {
            score = 0;
            date = DateTime.Now.Ticks;
        }
    }
}
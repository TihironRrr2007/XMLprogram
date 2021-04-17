using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace XMLTest
{
    class FileConfig
    {
        // компонент сериализации XML
        private readonly XmlSerializer _fileConfigXmlSerializer = new XmlSerializer(typeof(Configs));
        // Объект для конфига
        public Configs Configs = new Configs();
        //Путь для конфига
        private readonly string _filePath;

        //обращение при создании объекта когда нужно только путь для него
        public FileConfig(string filePath)
        {
            _filePath = filePath;
        }
        //обращение при создании объект когда нужно создать новый объект
        public FileConfig(string Name, Configs conf)
        {
            Configs = conf;
            _filePath = "C:\\Test\\" + Name + ".xml";
        }


        // когда хотим открыть объект
        public void Open()
        {
            if (string.IsNullOrEmpty(_filePath)) // проверка на отсутствие значения
            {
                MessageBox.Show("File path is empty or null", "Empty file name", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                //создаем поток обращения к файлу по пути _filePath
                using FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);
                // открываем файл и сохраняем в переменную Configs
                Configs = (Configs)_fileConfigXmlSerializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{_filePath} file open error\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        //когда хотим сохранить объект
        public void Save()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("File path is empty or null", "help", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                //поток обращения к файлу по пути _filePath
                using FileStream fileStream = new FileStream(_filePath, FileMode.Create);
                //сохраняем файл создавая его из переменной Configs
                _fileConfigXmlSerializer.Serialize(fileStream, Configs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file name is empty", "Empty file name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }

    [Serializable]
    public class Configs
    {
        public string AppName { get; set; }
        public string Color { get; set; }
        public int Port { get; set; }
        public string[] Logins { get; set; }
    }
}

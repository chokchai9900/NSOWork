using MongoDB.Driver;
using Newtonsoft.Json;
using NSOWork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;

namespace NSOWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //string json = File.ReadAllText(@"D:\API\Pref\zipdata.json");
            var result = new List<ResultModel>();
            var listname = new List<string>();
            using (var reader = new StreamReader(@"zipdata.json"))
            {
                var data = reader.ReadToEnd();
                //foreach (var item in data)
                //{
                //    result.Add(new ResultModel
                //    {
                //        ContainerName = item.
                //    });
                //}
                result = JsonConvert.DeserializeObject<List<ResultModel>>(data);
            }
            var listGroup = result
                .GroupBy(it => it.Zippath)
                .Select(it =>
                {
                    return new
                    {
                        zipName = it.Key,
                        isContainer = it.Any(i => i.ContainerName.Count() != 0)
                    };
                }
                )
                .Where(it => it.isContainer == true)
                .Select(it => it.zipName)
                .ToList();


            foreach (var item in listGroup)
            {
                string[] str = item.Split("\\");
                Program.CopyFile(item, str[str.Length - 1]);
           }



            //var client = new MongoClient("mongodb://dbagent:Nso4Passw0rd5@mongodbproykgte5e7lvm7y-vm0.southeastasia.cloudapp.azure.com/nso");
            //var database = client.GetDatabase("nso");
            //var collection = database.GetCollection<ListContainerNameModel>("ListContainerName");

            //string path = $@"D:\API\SurveyList\Nsodata.txt";
            //Console.WriteLine("Reading file in zip......");

            //int count = 0;
            //var result = new List<ResultModel>();
            //List<string> filePaths = Directory.GetFiles(@"E:\", "*.zip").ToList();
            //var GetContainerName = collection.AsQueryable().Select(it => it._id).ToList();

            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.Write("[");
            //    foreach (var zipPath in filePaths)
            //    {
            //        count++;
            //        Console.WriteLine($"Zip {count}: {zipPath}");
            //        using (ZipArchive zipFile = ZipFile.OpenRead(zipPath))
            //        {
            //            var data = zipFile.Entries.Where(it => it.FullName.EndsWith('/')).Select(it => it.FullName.Trim('/')).ToList();
            //            Console.WriteLine($"Data coun : {data.Count}");
            //            Console.WriteLine("Process.........");

            //            var ctnLst = data.Intersect(GetContainerName).ToList();

            //            result.Add(new ResultModel
            //            {
            //                Zippath = zipPath,
            //                ContainerName = ctnLst
            //            });
            //            foreach (var item in result)
            //            {
            //                foreach (var clist in item.ContainerName)
            //                {
            //                    Console.WriteLine($"{clist} has in the {zipPath}");
            //                }
            //            }
            //            var json = JsonSerializer.Serialize(result);
            //            writer.Write(json);
            //            writer.WriteLine(",");
            //            Console.WriteLine($"{zipPath} is success ......");
            //            //foreach (var item in GetContainerName)
            //            //{
            //            //    var valid = data.Find(it => it.FullName.Contains(item._id));
            //            //    if (valid != null)
            //            //    {
            //            //        Console.WriteLine($"{item._id} has in the {zipPath}");
            //            //        result.Add(new ResultModel
            //            //        {
            //            //            ContainerName = item._id,
            //            //            Zippath = zipPath
            //            //        });
            //            //    }
            //            //    else
            //            //    {
            //            //        Console.WriteLine($"{item._id} not in the {zipPath}");
            //            //    }
            //            //}
            //        }
            //    }
            //    writer.Write("[");
            //}
        }


        public static void CopyFile(string filePath, string fileName)
        {
            string[] paths = { @"E:\ContranZip", fileName };
            string sourceFile = @$"{filePath}";
            string targetPath = Path.Combine(paths);
            try
            {
                File.Copy(sourceFile, targetPath, true);
                Console.WriteLine($"{fileName} it copy !!");
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
                Console.WriteLine($"{fileName} copy error !!");
            }
            
        }



    }
}

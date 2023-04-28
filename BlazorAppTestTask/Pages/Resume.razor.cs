using BlazorAppTestTask.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorAppTestTask.Pages
{
    public class ResumeViewModel : ComponentBase
    {
        public bool Flag { get; set; }

        public DataPage datapage = new DataPage();
        protected override void OnInitialized()
        {
            ChangeFlag();
        }  

        public void ChangeFlag()
        {
            if (!Flag)
            {
                Flag = true;
            }
            else
            {
                Flag = false;
            }

            using (FileStream fs = new FileStream("data.json", FileMode.Open))
            {
                datapage = JsonSerializer.Deserialize<DataPage>(fs);
                Console.WriteLine($"Name: {datapage?.Name}  Age: {datapage?.Gender}");
                StateHasChanged();

            }
        }

        public void SaveData()
        {
            if (!Flag)
            {
                Flag = true;
            }
            else
            {
                Flag = false;
            }

            using (FileStream fs = new FileStream("data.json", FileMode.Open))
            {
                string json;
                json = JsonSerializer.Serialize(datapage);
                Console.WriteLine("Data has been saved to file");
                byte[] buffer = Encoding.Default.GetBytes(json);
                fs.WriteAsync(buffer, 0, buffer.Length);
                StateHasChanged();
            }
        }

        protected void CancelEdit()
        {
            if (!Flag)
            {
                Flag = true;
            }
            else
            {
                Flag = false;
            }
        }

    }
}

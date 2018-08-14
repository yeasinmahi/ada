using System;
using System.Windows.Forms;
using Raven.Client.Document;
using Serilog;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            DocumentStore documentStore = new DocumentStore
            {
                Url = "https://localhost:8080",
                DefaultDatabase = "logs"
            };

            documentStore.Initialize();
            InitializeComponent();
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File("a.txt")
                .WriteTo.RollingFile("aa.txt")
                .WriteTo.RavenDB(documentStore) 
                .CreateLogger();

            Log.Logger = logger;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Bag"
            };

            Log.Information("Add Product {@Product}",product);
        }
    }

    class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}

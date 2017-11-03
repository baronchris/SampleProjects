using FlooringMastery;
using FlooringMastery.BLL;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorData;


namespace FlooringTest
{
    [TestFixture]
    public class FileIOTest
    {
        const string directory = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\TestData\";
        const string productPath = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\TestData\Products.txt";
        const string statePath = @"C:\repos\chris-williams-individual-work\FlooringMasteryV5\Data\TestData\Taxes.txt";

        [Test]

        public void Loadfiletest()
        {
            List<string> Lines = FileIO.GetFileData(@"Orders_06012013.txt");
            Assert.AreEqual(1, Lines.Count);
        }

        [Test]
        public void CanParseOrders()
        {
            LoadOrders O = new LoadOrders();
            List<Order> Orders = O.ParseFileDataFromPath(directory+@"Orders_06012013.txt");
            Assert.AreEqual(1, Orders.Count);
            Assert.AreEqual(1, Orders[0].OrderNumber);
        }

        [Test]
        public void CanParseProducts()
        {
            List<Product> Products = LoadProducts.LoadAllProducts();
           
            Assert.AreEqual(Products[0].ProductName, "Marble Slab");
        }

        [Test]
        public void CanParseStates()
        {
            List<State> States = LoadStates.LoadAllStates();
            
            Assert.AreEqual(States[0].StateCode,"DS");
        }
    }
}

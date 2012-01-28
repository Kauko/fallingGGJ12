
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Falling
{

    /* Levelin ottaminen tiedostosta
     */
    class LevelLoader
    {
        Grid leveli = new Grid(32,32,3);    // test
        int testidata = 0;
        StorageDevice device = null;
        IAsyncResult result = null;

        // Avaa leveli nimen mukaan
        public void loadLevel(string nimi)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                DoLoadGame( device );
            }
        }
        
        public void DoLoadGame(StorageDevice device)
        {
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "testlevel.txt";

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }

            // Open the file.
            Stream stream = container.OpenFile(filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(testidata));
            testidata = (int)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            // Report the data to the console.
            Debug.WriteLine("Leveldata: " + testidata);
        }


        /// <summary>
        /// This method illustrates how to open a file. It presumes
        /// that demobinary.sav has been created.
        /// </summary>
        /// <param name="device"></param>
        private static void DoOpen(StorageDevice device)
        {
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Add the container path to our file name.
            string filename = "testileveli.txt";

            Stream file = container.OpenFile(filename, FileMode.Open);
            file.Close();

            // Dispose the container.
            container.Dispose();

            Debug.Write("Leveldata: " + 

        }



}
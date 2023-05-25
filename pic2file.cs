// pic2file.cs - Automatically save bitmaps from the clipboard
// to the filesystem.
//
// License: MIT / X11
// Copyright (c) 2009, 2023 by James K. Lawless
// See full license at:
//    https://github.com/jimlawless/playmp3/blob/main/LICENSE
// 
// jimbo@radiks.net 
// https://jiml.us
//
// 

using System ;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace Pic2File
{
    public class Pic2File
    {
        [STAThread]
	    public static void Main(string[] args)
        {    
            ImageFormat fmt;
            string dir,prefix,outputFile,suffix;
            int i;
            int count;
            fmt=ImageFormat.Jpeg;
            suffix=".jpg";
            dir=".\\";
            prefix="p2f_";
            bool beep=false;
            count=0;
            Console.WriteLine(
                "\nPic2File by Jim Lawless - jimbo@radiks.net");
            Console.WriteLine(
                "Github source: https://github.com/jimlawless/pic2file");
            Console.WriteLine(
                "Blog post: https://jiml.us/pic2file\n");
            Syntax();        
         
            for(i=0;i<args.Length;i++) 
            {
             
                if(args[i].ToLower().Equals("-h"))
                {
                        // Syntax has already been displayed.
                        // Just return.
                    return;
                }
                    // Play a sound when img files are saved
                if(args[i].ToLower().Equals("-beep")) 
                {
                    beep=true;
                }
                else
                if(args[i].ToLower().Equals("-dir")) 
                {
                    dir=args[i+1];
                        // make sure we have an
                        // ending slash
                   if( ! dir.EndsWith("\\"))
                      dir+="\\";
                   i++;
                }
                else
                if(args[i].ToLower().Equals("-prefix")) 
                {
                   prefix=args[i+1];
                   i++;
                }
                else
                if(args[i].ToLower().Equals("-gif"))
                {
                   fmt=ImageFormat.Gif;
                   suffix=".gif";
                }
                else
                if(args[i].ToLower().Equals("-jpg"))
                {
                    fmt=ImageFormat.Jpeg;
                   suffix=".jpg";
                }
                else
                if(args[i].ToLower().Equals("-png"))
                {
                    fmt=ImageFormat.Png;
                    suffix=".png";
                }
                else
                if(args[i].ToLower().Equals("-bmp"))
                {
                   fmt=ImageFormat.Bmp;
                   suffix=".bmp";
                }
                else
                if(args[i].ToLower().Equals("-tiff"))
                {
                    fmt=ImageFormat.Tiff;
                    suffix=".tif";
                }                           
                else 
                {
                    Console.WriteLine("Unknown option " + args[i]);
                    return;
                }
            }

                    // Write the selected options to the console
            Console.WriteLine("\nWork directory: " + dir);
            Console.WriteLine("Filename prefix: " + prefix);
            Console.WriteLine("Image format: " + suffix);
            Console.WriteLine("Play beep on save: " + beep );
         
                // Display error if work directory does not
                // exist
            if( ! Directory.Exists(dir)) 
            {
                Console.WriteLine("Work directory " + dir + " does not exist. Exiting.");
                return;
            }
         
                // Now, loop waiting for data to appear on the Clipboard
            Console.WriteLine("\nWaiting...\n");
            IDataObject cdata;
         
            for(;;) 
            {
                Thread.Sleep(100);
                Application.DoEvents();
         
                cdata=Clipboard.GetDataObject();
                if(cdata==null)
                    continue;

                if(cdata.GetDataPresent(DataFormats.Bitmap))
                {
                        // Loop until we have a unique filename
                    for(;;) 
                    {
                        outputFile=dir+prefix+count+suffix;
                        if( ! File.Exists(outputFile))
                            break;
                        count++;
                    }
                    Console.Write("Saving " + outputFile + " ... " );
                    Image im=(Image)cdata.GetData(DataFormats.Bitmap,true);                              
                    im.Save(outputFile,fmt);
                    Console.WriteLine("done.");
                    if(beep)
                        System.Media.SystemSounds.Beep.Play();
                    count++;
                    Clipboard.Clear();
                }
            }
        }          
        public static void Syntax() 
        {
            Console.WriteLine("Syntax:\tPic2File.exe -dir dir_to_store_pics -prefix filename_prefix -beep [ -gif -png -tiff -jpg -bmp ]\n");
            Console.WriteLine("The default output format is -jpg.\n");
        }
    }
}

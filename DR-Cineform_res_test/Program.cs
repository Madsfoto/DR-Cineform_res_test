using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DR_Cineform_res_test
{
    internal class Program
    {
        static string Createbatline(string inputFile, string x, string y)
        {
            // ffmpeg -i FILE -vf scale=1808:1080 -sws_flags lanczos -c:v cfhd -quality 0 "W:\W-Conv\%%~na.mov"

            // seperate strings to make it simpler to add or remove sections in the final string
            string ffInput = "ffmpeg -i " + inputFile + " -vf scale="+x+":"+y+ " -sws_flags lanczos -c:v cfhd -quality 0";
            
            
            string input_WO_Ext = inputFile.Remove((inputFile.Length) - 4); // We don't want a.mov_ etc, but a

            string outputFilename = input_WO_Ext + "_" +x +"_" + y+".mov";


            string FFCmd = ffInput + " "+outputFilename;
            return FFCmd;

        }

        static void Main(string[] args)
        {
            // Davinci resolve does not support all the resolutions ffmpeg can create in cineform. 
            // This program creates a bat file which converts a single video into all possible ffmpeg resolutions,
            // to test which ones are valid. 
            List<string> batContent = new List<string>();
            string input = "cc.mov";

            // main loop
            for (int x = 1; x <= 2048; x++)
            {
                for (int y = 1; y <= 2048; y++)
                {
                    if(x%16==0) // ffmpeg only supports cineform resolutions with a width of n*16
                    {
                        string batStrTest = Createbatline(input, x.ToString(), y.ToString());
                        batContent.Add(batStrTest);
                    }
                    
                }

            }

            File.WriteAllLines("0.bat", batContent);


        }
    }
}

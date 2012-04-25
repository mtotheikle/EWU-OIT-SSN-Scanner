/* Exclusion Path Testing by Aleksandr Melnikov
 * Last Updated: 04/24/2012
 * 
 * The purpose of this project is to develop, test, and implement the capability to exclude paths from the EWU Scanner's scans.
 * 
 * There are numerous comments in the code that, hopefully, explain the logic and reasoning behind why the code was written
 * and chosen the way it was.
 * 
 * <Usage>
 * I've written some general ways to test how String.StartsWith(pattern) works. I would suggest to write your own test-cases and run them through
 * the debugger.
 * 
 * <Summary>
 * Using the String.StartsWith(pattern), the capability to exclude certain paths was coded into the Scanner.
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace ExclusionPathTesting
{
    class Program
    {
        //======Exclusion Paths================================================================================================================================
        private static List<string> exclusionPaths = new List<string>();
        //This list of strings will hold the exclusion paths, that is, the paths that the scanner should not scan. A list is used to allow a dynamic number
        //of paths to be added. Performance and memory constraints may cause something other than a list to be used later on.
        //This list of strings will be set via GUI, when the MainForm calls RunScan, MainForm will also pass in the list of excluded paths.
        //The paths will be set by a user.
        //This global scope should ensure that these exclusion paths can be used wherever necessary in this code.

        //For testing, i'll manually code in paths to exclude.

        //=====================================================================================================================================================
        //======Special Folder Paths===========================================================================================================================
        private static string windowsFolderPath = (Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System))).FullName;
        private static string programFilesFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
        private static string usersDirectory = (Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))).FullName;
        //private static string usersDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\Testbed";
        private static string logFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\EWUScanner_Output.txt";
        //=====================================================================================================================================================
        static void Main(string[] args)
        {
            //testing exclusion paths
            //exclusionPaths.Add(@"C:\");//the @ will have the string be interpreted literally, so no need for escape sequences.
            //exclusionPaths.Add(@"D:\");
            //exclusionPaths.Add(@"E:\");

            //first exclusion clean up: Check for any drives to be excluded? this means checking root paths like C:\ or D:\
            //-possible complication, are there more complicated drives? Something other than C:\ or D:\? Like a network drive?
            //More testing will probably reveal the issue.


            //List of all the drives on the system.
            //DriveInfo[] listOfDrives = DriveInfo.GetDrives();

            //to check the list of drives and possibly remove drives from being scanned...I need to figure out a way to remove the drives from the
            //DriveInfo array, but the DriveInfo array also holds a bunch more information that the code uses later on.
            //I am trying to set them to null right now, but that might lead to code modifications when I try to add this feature to the main program.
            //I wonder how i'll do this...the null exception stuff might be easier to do, with some careful coding.

            //If I remove the list of drives early on, the scanner is saved a ton of work because it won't have to SCAN that entire drive..
            //but also, I could have tried to delete all the paths another way..as soon as the root is matched to any subdirectory path, but then I'd be deleting
            //a lot of paths...and the rest of the code would think the drive is being scanned and so a bunch of other stuff would be affected. This early on
            //deletion seems to be easier.

            //attempt to remove drives via pure string comparisons, level of hard-work/coding-inefficiency...Probably a 7 on a 10 scale. Good practice though.
            //The code is currently commented out in this region. Uncomment it to test it.
            #region Removing Drives via String Comparison
            /*
            for (int i = 0; i < listOfDrives.Length; i++)
            {
                Console.WriteLine("The current drive compared to the exclusion list is..." + listOfDrives[i].Name);
                //the exclusionPaths list should NOT change in size when this is running, so a foreach should be safe to use. No elements should be added/removed.
                foreach (string path in exclusionPaths)
                {
                    try
                    {
                        Console.WriteLine("Current exclusion path being looked at is..." + path); //trim \\?
                        if (String.Compare(Path.GetFullPath(path), Path.GetFullPath(listOfDrives[i].Name), StringComparison.InvariantCultureIgnoreCase) == 0)//they matched
                        {
                            Console.WriteLine("Match! Deleting from list...Will jump out from looking at the rest of the list..."); //testing
                            listOfDrives[i] = null; //attempt to remove the drive from the list
                            break; //want to stop looking through the rest of the list, the matching drive has been found and removed via null
                        }
                        else
                        {
                            Console.WriteLine("Not a match lexicographically!");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("The drive at " + i + " has hit a null-reference. So the drive was deleted in a previous run.");
                        //continue;
                    }
                }

            }*/
            #endregion


            //Initial path comparisons via String.Compare, to see the lexicographical results.
            #region Testing String.Compare and the Lexicographical results
            /*
            Console.WriteLine(@"C:\Users\Aleks");
            Console.WriteLine(String.Compare(Path.GetFullPath(@"C:\").TrimEnd('\\'), Path.GetFullPath(@"C:\Users\Aleks").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine(String.Compare(Path.GetFullPath(@"C:\").TrimEnd('\\'), Path.GetFullPath(@"D:\").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine(String.Compare(Path.GetFullPath(@"D:\").TrimEnd('\\'), Path.GetFullPath(@"C:\Users\Aleks").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine(String.Compare(Path.GetFullPath(@"D:\").TrimEnd('\\'), Path.GetFullPath(@"D:\").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine(String.Compare(Path.GetFullPath(@"D:\").TrimEnd('\\'), Path.GetFullPath(@"D:\Users\Aleks").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));

            Console.WriteLine(Path.GetPathRoot(@"C:\"));
            Console.WriteLine(Path.GetPathRoot(@"C:\Users\Aleks"));
            Console.WriteLine(String.Compare(Path.GetPathRoot(@"C:\").TrimEnd('\\'), Path.GetPathRoot(@"C:\Users\Aleks").TrimEnd('\\'), StringComparison.InvariantCultureIgnoreCase));

            */
            #endregion

            //Now for some regular expression like string matching...

            string[] subDirectories = new string[6] {@"E:\Turkey\Delicious",@"D:\", @"D:\Users", @"B:\Waffles", @"C:\", @"C:\Users"}; //Directory.GetDirectories(@"C:\");

            exclusionPaths.Add(@"C:\");
            exclusionPaths.Add(@"E:\Turkey"); //it seems E:\Turkey will work with E:\Turkey\Delicious...the second path is set to "", so it umbrellas that too.
            //What an infuriatingly simple solution. Too bad I spent a saturday on finding it hahaha...Woooow..Well, at least i'm making progress :)
            //test the removal of all the subdirectories of C:\ from the subDirectories[] via string matching.

            //The point is to see if I can umbrella paths. Like C:\ should umbrella anything C:\..., and thus I need to be able to manipulate that list
            //of sub-directories.
            foreach (string path in exclusionPaths)
            {//iterate through the exclusionPaths
                for(int i = 0; i < subDirectories.Length; i++)//iterate through all the subDirectories entries
                {
                    if (subDirectories[i].StartsWith(path)) //if the subDirectory path starts with any of the exclusion paths...change it to something else (later it might be null or something)
                        subDirectories[i] = "";
                }
            }
            
            //to prevent the console from disappearing
            Console.ReadKey();
        }
    }
}

/*
 * Alekandr Melnikov, 04/21/2012
 * Issue: The comparison that i'm using to "umbrella" exclusion paths..Like C:\ will exclude C:\Users\... or C:\Anything or C:\****...
 * This comparison SORT of works but it's a string comparison, so it's still lexicographical. I may have to do clever manipulations and testing to get these exclusion
 * paths to umbrella correctly..with this lexical comparison tool.
 * 
 * Possible Solution:
 * I could go through the array of exclusion paths and check the roots of each exclusion path against the path that subdirectory function found.
 * I'm not too concerned with performance right now, I have to focus on getting this to work well first. So i'll write shitty code and then i'll improve it just enough.
 * But anyway. To ensure lexicographical issues are avoided...Like comparing the exclusion C:\ against D:\..
 * Why is that an issue? Several reasons...
 * -C:\ should not be compared against D:\ at all. These are two separate drives, so why are they being compared?
 * -According to the lexicographical comparison nature of strings, C:\ will come out as -1 against D:\, so depending on how I write my code..Any drive that is after
 * C:\ lexicographically..will be excluded from the scan. This is very bad. BAD.
 * 
 * If I compare roots of every path against the roots in the exclusion path, I can find the correct path that I should compare against. Then i'll probably need to keep
 * going through the path, cutting it up, and comparing it...Like..
 * C:\Users versus C:\Windows. Well, C:\Users is shorter, so the string comparison will return that C:\Users is -1. Which is, again, depending on my code, it might
 * umbrella C:\Windows, even though those two paths have nothing to do with each other. So I have to keep comparing the paths until i'm certain they are the same ones..
 * I'll have to test-case this quite a bit to get a good grasp on this issue.
 * 
 * Update: 04/24/2012
 * String.StartsWith does the umbrella job beautifully. And it won't return C:\ as smaller than D:\, it actually looks to ensure that the StartsWith string is found first.
 * As soon as it finds the string, the path can be excluded...even if it has a bunch more paths after it. In this way, I can exclude paths.
 * 
*/
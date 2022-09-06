using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Tga;

namespace ImageForce1024
{
    class Program
    {
        public static TgaEncoder _tgaConfig;
        public static int _totalNumber = 0;
        static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            AssetInfo root = new AssetInfo(currentDir + "/../", "Root");
            AssetInfo.ReadAssetsInChildren(root);

            //_tgaConfig = new TgaEncoder();
            //_tgaConfig.BitsPerPixel = TgaBitsPerPixel.Pixel8;

            ModifyAsset(root);
            Console.WriteLine("Modify Image Number:" + _totalNumber);
            Console.ReadKey();
        }

        private static void ModifyAsset(AssetInfo root)
        {
            List<AssetInfo> childInfo = root.ChildAssetInfo;
            for (int i = 0; i < childInfo.Count; i++)
            {
                if (childInfo[i].AssetFileType == AssetInfo.FileType.Folder)
                {
                    ModifyAsset(childInfo[i]);
                }
                else if (childInfo[i].AssetFileType == AssetInfo.FileType.ValidFile)
                {
                    ModifyImage(childInfo[i].AssetFullPath);
                }
            }
        }

        private static void ModifyImage(string assetFullPath)
        {
            IImageInfo imageInfo = Image.Identify(assetFullPath);
            if (imageInfo.Width == imageInfo.Height && imageInfo.Width > 256)
            {
                using (Image image = Image.Load(assetFullPath))
                {
                    //image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                    image.Mutate(x => x.Resize(256, 256));
                    //image.Save(assetFullPath.Replace(".tga", "_copy.tga")/*, _tgaConfig*/);
                    image.Save(assetFullPath);
                    _totalNumber++;
                }
                Console.WriteLine("Modify Image Name:" + assetFullPath);
            }
        }
    }
}

# pic2file
Windows command line continuous clipboard image capture

When running, pic2file will save any image copied to the Windows clipboard to a file.  This will allow one to right-click and copy images from the web ( including WEBP format ) to easily save to a sequentially numbered GIF, PNG, TIFF, JPG, or BMP file.

Please see the blog post at: https://jiml.us/blog/posts/pic2file

    C:\>pic2file -h
    
    Pic2File by Jim Lawless - jimbo@radiks.net
    Github source: https://github.com/jimlawless/pic2file
    Blog post: https://jimlawless.net/blog/pic2file

    Syntax: Pic2File.exe -dir dir_to_store_pics -prefix filename_prefix -beep [ -gif -png -tiff -jpg -bmp ]

    The default output format is -jpg.

Binaries are available at https://jiml.us/downloads/pic2file.zip , however, I have had some issues with Windows Defender triggering false positives with some releases of this software.  You might need to compile your own copy.

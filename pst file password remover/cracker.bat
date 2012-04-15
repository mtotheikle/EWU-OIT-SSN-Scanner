echo cracking...
cd ..
crackme.exe -x dothis.pst
rename dothis.pst dothis.pst.old
crackme.exe -i dothis.psx
del dothis.pst.old
echo finished
REM Written by Chris 2012
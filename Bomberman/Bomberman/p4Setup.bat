@echo off
p4 set P4PORT=perforce.slctech.org:8095
p4 set P4IGNORE=%cd%\.p4ignore
SET /P variable=Please enter your perforce username:
echo.

p4 set P4USER=%variable%
rem @echo on
p4 set

echo.
echo if all the information above looks correct, you should be good to go.
echo.
echo Please make sure to run this batch file any time you work at a new PC!
echo.
echo.
PAUSE
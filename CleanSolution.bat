@echo off
if exist TestResults (rd TestResults /S /Q & echo Deleted "TestResults ")

rem delete obj & bin folder
for /f %%I in ('dir /A:D /B /S') do (
 if /i "%%~nI" == "bin" (
	if exist "%%I" (rd "%%I" /S /Q & echo Deleted "%%I")
 )else if /i "%%~nI" == "obj" (
	if exist "%%I" (rd "%%I" /S /Q & echo Deleted "%%I")
 )
)

pause
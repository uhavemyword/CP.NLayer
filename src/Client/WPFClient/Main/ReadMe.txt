
Knowledge:
Sign the application to avoid "Publisher unknown"
Option A: Purchase a cert (need money...)

Option B: Create own cert, and ask customer to install it.
B1. Create a cert:
::reference: http://blog.csdn.net/tcjiaan/article/details/12394045
::run following commands using Developer Command Prompt for VS2012
makecert -r -n "CN=CP" -b 01/01/2014 -e 01/01/2020 -sv CP.NLayer.pvk CP.NLayer.cer
Cert2spc CP.NLayer.cer CP.NLayer.spc
pvk2pfx -pvk CP.NLayer.pvk -spc CP.NLayer.spc -pfx CP.NLayer.pfx -f

B2. Sign the exe/dll/package with the cert.
signtool sign /f CP.NLayer.pfx CP.NLayer.Client.WpfClient.Main.exe 
(http://msdn.microsoft.com/zh-cn/library/8s9b9yaz.aspx)

B3. The customer need to install the cert(CP.NLayer.cer) into certificate strore "Trusted Root Certication Authorities"

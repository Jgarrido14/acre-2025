 
 

rd deploy_ACREv2_%date% /S /q

mkdir deploy_ACREv2_%date%
mkdir deploy_ACREv2_%date%\bin

copy bin\*.* deploy_ACREv2_%date%\bin

copy *.ASAX deploy_ACREv2_%date%
copy *.aspx deploy_ACREv2_%date%
copy *.ashx deploy_ACREv2_%date%
copy *.js deploy_ACREv2_%date%
copy *.css deploy_ACREv2_%date%
copy *.html deploy_ACREv2_%date%
copy *.Master deploy_ACREv2_%date%
copy *.ascx deploy_ACREv2_%date%

 
mkdir deploy_ACREv2_%date%\js
xcopy  js\*.* deploy_ACREv2_%date%\js /s /e

mkdir deploy_ACREv2_%date%\imgs
xcopy  imgs\*.* deploy_ACREv2_%date%\imgs /s /e

mkdir deploy_ACREv2_%date%\css
xcopy  css\*.* deploy_ACREv2_%date%\css /s /e


del deploy_ACREv2_%date%.7z
del deploy_ACREv2_%date%.rar
del deploy_ACREv2_%date%.zip

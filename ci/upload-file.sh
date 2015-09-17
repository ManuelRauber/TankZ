# Get the user, host and password from the Travis CI environment variables
ftpUser=${AZURE_WA_USERNAME}
ftpPassword=${AZURE_WA_PASSWORD}
ftpHost=${AZURE_WA_SITE}

# Get the filename from first command line argument
filename=$1

# Replace some paths to fit azure ftp structure
filenameAfterUpload=${filename/$(pwd)\/Build\/WebGL/\/site/wwwroot}

echo Uploading $filename to $filenameAfterUpload...
curl -u tankz\\$ftpUser:$ftpPassword --ftp-create-dirs -T $filename $ftpHost/$filenameAfterUpload

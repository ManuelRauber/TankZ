echo "Deploying to Azure via ftp..."

# Find all files for upload
find $(pwd)/Build/WebGL -type f -exec sh ./ci/upload-file.sh "{}" \;

echo "Deployment done! Have fun!"

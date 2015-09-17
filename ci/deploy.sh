echo "Deploying to Azure via rsync..."

# Get the user, host and password from the Travis CI environment variables
rsyncUser=${AZURE_WA_USERNAME}
rsyncPassword=${AZURE_WA_PASSWORD}
rsyncHost=${AZURE_WA_SITE}

# Export rsyncPassword to RSYNC_PASSWORD env variable, so we don't need to type that in
export RSYNC_PASSWORD=$rsyncPassword

# Upload webgl build to azure
rsync -e 'ssh -ax' -Iav --delete $(pwd)/Build/WebGL $rsyncUser@$rsyncHost:/

echo "Deployment done! Have fun!"

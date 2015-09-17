echo "Deploying to Azure via rsync..."

rsyncUser=${AZURE_WA_USERNAME}
rsyncPassword=${AZURE_WA_PASSWORD}
rsyncHost=${AZURE_WA_SITE}

export RSYNC_PASSWORD=$rsyncPassword

rsync -e 'ssh -ax' -Iav --delete $(pwd)/Build/WebGL $rsyncUser@$rsyncHost:/

echo "Deployment done! Have fun!"

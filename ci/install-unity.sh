#! /bin/sh

echo 'Downloading from http://netstorage.unity3d.com/unity/e7947df39b5c/MacEditorInstaller/Unity-5.2.0f3.pkg: '
curl -o Unity.pkg http://netstorage.unity3d.com/unity/e7947df39b5c/MacEditorInstaller/Unity-5.2.0f3.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /

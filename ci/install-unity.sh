#! /bin/sh

# Create unity folder if it does not exist yet
if [ ! -d ./unity ]; then
  mkdir ./unity
fi

# If unity was cached form a previous run, do not download it again
if [ ! -f ./unity/Unity.pkg ]; then
  echo 'Downloading from http://netstorage.unity3d.com/unity/cc9cbbcc37b4/MacEditorInstaller/Unity-5.3.1f1.pkg: '
  curl -o ./unity/Unity.pkg http://netstorage.unity3d.com/unity/cc9cbbcc37b4/MacEditorInstaller/Unity-5.3.1f1.pkg
else
  echo 'Using cached Unity version'
fi

# Install unity on the Travis CI worker
echo 'Installing Unity.pkg'
sudo installer -dumplog -package ./unity/Unity.pkg -target /

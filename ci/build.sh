#! /bin/sh

project="TankZ"

echo "Attempting to build $project as WebGL Output"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod TankZBuilder.WebGlBuild \
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log
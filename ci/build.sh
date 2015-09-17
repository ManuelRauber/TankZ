#! /bin/sh

# Name of the project which
project="TankZ"

echo "Attempting to build $project as WebGL Output"

# Start unity
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
# Enable batch mode (run without any human interaction)
  -batchmode \
# Don't initialize graphic devices, since we will not have any in the build environment
  -nographics \
# Don't display crash dialog (we still get them in the log file)
  -silent-crashes \
# Path of the generated log file
  -logFile $(pwd)/unity.log \
# Path to the project, which Unity will load
  -projectPath $(pwd) \
# Method which Unity will execute. This method will actually build the project
  -executeMethod TankZBuilder.WebGlBuild \
# Quit Unity when everything is done
  -quit

echo 'Logs from build'

# Echo the log file, so it is visible in Travis CI
cat $(pwd)/unity.log

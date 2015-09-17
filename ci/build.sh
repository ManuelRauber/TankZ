#! /bin/sh

# Name of the project which
project="TankZ"

echo "Attempting to build $project as WebGL Output"

# Start unity
# -batchmode: Enable batch mode (run without any human interaction)
# -nographics: Don't initialize graphic devices, since we will not have any in the build environment
# -silent-crashes: Don't display crash dialog (we still get them in the log file)
# -logFile: Path of the generated log file
# -projectPath: Path to the project, which Unity will load
# -executeMethod: Method which Unity will execute. This method will actually build the project
# -quit: Quit Unity when everything is done
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod TankZBuilder.WebGlBuild \
  -quit

echo 'Logs from build'

# Echo the log file, so it is visible in Travis CI
cat $(pwd)/unity.log

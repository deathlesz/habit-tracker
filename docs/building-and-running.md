# Building and Running the App

This project is built with [.NET MAUI](https://learn.microsoft.com/dotnet/maui), which supports cross-platform development for Android, iOS, macOS, and Windows.
The instructions differ slightly depending on your operating system and editor.

---

## Windows

### Install Workloads

1. Install **Visual Studio 2022** with the following workloads:
   - `.NET Multi-platform App UI development`
   - `Desktop development with C++` (required for Windows target)

### Run the App

- **Visual Studio (recommended):**
  1. Open the solution (`.sln`) file.
  1. Select a target (`Windows Machine`, `Android Emulator`, etc.) from the run dropdown.
  1. Press **F5** to build and run.
- **VS Code:**
  1. Install [.NET Meteor](https://marketplace.visualstudio.com/items?itemName=nromanov.dotnet-meteor) extension.
  1. Open the `Run and Debug` tab and click `create a launch.json file`.
  1. In the opened panel, select `.NET Meteor Debugger`.
  1. In the status bar, click the device name and select a target device/emulator.
  1. Press `F5` to debug or `Ctrl + F5` to run without debugging.

---

## Linux

### Installation

1. Install `.NET` using the official script:
   ```sh
   wget https://dot.net/v1/dotnet-install.sh
   chmod +x dotnet-install.sh
   ./dotnet-install.sh --channel 8.0

   export PATH=$HOME/.dotnet:$HOME/.dotnet/tools:$PATH
   ```
1. Install Android SDK tools. For Arch Linux (using `yay`, but `paru`/`trizen` also work):
   ```sh
   yay -S android-tools android-udev android-sdk-platform-tools android-sdk-cmdline-tools-latest

   export ANDROID_HOME=/opt/android-sdk
   export ANDROID_SDK_ROOT=/opt/android-sdk

   sudo groupadd android-sdk
   sudo gpasswd -a $USER android-sdk
   sudo chown -R :android-sdk /opt/android-sdk
   sudo chmod -R g+w /opt/android-sdk
   newgrp android-sdk

   export PATH=$ANDROID_HOME/cmdline-tools/latest/bin:$ANDROID_HOME/platform-tools:$PATH
   ```
1. Install necessary tools using `sdkmanager`:
   ```sh
   sdkmanager "platform-tools" "platforms;android-34" "build-tools;34.0.0"
   ```
1. Install MAUI workloads:
   ```sh
   dotnet workload update
   dotnet workload install maui-android
   ```

### Running

- **VS Code:**
  Follow the same steps as in the [Windows section](#windows).

- **Manual:**
  1. Enable USB debugging on your Android device and connect it to your PC.
  1. Verify it is detected:
     ```sh
     adb devices
     ```

     > If you see `unauthorized`, check your device for an RSA prompt.
  1. Build the solution:
     ```sh
     cd src/Presentation/HabitTracker.Presentation
     dotnet build -f net8.0-android
     ```
  1. Install the app on your device:
     ```sh
     adb install -r bin/Debug/net8.0-android/com.companyname.habittracker.presentation-Signed.apk
     ```

---

## Notes

Add environment variable exports permanently to your shell config (`~/.bashrc` or `~/.zshrc`) to avoid retyping them each session.

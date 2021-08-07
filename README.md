# BedrockServer2000

A cross-platform command line Minecraft Bedrock server wrapper (supports Windows 10 and Ubuntu 18.04)

_Some small parts of the code are based on my other repository (<https://github.com/BaoUida2000/minecraft-bedrock-server-manager>) which was forked from Benjerman's Minecraft Server Manager (<https://github.com/Benjerman/Minecraft-Server-Manager>)._

# Construction in progress

This software is still in development process, some features haven't been implemented and there are lots of bugs so don't expect it to work perfectly.

# Known bugs

- MAJOR BUG!: Online backups when loaded will cause missing files issues (no current solution).

# Compiling

This program is built with .NET Core 3.1 so you need to install the SDK to compile the code.

To build the source for different platforms, you need to use the `dotnet build` command with a RuntimeIdentifier (`-r {RuntimeIdentifier}`).

For Windows, the RID is `win-x64` and for Linux, the RID is `linux-x64`.

The same goes for `dotnet publish` and `dotnet run`.

## Example

- `dotnet run -r linux-x64`
- `dotnet build -r win-x64`
- `dotnet publish -c Release -r linux-x64`

# Start the wrapper

Simply copy the wrapper executable `bs2k` and its other 2 configuration files named `bs2k.conf` and `bs2k.banlist` to the bedrock server folder then execute the `bs2k` executable to run it.

Use the `commands` command to know more about the available commands in the program.

# Features

## Current features

- Server stop message to let players know that the server will close after a specified duration in the configuration file in seconds
- Automated backup saving, loading and other automated functions (has major bugs)
- Server exit timeout (automatically kill the server process if its exit procedure takes more than 30 seconds)
- Ban list (has minor bugs)

## Unimplemented features

- Automatic backup on DateTIme
- Logging
- Chat logging
- Better exception handlers

# Screenshots

Screenshots are obsolete.

## Misc

![app_screenshot_server_start_stop](resources/screenshots/app_screenshot_server_start_stop.png)
Server start/stop

![app_screenshot_commands](resources/screenshots/app_screenshot_commands.png)
Commands

![app_screenshot_configs](resources/screenshots/app_screenshot_configs.png)
Configs

## Automated backups saving/loading

![app_screenshot_online_backup](resources/screenshots/app_screenshot_online_backup.png)
Online backup

![app_screenshot_offline_backup](resources/screenshots/app_screenshot_offline_backup.png)
Offline backup

![app_screenshot_auto_backup](resources/screenshots/app_screenshot_auto_backup.png)
Automatic backups

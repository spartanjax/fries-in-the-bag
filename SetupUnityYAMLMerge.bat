@echo off
setlocal EnableExtensions EnableDelayedExpansion

echo ============================================================
echo   Unity Smart Merge (UnityYAMLMerge) - one-click setup
echo ============================================================
echo.

REM Make sure we are inside a Git repository, then switch to its root.
git rev-parse --show-toplevel >nul 2>nul
if errorlevel 1 (
    echo [ERROR] This folder is not inside a Git repository.
    echo Run this script from inside your Unity project folder.
    pause
    exit /b 1
)
for /f "delims=" %%R in ('git rev-parse --show-toplevel') do cd /d "%%R"

REM Make sure git is available.
where git >nul 2>nul
if errorlevel 1 (
    echo [ERROR] git was not found on your PATH.
    echo Install Git for Windows, then run this script again.
    pause
    exit /b 1
)

REM Read the Unity version this project uses.
set "UNITY_VERSION="
if exist "ProjectSettings\ProjectVersion.txt" (
    for /f "tokens=2" %%V in ('findstr /b "m_EditorVersion:" "ProjectSettings\ProjectVersion.txt"') do set "UNITY_VERSION=%%V"
)

REM Locate UnityYAMLMerge.exe.
set "MERGE_EXE="

if defined UNITY_VERSION (
    if exist "%ProgramFiles%\Unity\Hub\Editor\!UNITY_VERSION!\Editor\Data\Tools\UnityYAMLMerge.exe" (
        set "MERGE_EXE=%ProgramFiles%\Unity\Hub\Editor\!UNITY_VERSION!\Editor\Data\Tools\UnityYAMLMerge.exe"
    )
)

if not defined MERGE_EXE (
    echo Auto-detecting UnityYAMLMerge.exe ...
    for /r "%ProgramFiles%\Unity" %%F in (UnityYAMLMerge.exe) do (
        if not defined MERGE_EXE set "MERGE_EXE=%%F"
    )
)

if not defined MERGE_EXE (
    echo.
    echo [WARN] Could not auto-detect UnityYAMLMerge.exe.
    echo Drag the file onto this window and press Enter, or paste its full path.
    echo It is normally found at:
    echo   %ProgramFiles%\Unity\Hub\Editor\^<version^>\Editor\Data\Tools\UnityYAMLMerge.exe
    echo.
    set /p "MANUAL_PATH=> "
    if defined MANUAL_PATH set "MERGE_EXE=!MANUAL_PATH:"=!"
)

if not defined MERGE_EXE (
    echo [ERROR] No UnityYAMLMerge.exe provided. Nothing was changed.
    pause
    exit /b 1
)

if not exist "!MERGE_EXE!" (
    echo [ERROR] File not found: !MERGE_EXE!
    pause
    exit /b 1
)

echo Found: !MERGE_EXE!
echo.

REM Convert to forward slashes (required by Git's shell invocation).
set "MERGE_FWD=!MERGE_EXE:\=/!"

REM Register the merge driver globally so it applies to all your repos.
git config --global merge.unityyamlmerge.name "Unity Smart Merge (UnityYAMLMerge)"
git config --global merge.unityyamlmerge.driver "'!MERGE_FWD!' merge -p --force %%O %%B %%A %%A"
git config --global merge.unityyamlmerge.recursive binary

echo Registered. Verifying ...
echo.
git config --global --get merge.unityyamlmerge.name
git config --global --get merge.unityyamlmerge.driver
git config --global --get merge.unityyamlmerge.recursive

echo.
echo ============================================================
echo   Done. Scene/prefab/asset merges now use UnityYAMLMerge
echo   automatically on this machine.
echo ============================================================
echo.
pause
endlocal
exit /b 0

#include "pch.h"
#include <windows.h>
#include <CommCtrl.h>
#pragma comment(lib, "Comctl32.lib")

extern "C" __declspec(dllexport) int ShowUpdateMessage(HWND window, const wchar_t* msg)
{
	int result = 0;
	HRESULT hr = TaskDialog(window, nullptr, L"Update", L"A new version is available", msg, TDCBF_YES_BUTTON | TDCBF_NO_BUTTON, TD_INFORMATION_ICON, &result);
	if (result == IDYES)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

extern "C" __declspec(dllexport) void ShowAboutBox(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"About Application Blocker", L"Application Blocker v2.2.0\nCoded By: Emir Alp Koçak using C# and C++", TDCBF_OK_BUTTON, TD_INFORMATION_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ShowUpdateError(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Error", L"Unable to update Application Blocker", msg, TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void NoNewVersionMessage(HWND window)
{
	TaskDialog(window, nullptr, L"Update", L"No new versions available", L"No new versions of Application Blocker available at this time.", TDCBF_OK_BUTTON, TD_INFORMATION_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ShowUpdateCheckError(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Error", L"Unable to check for updates", msg, TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ShowBlockError(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Error", L"Unable to block application", msg, TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ShowUnblockError(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Error", L"Unable to unblock application", msg, TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ShowSelectApp(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Please select an application from the list", L"Please use the list to select the application you want to unblock.", TDCBF_OK_BUTTON, nullptr, nullptr);
}

extern "C" __declspec(dllexport) void ShowHelpError(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Error", L"Unable to open help file", msg, TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void SuccessfulUnblock(HWND window, const wchar_t* msg)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Unblock successful", msg, TDCBF_OK_BUTTON, TD_INFORMATION_ICON, nullptr);
}

extern "C" __declspec(dllexport) void ColorChanged(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Color has been changed", L"Color has been changed. Please click OK to restart Application Blocker.", TDCBF_OK_BUTTON, TD_INFORMATION_ICON, nullptr);
}

extern "C" __declspec(dllexport) void PasswordEmptyError(HWND window)
{
	TaskDialog(window, nullptr, L"Error", L"Password cannot be empty", L"You must set a password to use Application Blocker.", TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void IncorrectPasswordError(HWND window)
{
	TaskDialog(window, nullptr, L"Error", L"Password is incorrect", L"Please enter the correct password.", TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void PasswordWarning(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Password system changed", L"The way Application Blocker handles password has been changed. You must reset your password.", TDCBF_OK_BUTTON, TD_WARNING_ICON, nullptr);
}

extern "C" __declspec(dllexport) void CurrentPasswordIncorrect(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Current password is incorrect", L"Please enter your current password to change it.", TDCBF_OK_BUTTON, TD_ERROR_ICON, nullptr);
}

extern "C" __declspec(dllexport) void PasswordChanged(HWND window)
{
	TaskDialog(window, nullptr, L"Application Blocker", L"Password has been changed", L"You have to use your new password when launching Application Blocker.", TDCBF_OK_BUTTON, TD_INFORMATION_ICON, nullptr);
}
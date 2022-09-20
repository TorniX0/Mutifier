#include <stdio.h>
#include <windows.h>
#include <mmdeviceapi.h>
#include <endpointvolume.h>

#define Hotkey _declspec(dllexport)

extern "C"
{
    Hotkey void ChangeMicVolumeA(float volume)
    {
        HRESULT hr;
        CoInitialize(NULL);
        IMMDeviceEnumerator* deviceEnumerator = NULL;
        hr = CoCreateInstance(__uuidof(MMDeviceEnumerator), NULL, CLSCTX_INPROC_SERVER, __uuidof(IMMDeviceEnumerator), (LPVOID*)&deviceEnumerator);
        IMMDevice* defaultDevice = NULL;

        hr = deviceEnumerator->GetDefaultAudioEndpoint(eCapture, eConsole, &defaultDevice);
        deviceEnumerator->Release();
        deviceEnumerator = NULL;

        IAudioEndpointVolume* endpointVolume = NULL;
        hr = defaultDevice->Activate(__uuidof(IAudioEndpointVolume), CLSCTX_INPROC_SERVER, NULL, (LPVOID*)&endpointVolume);
        defaultDevice->Release();
        defaultDevice = NULL;

        hr = endpointVolume->SetMasterVolumeLevel(volume, NULL);
        endpointVolume->Release();
        CoUninitialize();
    }
}
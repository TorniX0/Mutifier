#include <stdio.h>
#include <windows.h>
#include <mmdeviceapi.h>
#include <endpointvolume.h>

#define Microphone _declspec(dllexport)

extern "C" 
{
    Microphone void ChangeMicVolume(float volume)
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

    Microphone float GetMicVolume()
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

        float currentVolume = 0;
        endpointVolume->GetMasterVolumeLevel(&currentVolume);

        endpointVolume->Release();
        CoUninitialize();

        return currentVolume;
    }

    Microphone void SetMicMuted(bool muted)
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

        hr = endpointVolume->SetMute(muted, NULL);
        endpointVolume->Release();
        CoUninitialize();
    }

    Microphone bool GetMicMuted()
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

        BOOL muted = false;

        hr = endpointVolume->GetMute(&muted);
        endpointVolume->Release();
        CoUninitialize();

        return muted;
    }
}
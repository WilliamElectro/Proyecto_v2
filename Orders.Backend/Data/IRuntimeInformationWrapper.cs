using System.Runtime.InteropServices;

namespace PGCELL.Backend.Data
{
    public interface IRuntimeInformationWrapper
    {
        bool IsOSPlatform(OSPlatform osPlatform);
    }
}
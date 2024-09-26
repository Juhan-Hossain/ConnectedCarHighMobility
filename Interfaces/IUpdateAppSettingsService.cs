using Demo_APP.Dtos;

namespace Demo_APP.Interfaces
{
    public interface IUpdateAppSettingsService
    {
        void UpdateAppSetting(AppSettingsUpdateDto dto);
    }
}
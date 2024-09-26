namespace Demo_APP.Dtos
{
    public class AppSettingsUpdateDto
    {
        public string Section { get; set; } // The section to modify
        public string Key { get; set; }     // The key within the section
        public string Value { get; set; }   // The new value
    }

}

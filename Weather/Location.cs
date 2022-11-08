using System;
using System.Device.Location;
using System.Windows.Forms;

namespace Weather
{
    class Location
    {
        GeoCoordinateWatcher watcher;
        GeoCoordinate coordinate;

        private string _defaulLocation = "Volgograd";

        public Location() { }

        public GeoCoordinate Coordinate => coordinate;

        public void Find(ComboBox list)
        {
            watcher = new GeoCoordinateWatcher();

            watcher.TryStart(true, TimeSpan.FromMilliseconds(1000));

            coordinate = watcher.Position.Location;

            if (coordinate.IsUnknown)
            {
                if (list.Items.Contains(_defaulLocation)) return;

                list.Items.Add(_defaulLocation);
                list.SelectedItem = _defaulLocation;

                Settings.s_SelectedLocation = _defaulLocation;

                Properties.Settings.Default.cb = list.SelectedItem.ToString();
                Properties.Settings.Default.Save();

                return;
            }

            if (list.Items.Contains(coordinate.ToString())) return;

            list.Items.Add(coordinate.ToString());

            Properties.Settings.Default.cb = list.SelectedItem.ToString();

            Properties.Settings.Default.Save();
        }
    }
}

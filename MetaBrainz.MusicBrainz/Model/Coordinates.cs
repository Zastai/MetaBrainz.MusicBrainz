using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Coordinates : Item, ICoordinates {

    #region XML Elements

    [XmlElement("latitude")]  public double Latitude;
    [XmlElement("longitude")] public double Longitude;

    #endregion

    #region ICoordinates

    double ICoordinates.Latitude => this.Latitude;

    double ICoordinates.Longitude => this.Longitude;

    #endregion

  }

}

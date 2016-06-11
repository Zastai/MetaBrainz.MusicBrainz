using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Coordinates : Item {

    [XmlElement("latitude")]  public double Latitude;
    [XmlElement("longitude")] public double Longitude;

  }

}

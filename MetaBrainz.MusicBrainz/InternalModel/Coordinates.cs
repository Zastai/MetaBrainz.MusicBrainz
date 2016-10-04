using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class Coordinates : Item, ICoordinates {

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

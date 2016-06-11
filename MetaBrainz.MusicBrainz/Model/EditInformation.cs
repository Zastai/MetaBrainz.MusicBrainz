using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class EditInformation : Item {

    [XmlElement("auto-edits-accepted")] public uint AutoEditsAccepted;
    [XmlElement("edits-accepted")]      public uint EditsAccepted;
    [XmlElement("edits-failed")]        public uint EditsFailed;
    [XmlElement("edits-rejected")]      public uint EditsRejected;

  }

}

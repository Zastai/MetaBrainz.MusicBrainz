using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class AnnotationList : ItemList, IResourceList<IAnnotation> {

    [XmlElement("annotation")] public Annotation[] Items;

    #region Implementation of IResourceList<out IAnnotation>

    uint? IResourceList<IAnnotation>.Count => this.ListCount;

    uint? IResourceList<IAnnotation>.Offset => this.ListOffset;

    IEnumerable<IAnnotation> IResourceList<IAnnotation>.Items => this.Items;

    #endregion

  }

}

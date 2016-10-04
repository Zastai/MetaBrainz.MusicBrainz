using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class AnnotationList : ItemList, IResourceList<IAnnotation> {

    [XmlElement("annotation")] public Annotation[] Items;

    #region IResourceList<IAnnotation>

    uint? IResourceList<IAnnotation>.Count => this.ListCount;

    uint? IResourceList<IAnnotation>.Offset => this.ListOffset;

    IEnumerable<IAnnotation> IResourceList<IAnnotation>.Items => this.Items;

    #endregion

  }

}

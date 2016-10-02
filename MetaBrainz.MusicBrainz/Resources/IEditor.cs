using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IEditor : IResource {

    uint Id { get; }

    string Age { get; }

    IArea Area { get; }

    string Bio { get; }

    IEditInformation EditInformation { get; }

    ITextResource Gender { get; }

    string HomePage { get; }

    IResourceList<ISpokenLanguage> LanguageList { get; }

    DateTime? MemberSince { get; }

    string Name { get; }

    uint? Privileges { get; }

  }

}
